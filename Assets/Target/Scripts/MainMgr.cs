using UnityEngine;

public class MainMgr : MonoBehaviour
{
    public GlobalsMgr g;

    void Start()
    {
        switch (g.mode)
        {
            case ModeType.none:
                break;
            case ModeType.train:
                g.trainMgr.StartTraining();
                Invoke(nameof(StartTraining), 1);
                break;
            case ModeType.infer:
                g.inferMgr.StartInference();
                break;
        }
    }

    void Update()
    {
        g.targetMgr.AnimateTarget();        
        TrainOrInfer();
        g.markerMgr.UpdateMarker();
    }

    void TrainOrInfer()
    {
        switch (g.mode)
        {
            case ModeType.none:
                break;
            case ModeType.train:
                if (g.ynTraining)
                {
                    g.trainMgr.AddTrainingData();
                }
                break;
            case ModeType.infer:
                g.inferMgr.Inference();
                break;
        }
    }

    void StartTraining()
    {
        g.ynTraining = true;
    }
}
