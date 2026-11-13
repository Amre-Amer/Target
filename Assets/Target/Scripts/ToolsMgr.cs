using System.IO;
using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    public GlobalsMgr g;

    void Start()
    {
        // CheckFolder(g.pathImageData);
        return;
        g.posNormal = new(.5f, .5f, .5f);
        SetClassification();
        // SetClassification2();
        Debug.Log(g.posNormal.x.ToString("F2") + ", " + g.posNormal.y.ToString("F2") + ", " + g.posNormal.z.ToString("F2") + " g.nClassification: " + g.nClassification + "\n");    

        g.posNormal = new(.95f, .95f, .95f);
        SetClassification();
        // SetClassification2();
        Debug.Log(g.posNormal.x.ToString("F2") + ", " + g.posNormal.y.ToString("F2") + ", " + g.posNormal.z.ToString("F2") + " g.nClassification: " + g.nClassification + "\n");    

        g.posNormal = new(.25f, .25f, .25f);
        SetClassification();
        // SetClassification2();
        Debug.Log(g.posNormal.x.ToString("F2") + ", " + g.posNormal.y.ToString("F2") + ", " + g.posNormal.z.ToString("F2") + " g.nClassification: " + g.nClassification + "\n");    
    }

    public void CheckFolder(string pathspec)
    {
        if (!Directory.Exists(pathspec))
        {
            Directory.CreateDirectory(pathspec);
            Debug.Log("Created folder at: " + pathspec);
        }  else
        {
            Debug.Log("Existing folder at: " + pathspec);
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
        if (!Directory.Exists(pathspec))
        {
            //Directory.Delete(pathspec);
            Debug.Log("Deleted folder at: " + pathspec);
        }
    }

    void CreateClassFolder()
    {
        CheckFolder(g.pathImageData);
    }

    void CreateClassSubFolders()
    {
        for(int n = 0; n < g.numClasses; n++)
        {
            g.nClassification = n;
            SetPathClass();
            string pathFolder = Path.Combine(g.pathImageData, g.pathClass);
            CheckFolder(pathFolder);
        }
    }

    public void GrabImage()
    {
        g.textureGrabbed = g.matDisplay.mainTexture as Texture2D;
    }

    public void SaveImage()
    {
        g.filename = g.pathClass + ".png";
        g.filespec = Path.Combine(g.pathImageData, g.pathClass, g.filename);
        g.bytes = g.textureGrabbed.EncodeToPNG();
        File.WriteAllBytes(g.filespec, g.bytes);
        Debug.Log("SaveImage: " + g.filespec + "\n");
    }

    public void SetClassification2()
    {
        //  6   7   8
        //  3   4   5
        //  0   1   2
        g.nClassification = -1;
        int s = (int)Mathf.Sqrt(g.numClasses);
        float ix = (int)(g.posNormal.x * s);
        float iy = (int)(g.posNormal.y * s);

        Debug.Log("ix,iy: " + ix + ", " + iy + " g.nClassification: " + g.nClassification + "\n");
    }

    public void SetClassification()
    {
        //  6   7   8
        //  3   4   5
        //  0   1   2
        g.nClassification = -1;
        if (g.posNormal.x < .33f)
        { 
            if (g.posNormal.y < .33f)
            {
                g.nClassification = 0;
            } else
            {
                if (g.posNormal.y < .67f)
                {
                    g.nClassification = 3;
                } else
                {
                    g.nClassification = 6;
                }
            }         
        } else
        {
            if (g.posNormal.x < .67f)
            {
                if (g.posNormal.y < .33f)
                {
                    g.nClassification = 1;
                    
                } else
                {
                    if (g.posNormal.y < .67f)
                    {
                        g.nClassification = 4;
                    } else
                    {
                        g.nClassification = 7;
                    }
                }         
            } else
            {
                if (g.posNormal.y < .33f)
                {
                    g.nClassification = 2;
                } else
                {
                    if (g.posNormal.y < .67f)
                    {
                        g.nClassification = 5;
                    } else
                    {
                        g.nClassification = 8;
                    }
                }         
            }
        }
        SetPathClass();
    }

    public void SetPathClass()
    {
        g.pathClass = "class_" + g.nClassification;
    }

    public Vector3 GetTargetPosition()
    {
        g.posScreen = GetPosScreen(g.target.transform.position);
        g.posNormal = Normalize(g.posScreen);

        string txt = PosToString(g.posNormal);
        SetInfo(txt);
        
        Vector3 posTarget = GetPosTarget(g.posScreen);
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
        // Debug.Log(txt + "\n");
    }

    string PosToString(Vector3 pos)
    {
        string f = "F2";
        string txt = "pos: " + pos.x.ToString(f) + ", " + pos.y.ToString(f) + ", " + pos.z.ToString(f);
        return txt;
    }
}
