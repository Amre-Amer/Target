using UnityEngine;

public class DisplayMgr : MonoBehaviour
{
    public GlobalsMgr g;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        g.toolsMgr.SetPosTarget();
        g.marker.transform.position = g.posMarker;
    }
}
