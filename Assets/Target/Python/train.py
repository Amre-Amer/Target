import torch
import torch.nn as nn
import torch.optim as optim
from torch.utils.data import DataLoader
from torchvision import datasets, transforms

# 1. Configuration
IMG_SIZE = 14 # 60

DATA_DIR = "./image_data"
ONNX_PATH = "target.onnx"
BATCH_SIZE = 32
EPOCHS = 1000
LEARNING_RATE = 1e-3
NUM_CLASSES = 9
CHANNELS = 3  # Change to 1 if using grayscale images


device = torch.device("cuda" if torch.cuda.is_available() else "cpu")

# 2. Data Loading & Preprocessing
transform = transforms.Compose([
    transforms.Resize((IMG_SIZE, IMG_SIZE)),
    # transforms.Grayscale(num_output_channels=1), # Uncomment if CHANNELS = 1
    transforms.ToTensor(),
    transforms.Normalize(mean=[0.5] * CHANNELS, std=[0.5] * CHANNELS)
])

dataset = datasets.ImageFolder(root=DATA_DIR, transform=transform)
dataloader = DataLoader(dataset, batch_size=BATCH_SIZE, shuffle=True)

print(f"Found {len(dataset)} images across {len(dataset.classes)} classes: {dataset.classes}")

# 3. Define the MLP Model
class ImageMLP(nn.Module):
    def __init__(self, input_dim, num_classes):
        super().__init__()
        self.net = nn.Sequential(
            nn.Flatten(),
            nn.Linear(input_dim, 512),
            nn.ReLU(),
            nn.Dropout(0.2),
            nn.Linear(512, 128),
            nn.ReLU(),
            nn.Linear(128, num_classes)
        )

    def forward(self, x):
        return self.net(x)

input_dim = CHANNELS * IMG_SIZE * IMG_SIZE  # 3 * 60 * 60 = 10,800
model = ImageMLP(input_dim=input_dim, num_classes=NUM_CLASSES).to(device)

# 4. Loss & Optimizer
criterion = nn.CrossEntropyLoss()
optimizer = optim.Adam(model.parameters(), lr=LEARNING_RATE)

# 5. Training Loop
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
        
        running_loss += loss.item() * images.size(0)
        
    epoch_loss = running_loss / len(dataset)
    graph = "|" * int(epoch_loss * 100);
    print(f"Epoch [{epoch+1}/{EPOCHS}] - Loss: {epoch_loss:.4f}\t{graph}")

# 6. Export to ONNX
model.eval()
dummy_input = torch.randn(1, CHANNELS, IMG_SIZE, IMG_SIZE, device=device)

torch.onnx.export(
    model,
    dummy_input,
    ONNX_PATH,
    export_params=True,
    opset_version=17,
    do_constant_folding=True,
    input_names=["input"],
    output_names=["output"],
    dynamic_axes={"input": {0: "batch_size"}, "output": {0: "batch_size"}}
)

print(f"Model successfully saved to {ONNX_PATH}")
