using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    public GlobalsMgr g;
    public Vector3 GetTargetPosition()
    {
        Vector3 posScreen = g.camDisplay.WorldToScreenPoint(g.target.transform.position);
        Debug.Log("pos: " + posScreen.x + ", " + posScreen.y + ", " + posScreen.z + "\n");
        float x = posScreen.x / g.width;
        float y = posScreen.y / g.height;
        float z = -1 * g.camDisplay.nearClipPlane;
        Vector3 pos = g.camDisplay.transform.TransformPoint(x, y, z);
        return pos;
    }
}
