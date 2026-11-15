using UnityEngine;
using Unity.InferenceEngine;
using TMPro;
using System.IO;

public class GlobalsMgr : MonoBehaviour
{
    [Header("Classes")]
    public MainMgr mainMgr;
    public ToolsMgr toolsMgr;
    public TrainMgr trainMgr;
    public InferMgr inferMgr;
    public TargetMgr targetMgr;
    public DisplayMgr displayMgr;

    [Header("Inference")]
    public bool ynTrain;
    public float depth = 3;
    public int size;
    public int width;
    public int height;
    public int numClasses = 9; //100;
    public int sizeOutput;
    public int sizeInput;
    public ModelAsset model;
    public string filespec;
    public string filename;
    public int nClassification;
    public string pathImageData;
    public string pathImageDataFolder = "image_data";
    public string pathClass;
    public byte[] bytes;
    public bool ynTraining;
    public int maxTraining = 100;
    public Worker worker;
    public Tensor<float> inputTensor;

    [Header("Display")]
    public Camera camDisplay;
    public RenderTexture renderTexture;
    public TMP_Text textInfo;
    
    [Header("Target")]
    public GameObject target;
    public GameObject marker;
    public Vector3 posMarker;
    public Vector3 posTarget;
    public Vector3 posScreen;
    public Vector3 posNormalized;
    public float radTarget;
    public float speedTarget;
    public float angTarget;
    public Vector3 centerTarget;
    public int[] tally;

    [Header("None til Play")]
    public Texture2D textureGrabbed;

    [Header("Other")]
    public int cntFrames;
    public int cntFps;
    public TMP_Text textFps;
    public int cntTraining;
    public int lengthInfo;

    void Awake()
    {
        size = 14;
        maxTraining = 1000;

        pathImageDataFolder = "image_data";
        pathImageData = Path.Combine(Application.dataPath, "Target/Python/", pathImageDataFolder);        
        width = size; //60;
        height = size; //60;
        depth = 3;
        numClasses = 9; //100;
        radTarget = .5f;
        speedTarget = 2;
        lengthInfo = 100;
        renderTexture.width = width;
        renderTexture.height = height;
    }

    void Start()
    {
        InvokeRepeating(nameof(Fps), 1, 1);
    }

    void Update()
    {
        cntFrames++;
        cntFps++;
    }

    void Fps()
    {
        textFps.text = "fps: " + cntFps;
        cntFps = 0;
    }
}

