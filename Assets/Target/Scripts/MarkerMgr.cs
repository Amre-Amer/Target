using UnityEngine;

public class MarkerMgr : MonoBehaviour
{
    public GlobalsMgr g;

    public void UpdateMarker()
    {
        switch (g.mode)
        {
            case ModeType.none:
                break;
            case ModeType.train:
                g.toolsMgr.SetPosMarkerFromPosTarget();
                break;
            case ModeType.infer:
                g.toolsMgr.SetPosMarkerFromNClassification();            
                break;
        }
        g.marker.transform.position = g.posMarker;
    }
}
