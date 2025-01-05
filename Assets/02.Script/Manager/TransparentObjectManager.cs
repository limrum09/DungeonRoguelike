using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjectManager : MonoBehaviour
{

    public void CheckTransParentCoroutine(TransparentObject targetObject, float currentDistance, float maxDistance, float minDistance)
    {
        StartCoroutine(CheckActiveObjectToCameraDistance(targetObject, currentDistance, maxDistance, minDistance));
    }

    // ray와 상관없이 카메라의 특정 범위에서 벗어나면 오브젝트 활성화
    private IEnumerator CheckActiveObjectToCameraDistance(TransparentObject targetObject, float currentDistance, float maxDistance, float minDistance)
    {
        GameObject cameraRay = GameObject.FindGameObjectWithTag("CameraRayPos");

        // 카메라의 Ray시작 위치가 확인되지 않을 경우, 종료
        if (cameraRay == null)
        {
            Debug.LogError("Tag:CameraRayPos가 없음");
            yield break;
        }

        // 현제 거리
        float distance = currentDistance;

        while (distance <= maxDistance && distance > minDistance)
        {
            // 카메라와 Object간의 거리 계산
            distance = Vector3.Distance(targetObject.transform.position, cameraRay.transform.position);
            yield return null;
        }

        targetObject.gameObject.SetActive(true);
    }
}
