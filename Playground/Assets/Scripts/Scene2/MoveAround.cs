using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{

    public float speed, lookSensitivity, maxLook;
    public GameObject cam;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * lookSensitivity, 0));
        Debug.Log(Mathf.Abs(cam.transform.rotation.x));
        cam.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * lookSensitivity, 0, 0));
        if (!(Mathf.Abs(cam.transform.rotation.x) <= maxLook))
        {
            cam.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * lookSensitivity, 0, 0));
        }
    }
}