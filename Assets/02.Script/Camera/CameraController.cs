using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private List<CinemachineVirtualCamera> virtualCamearas;

    private CinemachinePOV composer;

    [Header("Camera Zoom Distance")]
    [SerializeField]
    [Range(5, 15)] private float minDistance;
    [SerializeField]
    [Range(16, 30)] private float maxDistance;


    private Transform target;

    public Camera CurrentCamera => camera;
    public void CameraStart()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        this.gameObject.transform.position = target.position;

        foreach(var virtualCameara in virtualCamearas)
        {
            virtualCameara.Follow = target;
            virtualCameara.LookAt = target;
        }

        composer = virtualCamearas[0].GetCinemachineComponent<CinemachinePOV>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(Manager.Instance.Key.GetKeyCode("Camera")))
        {
            ChangeCameraView();
        }

        if (Input.GetMouseButton(1))
        {
            CameraZoom();
            CameraMovement();
        }

    }

    private void CameraMovement()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightAlt))
        {
            float axisValueX = Input.GetAxis("Mouse X");
            float axisValueY = Input.GetAxis("Mouse Y");

            composer.m_HorizontalAxis.Value += axisValueX;
            composer.m_VerticalAxis.Value -= axisValueY;
        }
    }

    private void CameraZoom()
    {
        float scrollwheel = Input.GetAxis("Mouse ScrollWheel");

        if (virtualCamearas[0].gameObject.activeSelf && scrollwheel != 0)
        {
            CinemachineComponentBase componentBase = virtualCamearas[0].GetCinemachineComponent(CinemachineCore.Stage.Body);

            if (componentBase is CinemachineFramingTransposer Ftransposer)
            {
                float distance = Ftransposer.m_CameraDistance;

                distance = Mathf.Clamp(distance - scrollwheel * 10f, 8f, 40f);

                Ftransposer.m_CameraDistance = distance;
            }

        }
        else if (virtualCamearas[1].gameObject.activeSelf && scrollwheel != 0)
        {
            CinemachineComponentBase componentBase = virtualCamearas[1].GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is CinemachineTransposer transposer)
            {
                Vector3 currentOffset = transposer.m_FollowOffset;

                float y = Mathf.Clamp(currentOffset.y - scrollwheel * 10f, 5f, 30f);
                float z = Mathf.Clamp(currentOffset.z + scrollwheel * 10f, -29f, -4f);

                Vector3 newOffset = new Vector3(0, y, z);

                transposer.m_FollowOffset = newOffset;
            }
        }
    }

    public void ChangeCameraView()
    {
        int camearaCnt = virtualCamearas.Count;
        for(int i = 0; i < camearaCnt ; i++)
        {
            if (virtualCamearas[i].gameObject.activeSelf)
            {
                virtualCamearas[i++].gameObject.SetActive(false);

                if (i >= camearaCnt)
                    i = 0;

                virtualCamearas[i].gameObject.SetActive(true);

                break;
            }
        }
    }
}
