using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //rotation speeds
    public float horizontalMouseSensitivity = 2.0f;
    public float verticalMouseSensitivity = 2.0f;
    public float forwardSpeed = 10.0f;
    public float sidewaysSpeed = 10.0f;


    //current horizontal and vertical rotations
    private float horizontalRotation = 0.0f;
    private float verticalRotation = 0.0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        //rotating with mouse
        horizontalRotation += horizontalMouseSensitivity * Input.GetAxis("Mouse X");
        verticalRotation -= verticalMouseSensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);

        //moving with wasd

        //detect keypresses/releases
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * sidewaysSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * forwardSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * sidewaysSpeed * Time.deltaTime);
        }
    }
}
