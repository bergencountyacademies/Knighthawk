//
// Gargore AIRCRAFT TOOLKIT (bundle edition, version 0.83)
//

//
// IMPORTANT NOTICE - this file should not be directly edited, if you really need to
//                    modify it, it's better if you create a children class or subclass.
//                    If you need more info, please check the manual.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]public class GSRotorBreakAux: MonoBehaviour {
	[HideInInspector]public Vector3 startLocalPosition = Vector3.zero;
	public GAircraft parentGAircraft = null;
	public string checkPivotId = "drive1";
	public float checkRpmThreshold = 200f;
	public bool debugMessagesEnabled = false;
	private string name1 = "";
	private string name2 = "";

	void Start() {
		startLocalPosition = gameObject.transform.localPosition;
		Transform transform = gameObject.transform;
		int maxLoops = 99;
		if (parentGAircraft == null) while (transform != null) {
			--maxLoops; if (maxLoops < 0) break;
			if (transform.gameObject.GetComponent("GAircraft")) {
				parentGAircraft = (GAircraft)transform.gameObject.GetComponent("GAircraft");
				break;
			}
			transform = transform.parent;
		}
	}

	void FixedUpdate() {
		gameObject.transform.localPosition = startLocalPosition;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	void OnCollisionEnter(Collision collision) {
		if ((parentGAircraft != null) && (Time.realtimeSinceStartup > parentGAircraft.ignoreCrashUntil)) {
			if (GPivot.getAnyPivot(checkPivotId + ".rpm") >= checkRpmThreshold) {
				parentGAircraft.isCrashed = true;
				name1 = collision.collider.gameObject.name;
				name2 = collision.rigidbody.gameObject.name;
				Debug.Log("Crash detected! (D) between \"" + name1 + "\" and \"" + name2 + "\"");
			}
		}
	}
}

