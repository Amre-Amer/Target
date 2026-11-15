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
        if (g.nClassification < 0 || g.nClassification >= g.numClasses)
        {
            Debug.Log("skipped: g.nClassification = " + g.nClassification + "\n");
            return;
        }
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
        // shouldn't this be the data
        // maybe encode in image or switch to text files ?
        g.toolsMgr.SetPosNormalized();
    }

    void SetInfoTraining()
    {
        string txt = "training: " + g.cntTraining + " / " + g.maxTraining;
        g.toolsMgr.SetInfo(txt);
    }
}
