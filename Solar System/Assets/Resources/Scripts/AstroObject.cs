using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroObject : MonoBehaviour
{
    private Rigidbody rb;

    // mass is denoted in kg
    [SerializeField]
    public float mass = 1F;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddForce(Vector3 forceVec)
    {
        rb.AddForce(forceVec, ForceMode.Force);
    }
}
