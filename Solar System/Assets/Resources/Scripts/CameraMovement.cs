using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //rotation speeds
    public float horizontalMouseSensitivity = 2.0f;
    public float verticalMouseSensitivity = 2.0f;
    public float forwardSpeed = 0.001f;
    public float sidewaysSpeed = 0.001f;


    //current horizontal and vertical rotations
    private float horizontalRotation = 0.0f;
    private float verticalRotation = 0.0f;

    private bool keyW = false;
    private bool keyA = false;
    private bool keyS = false;
    private bool keyD = false;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        //rotating with mouse
        horizontalRotation += horizontalMouseSensitivity * Input.GetAxis("Mouse X");
        verticalRotation -= verticalMouseSensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);

        //moving with wasd

        //detect keypresses/releases
        if (Input.GetKeyDown(KeyCode.W))
        {
            keyW = true;
            Debug.Log("w unpress");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            keyW = false;
            Debug.Log("w press");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyA = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            keyA = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            keyS = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            keyS = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            keyD = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            keyD = false;
        }


        //move based on pressed keys
        if (keyW)
        {
            transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
        }
        if (keyS)
        {
            transform.Translate(0, 0, -forwardSpeed * Time.deltaTime);
        }
        if (keyA)
        {
            transform.Translate(-sidewaysSpeed * Time.deltaTime, 0, 0);
        }
        if (keyD)
        {
            transform.Translate(sidewaysSpeed * Time.deltaTime, 0, 0);
        }


    }
}
