using UnityEngine;
using Unity.InferenceEngine;
using TMPro;

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
    public int sizeOutput;
    public int sizeInput;
    public ModelAsset model;
    [Header("Display")]
    public Camera camDisplay;
    public TMP_Text textInfo;
    [Header("Target")]
    public GameObject target;
    public GameObject marker;
    public Vector3 posMarker;
}
