using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount;
    public float maxSwayAmount;
    public float smoothAmount;
    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxis("Mouse X") * swayAmount;
        float movementY = Input.GetAxis("Mouse Y") * swayAmount;

        movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

        Vector3 finalPosition = new Vector3(-movementX, -movementY, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initPosition, Time.deltaTime * smoothAmount);
    }
}
