using UnityEngine;

public class ScreenshotScript : MonoBehaviour
{
    public void TakeScreenshot()
    {
        string time = System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
        string name = "IMG-" + time + ".png";
        ScreenCapture.CaptureScreenshot(name);
    }
}
