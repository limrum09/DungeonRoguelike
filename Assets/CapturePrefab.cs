using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CapturePrefab : MonoBehaviour
{
    public Camera screenshotCamera;  // 스크린샷을 찍을 카메라
    public int resolutionMultiplier = 2;  // 해상도 배율
    // Adjust the filename and path as needed
    public string screenShotName;
    private string screenshotFileName;

    void Update()
    {
        screenshotFileName = screenShotName + ".png";
        if (Input.GetKeyDown(KeyCode.P))  // Change the key as needed
        {
            //ImageCapture();
            CaptureScreenshot();
        }
    }

    void CaptureScreenshot()
    {
        int resWidth = Screen.width * resolutionMultiplier;
        int resHeight = Screen.height * resolutionMultiplier;

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        screenshotCamera.targetTexture = rt;

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        screenshotCamera.Render();

        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        
        System.IO.File.WriteAllBytes(screenshotFileName, bytes);

        Debug.Log($"Screenshot saved: {screenshotFileName}");
    }

    void ImageCapture()
    {
        Texture2D prefabTexture = AssetPreview.GetAssetPreview(this.gameObject);
        if(prefabTexture != null)
        {
            Debug.Log("Capture");
            byte[] textureBytes = prefabTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/06.Image/" + name + ".png", textureBytes);
        }
        else
        {
            Debug.Log("Fail");
        }
        
    }
}
