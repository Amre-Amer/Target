using UnityEngine;

public class InferMgr : MonoBehaviour
{
    public GlobalsMgr g;

    public void StartInference()
    {
        
    }

    public void Infer()
    {
        g.toolsMgr.GrabImage();
        Inference();
        g.toolsMgr.AddTally();
        g.toolsMgr.ShowTally();
    }

    void Inference()
    {
        g.nClassification = Random.Range(0, g.numClasses);
    }
}
