//
// Gargore AIRCRAFT TOOLKIT (bundle edition, version 0.83)
//

//
// IMPORTANT NOTICE - this file should not be directly edited, if you really need to
//                    modify it, it's better if you create a children class or subclass.
//                    If you need more info, please check the manual.

using UnityEngine;
using System.Collections;

public class GSParagliderAux: MonoBehaviour {
	public GameObject moveableMassBody = null;
	public Vector3 moveableExtrusion = Vector3.one;
	private Vector3 moveableMassBodyStartPosition = Vector3.zero;
	private GAircraft aircraft = null;

	void Start() {
		moveableMassBodyStartPosition = gameObject.transform.InverseTransformPoint(moveableMassBody.transform.position);
		if (gameObject.GetComponent("GAircraft") != null) aircraft = (GAircraft)gameObject.GetComponent("GAircraft");
	}

	Vector3 MultiplyVector3Components(Vector3 v1, Vector3 v2) {
		return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}

	void FixedUpdate() {
		//Vector3 down = moveableMassBody.transform.TransformPoint(-gameObject.transform.up);
		//moveableMassBody.transform.localPosition = moveableMassBodyStartPosition + down * 10.0f;
		//moveableMassBody.transform.position = gameObject.transform.TransformPoint(moveableMassBodyStartPosition) + gameObject.transform.TransformDirection(MultiplyVector3Components(gameObject.transform.InverseTransformDirection(-Vector3.up), moveableExtrusion));

		Vector3 gravitybalance = MultiplyVector3Components(gameObject.transform.InverseTransformDirection(-Vector3.up), moveableExtrusion);
		Vector3 gravitybalancexz = gravitybalance;
		gravitybalancexz.y = 0.0f;
		gravitybalance.y += (gravitybalancexz.magnitude * gravitybalancexz.magnitude) * 0.4f;
		Vector3 controls = Vector3.zero;
		if (aircraft != null) {
			controls.z = -(aircraft.inputElevator_output - 0.5f) * 4.0f;
			controls.x = (aircraft.inputAilerons_output - 0.5f) * 6.0f;
			controls.y = controls.magnitude * controls.magnitude * 0.2f;
		}
		Debug.Log(controls);
		moveableMassBody.transform.position = gameObject.transform.TransformPoint(moveableMassBodyStartPosition) + gameObject.transform.TransformDirection(gravitybalance + controls);
		moveableMassBody.transform.rotation.SetLookRotation(moveableMassBody.transform.position - moveableMassBodyStartPosition);
	}
}

