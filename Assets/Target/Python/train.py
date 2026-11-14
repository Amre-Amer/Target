import torch
import torch.nn as nn
import torch.optim as optim
from torch.utils.data import DataLoader
from torchvision import datasets, transforms

# 1. Configuration
DATA_DIR = "./image_data"       # Path to your root image folder
ONNX_PATH = "target.onnx"
NUM_CLASSES = 9
BATCH_SIZE = 32
EPOCHS = 10
LEARNING_RATE = 0.001

device = torch.device("cuda" if torch.cuda.is_available() else "cpu")

# 2. Data Loading & Preprocessing
# Resizes images to 60x60 and converts them to [0.0, 1.0] tensors
transform = transforms.Compose([
    transforms.Resize((60, 60)),
    transforms.ToTensor(),
    # Optional: Add normalization if needed, e.g.:
    # transforms.Normalize(mean=[0.5, 0.5, 0.5], std=[0.5, 0.5, 0.5])
])

dataset = datasets.ImageFolder(root=DATA_DIR, transform=transform)
dataloader = DataLoader(dataset, batch_size=BATCH_SIZE, shuffle=True)

print(f"Found {len(dataset)} images across {len(dataset.classes)} classes.")
print(f"Class mapping: {dataset.class_to_idx}")

# 3. Lightweight CNN Model for 60x60 RGB Images
class SimpleCNN(nn.Module):
    def __init__(self, num_classes=9):
        super().__init__()
        self.features = nn.Sequential(
            # Input: (3, 60, 60)
            nn.Conv2d(3, 16, kernel_size=3, padding=1),
            nn.ReLU(),
            nn.MaxPool2d(2, 2),  # -> (16, 30, 30)

            nn.Conv2d(16, 32, kernel_size=3, padding=1),
            nn.ReLU(),
            nn.MaxPool2d(2, 2),  # -> (32, 15, 15)

            nn.Conv2d(32, 64, kernel_size=3, padding=1),
            nn.ReLU(),
            nn.MaxPool2d(2, 2),  # -> (64, 7, 7)
        )
        self.classifier = nn.Sequential(
            nn.Flatten(),
            nn.Linear(64 * 7 * 7, 128),
            nn.ReLU(),
            nn.Linear(128, num_classes)
        )

    def forward(self, x):
        x = self.features(x)
        return self.classifier(x)

model = SimpleCNN(num_classes=NUM_CLASSES).to(device)
criterion = nn.CrossEntropyLoss()
optimizer = optim.Adam(model.parameters(), lr=LEARNING_RATE)

# 4. Training Loop
model.train()
for epoch in range(EPOCHS):
    running_loss = 0.0
    for images, labels in dataloader:
        images, labels = images.to(device), labels.to(device)

        optimizer.zero_grad()
        outputs = model(images)
        loss = criterion(outputs, labels)
        loss.backward()
        optimizer.step()

        running_loss += loss.item()

    print(f"Epoch [{epoch+1}/{EPOCHS}] - Loss: {running_loss / len(dataloader):.4f}")

# 5. Export to ONNX
model.eval()
# Create dummy input matching (batch_size, channels, height, width)
dummy_input = torch.randn(1, 3, 60, 60, device=device)

torch.onnx.export(
    model,
    dummy_input,
    ONNX_PATH,
    export_params=True,
    opset_version=17,
    do_constant_folding=True,
    input_names=["input"],
    output_names=["output"],
    dynamic_axes={
        "input": {0: "batch_size"},
        "output": {0: "batch_size"}
    }
)

print(f"Model successfully exported to {ONNX_PATH}")
