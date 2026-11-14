using System.IO;
using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    public GlobalsMgr g;

    void TestClassification()
    {
        g.posNormal = new(.5f, .5f, .5f);
        SetClassification();
        Debug.Log(g.posNormal.x.ToString("F2") + ", " + g.posNormal.y.ToString("F2") + ", " + g.posNormal.z.ToString("F2") + " g.nClassification: " + g.nClassification + "\n");    

        g.posNormal = new(.95f, .95f, .95f);
        SetClassification();
        Debug.Log(g.posNormal.x.ToString("F2") + ", " + g.posNormal.y.ToString("F2") + ", " + g.posNormal.z.ToString("F2") + " g.nClassification: " + g.nClassification + "\n");    

        g.posNormal = new(.25f, .25f, .25f);
        SetClassification();
        Debug.Log(g.posNormal.x.ToString("F2") + ", " + g.posNormal.y.ToString("F2") + ", " + g.posNormal.z.ToString("F2") + " g.nClassification: " + g.nClassification + "\n");    
    }

    public void AddTally()
    {
        g.tally[g.nClassification]++;
    }

    public void ShowTally()
    {
        string txt = "Tally";
        for(int n = 0; n < g.numClasses; n++)
        {
            int v = g.tally[n];
            txt += "\n" + n + ": " + v + "\t" + new string('|', v);
        }
        g.toolsMgr.SetInfo(txt);
        Debug.Log(txt + "\n");
    }

    public void CheckAddFolder(string pathspec)
    {
        if (!Directory.Exists(pathspec))
        {
            Directory.CreateDirectory(pathspec);
        }
    }

    public void ResetFolders()
    {
        CleanClassFolder();
        CreateClassFolder();
        CreateClassSubFolders();
    }

    void CleanClassFolder()
    {
        CleanFolder(g.pathImageData);        
    }

    void CleanFolder(string pathspec)
    {
        if (Directory.Exists(pathspec))
        {
            Directory.Delete(pathspec, true);
        }
    }

    void CreateClassFolder()
    {
        CheckAddFolder(g.pathImageData);
    }

    void CreateClassSubFolders()
    {
        for(int n = 0; n < g.numClasses; n++)
        {
            g.nClassification = n;
            SetPathClass();
            string pathFolder = Path.Combine(g.pathImageData, g.pathClass);
            CheckAddFolder(pathFolder);
        }
    }

    public void GrabImage()
    {
        g.textureGrabbed = RenderTextureToTexture2D(g.renderTexture);
    }

    public Texture2D RenderTextureToTexture2D(RenderTexture rTex)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = rTex;

        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);

        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();

        RenderTexture.active = currentActiveRT;
        return tex;
    }

    public void SaveImage()
    {
        SetFilename();
        SetFilespec();
        g.bytes = g.textureGrabbed.EncodeToPNG();
        File.WriteAllBytes(g.filespec, g.bytes);
        // Debug.Log("SaveImage: " + g.filespec + "\n");
    }

    void SetFilespec()
    {
        g.filespec = Path.Combine(g.pathImageData, g.pathClass, g.filename);
    }

    void SetFilename()
    {
        g.filename = "Image-" + g.cntFrames + "-" + g.pathClass + ".png";
    }

    public void SetClassification()
    {
        //  6   7   8
        //  3   4   5
        //  0   1   2
        g.nClassification = -1;
        int side = (int)Mathf.Sqrt(g.numClasses);
        int ix = (int)(g.posNormal.x * side);
        int iy = (int)(g.posNormal.y * side);
        g.nClassification = iy * side + ix;
        Debug.Log(g.posNormal.ToString("F2") + " SetClassification: ix,iy: " + ix + ", " + iy + " g.nClassification: " + g.nClassification + "\n");
    }

    public void SetPathClass()
    {
        g.pathClass = "class_" + g.nClassification;
    }

    public void SetPosTarget()
    {
        g.posScreen = GetPosScreen(g.target.transform.position);
        g.posNormal = Normalize(g.posScreen);

        g.posTarget = GetPosTarget(g.posScreen);
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

    public void SetInfo(string txt)
    {
        g.textInfo.text = txt;
        // Debug.Log(txt + "\n");
    }

    string PosToString(Vector3 pos)
    {
        string f = "F2";
        string txt = "pos: " + pos.x.ToString(f) + ", " + pos.y.ToString(f) + ", " + pos.z.ToString(f);
        return txt;
    }
}
