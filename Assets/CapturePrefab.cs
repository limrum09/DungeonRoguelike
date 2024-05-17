using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CapturePrefab : MonoBehaviour
{
    // Adjust the filename and path as needed
    public string screenShotName;
    private string screenshotFileName = "PrefabScreenshotHeads2.png";

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
        // Capture the screenshot
        ScreenCapture.CaptureScreenshot(screenshotFileName);

/*        string name = this.gameObject.name;

        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            Texture2D texture = spriteRenderer.sprite.texture;
            byte[] textureBytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/06.Image/"+ name + ".png", textureBytes);
        }
        else
        {
            Debug.Log("Fail");
            Debug.Log(spriteRenderer);
            Debug.Log(spriteRenderer.sprite);
        }

        Debug.Log(this.gameObject.name);*/
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
