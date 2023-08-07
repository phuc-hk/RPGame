using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float cameraRotationSpeed = 2f;
    [SerializeField] Transform player;

    private Vector3 oldMousePosition;

    void Start()
    {
        //virtualCamera = GameObject.FindGameObjectWithTag("FollowCamera").GetComponent<CinemachineVirtualCamera>();
    }


    void Update()
    {
        transform.position = player.position;

        if (Input.GetMouseButtonDown(1))
        {
            oldMousePosition = Input.mousePosition;
            return;
        }


        if (Input.GetMouseButton(1))
        {

            Vector3 currentMousePosition = Input.mousePosition;

            if (currentMousePosition.x < oldMousePosition.x)
            {
                float x = transform.eulerAngles.x;
                float y = transform.eulerAngles.y;
                transform.eulerAngles = new Vector3(x, y + cameraRotationSpeed);
            }

            if (currentMousePosition.x > oldMousePosition.x)
            {
                float x = transform.eulerAngles.x;
                float y = transform.eulerAngles.y;
                transform.eulerAngles = new Vector3(x, y - cameraRotationSpeed);
            }

        }

    }
}
