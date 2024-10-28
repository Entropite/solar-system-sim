using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    public const float G = 6.67430E-11F;
    public float speedUp = 1.0f;

    public static SimManager manager {get; private set;}

    private List<AstroObject> astroObjects;


    void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }


        astroObjects = new List<AstroObject>(FindObjectsOfType<AstroObject>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        Vector3[] gravForces = new Vector3[astroObjects.Count];
        for (int i = 0; i < astroObjects.Count - 1; i++)
        {
            for (int j = i + 1; j < astroObjects.Count; j++) {
                AstroObject body1 = astroObjects[i];
                AstroObject body2 = astroObjects[j];
                Vector3 gravForce = GetGravity(body1, body2);
                gravForces[i] += -gravForce;
                gravForces[j] += gravForce;
            }
        }

        for(int i = 0; i < astroObjects.Count; i++)
        {
            astroObjects[i].gravity = gravForces[i];
        }
    }

    public Vector3 GetGravity(AstroObject body1, AstroObject body2)
    {
        Vector3 dir = body1.transform.position - body2.transform.position;

        return dir.normalized * (G * body1.mass * body2.mass) / (dir.magnitude * dir.magnitude);

    }
}
