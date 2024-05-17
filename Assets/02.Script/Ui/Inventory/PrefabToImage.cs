using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PrefabToImage : MonoBehaviour
{
    public GameObject prefab;
    private Image image;
    private Sprite prefabSprite;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        GetImage();
    }
    private void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    private void GetImage()
    {
        if(prefab != null)
        {
            Texture2D prefabTexture = AssetPreview.GetAssetPreview(prefab);
            if(prefabTexture != null)
            {
                prefabSprite = Sprite.Create(prefabTexture, new Rect(0, 0, prefabTexture.width, prefabTexture.height), new Vector2(0.5f, 0.5f));
                SetImage(prefabSprite);
            }
            else
            {
                Debug.Log("PrefabTexture NULL");
            }
        }
        else
        {
            Debug.Log("Prefab NULL");
        }
    }
}
