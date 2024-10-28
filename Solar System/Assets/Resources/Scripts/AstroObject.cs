using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroObject : MonoBehaviour
{
    private Rigidbody rb;

    // mass is denoted in kg
    public float mass = 1F;

    
    public AstroObject orbit;

    public Vector3 velocity;
    public Vector3 gravity;

    private float speedUp;
    private SimManager manager;



    // Start is called before the first frame update
    void Start()
    {
        manager = SimManager.manager;
        speedUp = manager.speedUp;

        if (orbit != null)
        {
            Vector3 gravForce = manager.GetGravity(this, orbit);
            Vector3 dir = transform.position - orbit.transform.position;
            velocity = Mathf.Sqrt(gravForce.magnitude * dir.magnitude) * Vector3.Cross(dir, Vector3.up).normalized;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 acc = gravity / mass;
        velocity += acc * speedUp;
        transform.Translate(velocity * speedUp);

    }


}
