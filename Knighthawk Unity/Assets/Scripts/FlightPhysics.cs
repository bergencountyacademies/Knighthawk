using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPhysics : MonoBehaviour {

    public GameObject controllerRight;
    public GameObject controllerLeft;
    public GameObject trackerFront;

    public GameObject wingRightFront;
    public GameObject wingLeftFront;

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        wingLeftFront.transform.eulerAngles = new Vector3(wingLeftFront.transform.eulerAngles.x, wingLeftFront.transform.eulerAngles.y, controllerLeft.transform.eulerAngles.z);
        wingRightFront.transform.eulerAngles = new Vector3(wingRightFront.transform.eulerAngles.x, wingRightFront.transform.eulerAngles.y, controllerRight.transform.eulerAngles.z);
        Debug.Log(trackerFront.transform.localPosition.y - .2f + ", " + controllerRight.transform.localPosition.y + ", " + controllerLeft.transform.localPosition.y);
        if ((trackerFront.transform.localPosition.y-.25f > controllerRight.transform.localPosition.y) && (trackerFront.transform.localPosition.y - .25f > controllerLeft.transform.localPosition.y))
        {
            Debug.Log("HHHHH");
            player.GetComponent<Rigidbody>().AddForce(transform.up * 1800f);
        }
    }
}
