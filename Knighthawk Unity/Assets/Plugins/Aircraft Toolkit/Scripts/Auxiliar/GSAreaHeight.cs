//
// Gargore AIRCRAFT TOOLKIT (bundle edition, version 0.83)
//

//
// IMPORTANT NOTICE - this file should not be directly edited, if you really need to
//                    modify it, it's better if you create a children class or subclass.
//                    If you need more info, please check the manual.

using UnityEngine;
using System.Collections;

public class GSAreaHeight: MonoBehaviour {

	private float baseHeight = 0.0f;
	public float coefHeight = 0.01f;
	public GameObject viewer = null;
	private Vector3 position = Vector3.zero;

	void Start() {
		baseHeight = gameObject.transform.position.y;
	}

	void Update() {
		if (viewer != null) {
			position = gameObject.transform.position;
			if (viewer.transform.position.y > baseHeight) {
				position.y = baseHeight + (viewer.transform.position.y - baseHeight) * coefHeight;
			} else {
				position.y = baseHeight;
			}
			gameObject.transform.position = position;
		}
	}
}

