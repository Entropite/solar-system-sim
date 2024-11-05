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

    private Vector3 cameraVelocity = Vector3.zero;

    public AstroObject target;


    // Start is called before the first frame update
    void Start() { 
        // Disable XR controllers if not running in XR. If they are enabled, you can stil see the controller rays in desktop mode.
        if (!UnityEngine.XR.XRSettings.enabled)
        {
            Debug.Log("XR is disabled");
            GameObject.Find("Left Controller").SetActive(false);
            GameObject.Find("Right Controller").SetActive(false);
        }
    }

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

            cameraVelocity += Vector3.forward * forwardSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cameraVelocity += Vector3.back * forwardSpeed * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            cameraVelocity.z = 0;
        }


        if (Input.GetKey(KeyCode.A))
        {
            cameraVelocity += Vector3.left * sidewaysSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cameraVelocity += Vector3.right * sidewaysSpeed * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            cameraVelocity.x = 0;
        }
        

        transform.Translate(cameraVelocity);
    }

}
