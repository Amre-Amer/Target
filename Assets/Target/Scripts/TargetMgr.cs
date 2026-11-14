using UnityEngine;

public class TargetMgr : MonoBehaviour
{
    public GlobalsMgr g;

    void Start()
    {
        g.centerTarget = g.target.transform.position;
    }

    void Update()
    {
        AnimateNext();        
    }

    public void AnimateNext()
    {
        float x = g.centerTarget.x + g.radTarget * Mathf.Cos(g.angTarget * Mathf.Deg2Rad);
        float y = g.centerTarget.y + g.radTarget * Mathf.Sin(g.angTarget * Mathf.Deg2Rad);
        float z = g.target.transform.position.z;

        Vector3 pos = g.centerTarget + g.radTarget * Random.insideUnitSphere;

        g.target.transform.position = pos; //  new(x, y, z);
        g.angTarget += g.speedTarget;        
    }
}
