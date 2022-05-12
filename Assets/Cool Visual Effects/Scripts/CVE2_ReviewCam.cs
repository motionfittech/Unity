using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVE2_ReviewCam : MonoBehaviour
{
    public float sensitivity;

    public bool lockZRot = false;

    public bool lockMouse = false;
    private void Start()
    {
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private float yValue = 0.0f, zValue = 0.0f;
    private void Update()
    {
        yValue = transform.eulerAngles.y + sensitivity * Input.GetAxis("Mouse X");
        zValue = transform.eulerAngles.z + sensitivity * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(0,
            Mathf.Clamp(yValue > 180 ? yValue - 360 : yValue, -70, 70),
            !lockZRot ? Mathf.Clamp(zValue > 180 ? zValue - 360 : zValue, -40, 17) : 0.0f);
    }
}
