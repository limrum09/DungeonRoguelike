using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentInCameraView : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private LayerMask treeLayer;

    // 초기화 할때 List안에 있는 MeshRender를 호출한다. 그렇기에 투명화 할 시 List에 추가해야함.
    private List<TransparentObject> transparentObjects = new List<TransparentObject>();

    // Start is called before the first frame update
    public void TransParentInCameraStart(Transform _target)
    {
        target = _target;

        Manager.Instance.Game.playerMoveInGame += SetTransparencyObjects;
    }

    public void SetTransparencyObjects()
    {
        // 투명화 오브젝트 초기화
        for (int i = 0; i < transparentObjects.Count; i++)
        {
            if (transparentObjects[i] != null)
                transparentObjects[i].SetObjectTransparent(0f, 2000);
        }
        transparentObjects.Clear();

        // 카메라의 위치와 player의 높이에서 Ray시작
        Vector3 rayCheckPos = transform.position;
        rayCheckPos.y = target.position.y + 3.0f;

        // Raycast로 Player(target)를 카메라에서 찾아 거리 확인
        Ray ray = new Ray(rayCheckPos, target.position - rayCheckPos);
        float distance = Vector3.Distance(transform.position, target.position);

        // Raycast의 범위안에 TreeLayer들 가져오기
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, treeLayer);

        // 오브젝트 투명화
        foreach(var hit in hits)
        {
            // Ray에 걸린 오브젝트가 TransparentObject Script를 가지고 있는지 확인
            TransparentObject tpObject = hit.collider.GetComponentInParent<TransparentObject>();

            if (tpObject != null)
            {
                // TransparentObject를 가지고 있다면, 현제 카메라까지의 거리 확인
                float targetDistance = Vector3.Distance(transform.position, hit.transform.position);
                Debug.Log("Render 확인!");
                // 투명화
                tpObject.SetObjectTransparent(1f, 3000, targetDistance, 20f, 5f);
                // 투명화를 풀기위해 List에 추가
                transparentObjects.Add(tpObject);
            }
        }
    }

    private void OnDestroy()
    {
        Manager.Instance.Game.playerMoveInGame -= SetTransparencyObjects;
    }
}
