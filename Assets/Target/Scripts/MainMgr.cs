using UnityEngine;

public class MainMgr : MonoBehaviour
{
    public GlobalsMgr g;

    void Start()
    {
        if (g.ynTrain)
        {
            g.trainMgr.StartTraining();
            Invoke(nameof(StartTraining), 1);
        } else
        {
            g.inferMgr.StartInference();
        }
    }

    void Update()
    {
        g.targetMgr.AnimateTarget();        
        if (g.ynTrain)
        {
            if (g.ynTraining)
            {
                g.trainMgr.AddTrainingData();
            }
        } else
        {
            g.inferMgr.Infer();
        }
        g.displayMgr.UpdateMarker();
    }

    void StartTraining()
    {
        g.ynTraining = true;
    }
}
