using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroObject : MonoBehaviour
{

    // mass is denoted in kg
    public double mass = 5.9E24F;

    // rotation speed is in radians per second
    public double rotationSpeed = 0;

    // how eccentric the planet's orbit is
    // a circular orbit has an eccentricity of 0
    public double eccentricity = 0;

    // closest distance to orbited object
    public double periapsis = 0;

    // furthest distance to orbited object
    public double apoapsis = 0;

    
    public AstroObject orbit;

    // the last time the planet reached periapsis
    public int periapsisYear = 2023;
    public int periapsisMonth = 12;
    public int periapsisDay = 31;
    public int periapsisHour = 23;
    public int periapsisMinute = 59;
    public int periapsisSecond = 59;

    public float orbitScale;

    private double semiMajorAxis;

    private double orbitalPeriod;
    // orbital speed is in radians/s
    private double orbitalSpeed;

    // the starting ecliptic longitude angle
    private double startLongitude;

    private Vector3 centre;

    private System.DateTime periapsisDate;
    private SimManager manager;


    void Start()
    {

        InitPosition();
    }

    public void InitPosition()
    {
        manager = SimManager.manager;
        if (orbit == null)
            return;

        
        // Set up orbital period, speed and starting longitude
        semiMajorAxis = (periapsis + apoapsis) / 2;


        orbitalPeriod = 2 * Mathf.PI * Mathf.Sqrt((float)(Mathf.Pow((float)semiMajorAxis, 3) / (SimManager.G * (orbit.mass + mass))));
        orbitalSpeed = 2 * Mathf.PI / orbitalPeriod;

            
        periapsisDate = new System.DateTime(periapsisYear, periapsisMonth, periapsisDay, periapsisHour, periapsisMinute, periapsisSecond);
        startLongitude = orbitalSpeed * manager.startDate.Subtract(periapsisDate).TotalSeconds / (2 * Mathf.PI);
    }

    private Vector3 GetCentre()
    {
        return orbit.transform.position + orbitScale * Vector3.right * (float)(semiMajorAxis * eccentricity);
    }

    public void UpdateOrbit(double totalTime, double updateTime)
    {
        if (orbit == null)
            return;

        transform.Rotate(0, (float)(360 * rotationSpeed * updateTime / (2 * Mathf.PI)), 0);

        double angularRotation = startLongitude + orbitalSpeed * totalTime;

        double currentRadius = orbitScale * semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Mathf.Cos((float)angularRotation));
        transform.position = GetCentre() + new Vector3((float)(currentRadius * Mathf.Cos((float)angularRotation)), 0,
                                        (float)(currentRadius * Mathf.Sin((float)angularRotation)));
    }


}
