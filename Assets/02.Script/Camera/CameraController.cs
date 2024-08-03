using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private List<Camera> cameras;

    private Transform target;
    public void CameraStart()
    {
        target = GameObject.FindGameObjectWithTag("PlayerComponent").transform;

        this.gameObject.transform.position = target.position;
    }

    public void ChangeCameraView()
    {
        int camearaCnt = cameras.Count;
        for(int i = 0; i < camearaCnt ; i++)
        {
            if (cameras[i].gameObject.activeSelf)
            {
                cameras[i++].gameObject.SetActive(false);

                if (i >= camearaCnt)
                    i = 0;

                cameras[i].gameObject.SetActive(true);

                break;
            }
        }
    }
}
