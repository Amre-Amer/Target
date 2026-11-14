using UnityEngine;

public class TrainMgr : MonoBehaviour
{
    public GlobalsMgr g;

    public void StartTraining()
    {
        g.toolsMgr.ResetFolders();
        g.toolsMgr.ClearTally();
    }

    public void AddTrainingData()
    {
        g.toolsMgr.GrabImage();
        g.toolsMgr.SetClassification();
        g.toolsMgr.AddTally();
        g.toolsMgr.SetPathClass();
        g.toolsMgr.SaveImage();
        g.cntTraining++;
        SetInfoTraining();
        if (g.cntTraining == g.maxTraining)
        {
            g.ynTraining = false;
            g.toolsMgr.ShowTally();
        }
    }

    void SetInfoTraining()
    {
        string txt = "training: " + g.cntTraining + " / " + g.maxTraining;
        g.toolsMgr.SetInfo(txt);
    }
}
