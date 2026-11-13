using UnityEngine;
using Unity.InferenceEngine;
using TMPro;
using System.IO;

public class GlobalsMgr : MonoBehaviour
{
    [Header("Classes")]
    public ToolsMgr toolsMgr;
    public TrainMgr trainMgr;
    public InferMgr inferMgr;
    public TargetMgr targetMgr;
    public DisplayMgr displayMgr;
    [Header("Inference")]
    public bool ynTrain;
    public int width = 60;
    public int height = 60;
    public float depth = 3;
    public int numClasses = 9; //100;
    public Texture2D textureGrabbed;
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
    [Header("Display")]
    public Camera camDisplay;
    public Material matDisplay;
    public TMP_Text textInfo;
    [Header("Target")]
    public GameObject target;
    public GameObject marker;
    public Vector3 posMarker;
    public Vector3 posTarget;
    public Vector3 posScreen;
    public Vector3 posNormal;

    void Awake()
    {
        pathImageData = Path.Combine(Application.dataPath, "Target/Python/", pathImageDataFolder);
    }
}

