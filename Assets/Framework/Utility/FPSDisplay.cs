///This script has been taken from : http://wiki.unity3d.com/index.php?title=FramesPerSecond
///Author: Dave Hampson 

using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    public Color color;
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = color;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = Time.timeScale == 0 ? "Pause" : string.Format("{0:0.0} ms / {1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}