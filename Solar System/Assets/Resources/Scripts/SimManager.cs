using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    public const float G = 6.67430E-11F;
    public float timeScale = 1F;

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

        Time.timeScale = timeScale;

        astroObjects = new List<AstroObject>(FindObjectsOfType<AstroObject>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        for (int i = 0; i < astroObjects.Count - 1; i++)
        {
            for (int j = i + 1; j < astroObjects.Count; j++) {
                AstroObject body1 = astroObjects[i];
                AstroObject body2 = astroObjects[j];
                Vector3 dir = body1.transform.position - body2.transform.position;

                float gravForce = (G * body1.mass * body2.mass) / (dir.magnitude * dir.magnitude);
                
                body1.AddForce(gravForce * -dir.normalized);
                body2.AddForce(gravForce * dir.normalized);
            }
        }
    }
}
