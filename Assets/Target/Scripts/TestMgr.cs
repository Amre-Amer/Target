using UnityEngine;

public class TestMgr : MonoBehaviour
{
    public Camera cam;
    public GameObject target;
    public GameObject marker;
    public GameObject display;
    int width = 14;
    int height = 14;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posScreen = cam.WorldToScreenPoint(target.transform.position);
        // Debug.Log("posScreen: " + posScreen.ToString("F2"));
        float x = -.5f + posScreen.x / width;
        float y = -.5f + posScreen.y / height;
        float z = 0;
        Vector3 posMarker = display.transform.TransformPoint(x, y, z);
        marker.transform.position = posMarker;
    }
}
