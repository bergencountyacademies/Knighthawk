using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class Knighthawk : MonoBehaviour {

    public Arduino arduino;
    public int pin = 9;

    public GameObject player;
    public GameObject frontTracker;
    public GameObject backTracker;

	// Use this for initialization
	void Start () {
        //arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }
	
	// Update is called once per frame
	void Update () {
        float playerAngle = player.transform.rotation.x*Mathf.Rad2Deg;
        float hardwareAngle = Mathf.Atan((backTracker.transform.position.y - frontTracker.transform.position.y) / (.3f))*Mathf.Rad2Deg; // .3 f is distance between actuators

        //Debug.Log(hardwareAngle);

        if (playerAngle < -20)
        {
            moveTo(hardwareAngle, -20);
        } else if (playerAngle > 20)
        {
            moveTo(hardwareAngle, 20);
        } else
        {
            moveTo(hardwareAngle, playerAngle);
        }
        
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            arduino.analogWrite(pin, 1700);
            Debug.Log("up");
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            arduino.analogWrite(pin, 1300);
            Debug.Log("down");
        } else
        {
            arduino.analogWrite(pin, 1500);
            Debug.Log("stop");
        }*/
    }

    void moveTo(float from, float to)
    {
        float dif = Mathf.Abs(Mathf.Abs(from) - Mathf.Abs(to));
        float expScale = Mathf.Exp(dif / 40);
        int f = (int)((expScale - 1) * 200);
        if (from < to)
        {
            arduino.analogWrite(pin, 1500 + f);
        } else if (to < from)
        {
            arduino.analogWrite(pin, 1500 - f);
        } else
        {
            arduino.analogWrite(pin, 1500);
        }
        Debug.Log(from + ", " + to + ", " + f); // if at 40 expScale should be 2, if at 0 expScale should be 1
        
    }

    void ConfigurePins()
    {
        arduino.pinMode(pin, PinMode.SERVO);
    }
}
