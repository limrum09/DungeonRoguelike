using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    private MeshRenderer rendererCheck;

    private void Awake()
    {
        rendererCheck = GetComponent<MeshRenderer>();
    }

    public void SetObjectTransparent(float mode, int _renderQueue, float _distnace, float maxDistance, float minDistance)
    {
        SetObjectTransparent(mode, _renderQueue);
        float distance = _distnace;
        if (distance < 15f)
        {
            Manager.Instance.TransParent.CheckTransParentCoroutine(this, distance, maxDistance, minDistance);
            this.gameObject.SetActive(false);
        }
    }

    // mode = 1 : 투명, 0 : 불투명
    public void SetObjectTransparent(float mode ,int _renderQueue)
    {
        foreach (Material material in rendererCheck.materials)
        {
            // 나뭇잎 일 경우, 안보이게 해야하는데, 불가능한가?
            if (material.name.Contains("Leaves"))
            {
                if(mode == 1f)
                {
                    material.SetInt("_ZTest", 7);
                    Debug.Log("나뭇잎 수정" + material.GetInt("_ZTest"));
                }
                else
                {
                    material.SetInt("_ZTest", 1);
                    Debug.Log("나뭇잎 복구" + material.GetInt("_ZTest"));
                }
            }
            else
            {
                // 투명화
                if(mode == 1f)
                {
                    material.SetFloat("_Mode", 1);
                    material.SetFloat("_Surface", 1); // Surface Type: Transparent
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    material.renderQueue = _renderQueue;
                }
                // 불투명화
                else
                {
                    material.SetFloat("_Mode", 0);
                    material.SetFloat("_Surface", 0); // Surface Type: Opaque
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    material.renderQueue = _renderQueue;
                }
                
            }
        }
    }
}
