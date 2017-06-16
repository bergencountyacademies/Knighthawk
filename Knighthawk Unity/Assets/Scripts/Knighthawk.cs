using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class Knighthawk : MonoBehaviour {

    public Arduino arduino;
    public int pin = 9;

    private int gameThreshold = 8;
    private int hardwareThreshold = 12;

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
        //keyControl();
        float hardwareAngle = Mathf.Atan((backTracker.transform.position.y - frontTracker.transform.position.y) / (.3f)) * Mathf.Rad2Deg; // .3 f is distance between actuators
        if (hardwareAngle > hardwareThreshold && hardwareAngle < -hardwareThreshold)
            arduino.analogWrite(pin, 1500);
        else
            angleControl(hardwareAngle);
    }

    void angleControl(float hardwareAngle)
    {
        float playerAngle = player.transform.rotation.x * Mathf.Rad2Deg;

        Debug.Log(playerAngle);

        if (playerAngle < -gameThreshold)
        {
            moveToLinear(hardwareAngle, -gameThreshold);
        }
        else if (playerAngle > gameThreshold)
        {
            moveToLinear(hardwareAngle, gameThreshold);
        }
        else
        {
            moveToLinear(hardwareAngle, playerAngle);
        }
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

    void moveToLinear(float from, float to)
    {
        float dif = Mathf.Abs(Mathf.Abs(from) - Mathf.Abs(to));
        int speed = 0;
        if (dif < 0.5f)
            speed = 0;
        else if (dif < 3)
            speed = 100;
        else if (dif > 20)
            speed = 300;
        else
            speed = 200;
        if (from < to)
        {
            arduino.analogWrite(pin, 1500 + speed);
        }
        else if (to < from)
        {
            arduino.analogWrite(pin, 1500 - speed);
        }
        else
        {
            arduino.analogWrite(pin, 1500);
        }
    }

    void keyControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            arduino.analogWrite(pin, 1700);
            Debug.Log("up");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            arduino.analogWrite(pin, 1300);
            Debug.Log("down");
        }
        else
        {
            arduino.analogWrite(pin, 1500);
            Debug.Log("stop");
        }
    }

    void ConfigurePins()
    {
        arduino.pinMode(pin, PinMode.SERVO);
    }

    /*void reset()
    {
        float hardwareAngle = Mathf.Atan((backTracker.transform.position.y - frontTracker.transform.position.y) / (.3f)) * Mathf.Rad2Deg;
        while (hardwareAngle > 0.5f || hardwareAngle < -0.5f)
        {
            hardwareAngle = Mathf.Atan((backTracker.transform.position.y - frontTracker.transform.position.y) / (.3f)) * Mathf.Rad2Deg;
            moveToLinear(hardwareAngle, 0);
        }
        arduino.analogWrite(pin, 1500);
    }*/

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            arduino.analogWrite(pin, 1500);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            arduino.analogWrite(pin, 1500);
    }

    private void OnApplicationQuit()
    {
        arduino.analogWrite(pin, 1500);
    }
}
