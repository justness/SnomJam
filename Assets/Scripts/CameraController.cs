using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Transform t;
    float rotateSpeed = 20f;

    void Start()
    {
        t = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");
        player.transform.Rotate(-rotateVertical*rotateSpeed, rotateHorizontal*rotateSpeed, 0, Space.Self);

        t.forward = player.transform.forward;
        t.position = player.transform.position;
    }
}
