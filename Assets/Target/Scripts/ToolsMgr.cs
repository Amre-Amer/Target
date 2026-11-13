using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    public GlobalsMgr g;
    public Vector3 GetTargetPosition()
    {
        Vector3 posScreen = GetPosScreen(g.target.transform.position);
        Debug.Log("posScreen: " + posScreen + "\n");
        Vector3 posNormal = Normalize(posScreen);
        string txt = PosToString(posNormal);
        SetInfo(txt);
        Vector3 posTarget = GetPosTarget(posScreen);
        return posTarget;
    } 
    Vector3 GetPosTarget(Vector3 posScreen)
    {
        float x = posScreen.x / g.width;
        float y = posScreen.y / g.height;
        float z = -1 * g.camDisplay.nearClipPlane;
        x -= .5f;
        y -= .5f;
        Vector3 pos = g.camDisplay.transform.TransformPoint(x, y, z);
        return pos;
    }

    Vector3 Normalize(Vector3 pos)
    {
        float x = pos.x / g.width;
        float y = pos.y / g.height;
        float z = pos.z / g.depth;
        return new(x, y, z);
    }

    Vector3 GetPosScreen(Vector3 pos)
    {
        int ww = g.width;
        int hh = g.height;        
        Vector3 posScreen = g.camDisplay.WorldToScreenPoint(pos);
        if (posScreen.x > ww) posScreen.x = ww;
        if (posScreen.x < -ww) posScreen.x = -ww;
        if (posScreen.y > hh) posScreen.y = hh;
        if (posScreen.y < -hh) posScreen.y = -hh;
        if (posScreen.z > g.depth) posScreen.z = g.depth;
        if (posScreen.z < 0) posScreen.z = 0;
        return posScreen;
    }

    void AddInfo(string txt)
    {
        string txtInfo = g.textInfo.text;
        string s = "\n";
        if (txtInfo == "") s = "";
        txtInfo += s + txt;
        SetInfo(txtInfo);
    }

    void SetInfo(string txt)
    {
        g.textInfo.text = txt;
        Debug.Log(txt + "\n");
    }

    string PosToString(Vector3 pos)
    {
        string f = "F2";
        string txt = "pos: " + pos.x.ToString(f) + ", " + pos.y.ToString(f) + ", " + pos.z.ToString(f);
        return txt;
    }
}
