using UnityEngine;

public class GlobalsMgr : MonoBehaviour
{
    [Header("Classes")]
    public ToolsMgr toolsMgr;
    public TrainMgr trainMgr;
    public InferMgr inferMgr;
    public TargetMgr targetMgr;
    public DisplayMgr displayMgr;
    [Header("Display")]
    public Camera camDisplay;
    [Header("Target")]
    public GameObject target;
    public GameObject marker;
    public Vector3 posMarker;
    public int width = 60;
    public int height = 60;
}
