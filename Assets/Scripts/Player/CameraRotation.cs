using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private const int ROT_VERT_MIN_ANGLE = -90;
    private const int ROT_VERT_MAX_ANGLE = 90;

    bool isEnabled;

    private CharacterController controller;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float sensitityX = 2.5f;
    public float sensitityY = 2.0f;
    public Camera attachedCamera;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            yaw += sensitityX * Input.GetAxis("Mouse X");
            pitch -= sensitityY * Input.GetAxis("Mouse Y");

            // Limit vertical rotation
            pitch = Mathf.Clamp(pitch, ROT_VERT_MIN_ANGLE, ROT_VERT_MAX_ANGLE);

            controller.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
            attachedCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }
}
