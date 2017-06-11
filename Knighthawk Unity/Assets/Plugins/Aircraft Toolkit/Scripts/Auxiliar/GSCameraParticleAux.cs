//
// Gargore AIRCRAFT TOOLKIT (bundle edition, version 0.83)
//

//
// IMPORTANT NOTICE - this file should not be directly edited, if you really need to
//                    modify it, it's better if you create a children class or subclass.
//                    If you need more info, please check the manual.

using UnityEngine;
using System.Collections;

public class GSCameraParticleAux: MonoBehaviour {

	public Camera followCamera = null;
	public float followCameraMoveVector = 10.0f;
	public float maxHeight = 2500.0f;
	void Start() {
		if (followCamera == null) {
			followCamera = Camera.main;
			followCameraLastPosition = followCamera.transform.position;
		}
	}

	private Vector3 followCameraLastPosition = Vector3.zero;
	private Vector3 deltaFiltered = Vector3.zero;
	void FixedUpdate() {
		deltaFiltered = deltaFiltered * 0.9f + (followCamera.transform.position - followCameraLastPosition) * 0.1f;
		followCameraLastPosition = followCamera.transform.position;
		if (followCameraLastPosition.y > maxHeight) followCameraLastPosition.y = maxHeight;
		gameObject.transform.position = followCameraLastPosition + deltaFiltered * 30.0f;
	}
}

