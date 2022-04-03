using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float rotateSpeed = 5;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");
        
        transform.Rotate(-rotateVertical * rotateSpeed, 0, 0, Space.Self);
        player.transform.Rotate(0, rotateHorizontal * rotateSpeed, 0, Space.Self);
    }
}
