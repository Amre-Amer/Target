using UnityEngine;

public class TargetMgr : MonoBehaviour
{
    public GlobalsMgr g;

    void Start()
    {
        if (g.ynTrain)
        {
            g.trainMgr.StartTraining();
        } else
        {
            
        }
    }

    void Update()
    {
        if (g.ynTrain)
        {
            g.trainMgr.AddTrainingData();
        } else
        {
            
        }
    }
}
