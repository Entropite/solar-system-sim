using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimManager : MonoBehaviour
{
    public const float G = 6.67430E-11F;
    public float speedUp = 1.0f;

    public static SimManager manager {get; private set;}

    private List<AstroObject> astroObjects;

    private float totalTime = 0;
    
    public int startYear;
    public int startMonth;
    public int startDay;
    private System.DateTime date;
    public System.DateTime startDate;

    private Text dateText;
    private Slider dateSlider;



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

        dateText = GameObject.Find("Date").GetComponent<Text>();
        dateSlider = GameObject.Find("DateSlider").GetComponent<Slider>();

        startDate = new System.DateTime(startYear, startMonth, startDay, 0, 0, 0);
        date = new System.DateTime(startYear, startMonth, startDay, 0, 0, 0);

        astroObjects = new List<AstroObject>(FindObjectsOfType<AstroObject>());
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        dateText.text = date.ToString("HH:mm:ss yyyy-MM-dd G\\MT");
        foreach (AstroObject astro in astroObjects)
        {
            astro.UpdateOrbit(date.Subtract(startDate).TotalSeconds, Time.deltaTime * speedUp);
        }

        date = date.AddSeconds(speedUp * Time.deltaTime);
    }

    public void UpdateSpeedUp()
    {

        speedUp = 1 + dateSlider.value * 1000000;
    }


    public Vector3 GetGravity(AstroObject body1, AstroObject body2)
    {
        Vector3 dir = body1.transform.position - body2.transform.position;

        return dir.normalized * (float)(G * body1.mass * body2.mass) / (dir.magnitude * dir.magnitude);

    }
}
