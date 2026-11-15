using UnityEngine;

public class DisplayMgr : MonoBehaviour
{
    public GlobalsMgr g;

    public void UpdateMarker()
    {
        g.toolsMgr.SetPosScreen();
        g.toolsMgr.SetPosMarker();
        g.marker.transform.position = g.posMarker;
    }
}
