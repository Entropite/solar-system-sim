using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroObject : MonoBehaviour
{

    // mass is denoted in Earth masses (~10^24 kg)
    public float mass = 1F;

    
    public AstroObject orbit;

    public Vector3 velocity;
    public Vector3 gravity;

    private float speedUp;
    private SimManager manager;
    private List<AstroObject> satellites = new List<AstroObject>();


    void Start()
    {
        manager = SimManager.manager;
        speedUp = manager.speedUp;

        // calculate perpendicular velocity needed to counteract gravity and start an orbit
        if (orbit != null)
        {
            // Build satellite list
            orbit.AddSatellite(this);

            Vector3 gravForce = manager.GetGravity(this, orbit);
            Vector3 dir = transform.position - orbit.transform.position;
            velocity = Mathf.Sqrt(gravForce.magnitude * dir.magnitude) * Vector3.Cross(dir, Vector3.down).normalized;

            
        }

        StartCoroutine(UpdateSatelliteVelocities());
    }

    private IEnumerator UpdateSatelliteVelocities()
    {
        // must wait until all of the initial velocities have been calculated
        yield return new WaitForEndOfFrame();

        // satellites must be updated to account for a velocity change
        // at least one object must have no orbit
        if (orbit == null)
        {
            UpdateSatellites();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 acc = gravity / mass;
        velocity += acc * speedUp;
        transform.Translate(velocity * speedUp);

    }

    public void UpdateSatellites()
    {
        foreach (AstroObject sat in satellites)
        {
            Debug.Log(sat);
            sat.velocity += velocity;
            sat.UpdateSatellites();
        }
    }

    public void AddSatellite(AstroObject satellite)
    {
        satellites.Add(satellite);
    }


}
