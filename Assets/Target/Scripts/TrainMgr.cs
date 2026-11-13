using UnityEngine;

public class TrainMgr : MonoBehaviour
{
    public GlobalsMgr g;

    public void StartTraining()
    {
        Debug.Log("StartTraining\n");
        g.toolsMgr.ResetFolders();
    }

    public void AddTrainingData()
    {
        g.toolsMgr.GrabImage();
        g.toolsMgr.SetClassification();
        g.toolsMgr.SetPathClass();
        g.toolsMgr.SaveImage();
        Debug.Log("AddTrainingData\n");
    }
}
