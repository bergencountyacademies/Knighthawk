//
// Gargore AIRCRAFT TOOLKIT (bundle edition, version 0.83)
//

//
// IMPORTANT NOTICE - this file should not be directly edited, if you really need to
//                    modify it, it's better if you create a children class or subclass.
//                    If you need more info, please check the manual.

using UnityEngine;
using System.Collections;

public class GSLandingGearAux: MonoBehaviour {

	public enum ActivationCondition { never, ifUnity5, always }
	public ActivationCondition activationCondition = ActivationCondition.ifUnity5;
	public GameObject activationExclude = null;
	public GameObject activationPlaceHere = null;
	public Vector3 activationPlaceHereDelta = Vector3.zero;

	private Rigidbody parentRigidbody = null;
	private GAircraft parentGAircraft = null;
	public bool debugVerbose = true;

	public Vector3 projectorLocalDirection = -Vector3.up;

	public bool projectorDistanceIsRelative = true;
	private float projectorDistanceScale = 1f;
	public float projectorBaseDistance = 0.333333333f;
	public float projectorMaxDistance = 0.666666667f;

	public bool separatorForce = true;
	public float separatorMinForce = 0f;
	public float separatorMaxForce = 3f;
	public float separatorForceExponent = 1f;
	public float separatorMassExponent = 1f;

	private Vector3 velocity = Vector3.zero;
	private float velocityFilter = 0.3f;
	private float lastTime = 0f;
	private Vector3 lastPosition;

	public Vector3 spinLocalDirection = Vector3.right;

	public bool forwardForce = false;
	public float forwardFrictionMinMultiplier = 0f;
	public float forwardFrictionMaxMultiplier = 0f;
	public float forwardFrictionMassExponent = 1f;
	public float forwardFrictionMaxSpeed = 1f;
	public bool sidewaysForce = false;
	public float sidewaysFrictionMinMultiplier = 1f;
	public float sidewaysFrictionMaxMultiplier = 1f;
	public float sidewaysFrictionMassExponent = 1f;
	public float sidewaysFrictionMaxSpeed = 1f;

	public string gearsPivot = "gears";
	public float gearsTargetValue = 1f;
	private float gearsTargetValueThreshold = 0.1f;

	public GameObject debug = null;

	Rigidbody findRigidbody() {
		Transform transform = gameObject.transform;
		int maxLoops = 99;
		while (transform != null) {
			--maxLoops; if (maxLoops < 0) break;
			if (transform.gameObject.GetComponent<Rigidbody>()) {
				return transform.gameObject.GetComponent<Rigidbody>();
			}
			transform = transform.parent;
		}
		return null;
	}

	GAircraft findGAircraft() {
		Transform transform = gameObject.transform;
		int maxLoops = 99;
		while (transform != null) {
			--maxLoops; if (maxLoops < 0) break;
			if (transform.gameObject.GetComponent<GAircraft>()) {
				return transform.gameObject.GetComponent<GAircraft>();
			}
			transform = transform.parent;
		}
		return null;
	}

	void Start() {
		#if UNITY_5
		if ((activationCondition == ActivationCondition.always) || (activationCondition == ActivationCondition.ifUnity5)) {
			if (activationExclude != null) activationExclude.SetActive(false);
		} else {
			gameObject.SetActive(false);
			return;
		}
		#else
		if (activationCondition == ActivationCondition.always) {
			if (activationExclude != null) activationExclude.SetActive(false);
		} else {
			gameObject.SetActive(false);
			return;
		}
		#endif

		if (parentRigidbody == null) parentRigidbody = findRigidbody();
		if (parentGAircraft == null) parentGAircraft = findGAircraft();

		lastPosition = transform.position;
		lastTime = Time.time;

		if (projectorDistanceIsRelative) projectorDistanceScale = (gameObject.transform.lossyScale.x + gameObject.transform.lossyScale.y + gameObject.transform.lossyScale.z) / 3f;
		if (projectorDistanceIsRelative) projectorBaseDistance *= projectorDistanceScale;
		if (projectorDistanceIsRelative) projectorMaxDistance *= projectorDistanceScale;
	}

	void FixedUpdate() {
		float gearsPivotValue = GPivot.toTAxisValue(gearsPivot, parentGAircraft, "gears");
		bool gearsDown = Mathf.Abs(gearsPivotValue - gearsTargetValue) < gearsTargetValueThreshold;
		Debug.Log("gearsdown = " + (gearsDown ? "true" : "false") + "; gears = " + gearsPivotValue + "; gaircraft = " + parentGAircraft + ";");

		float currentTime = Time.time;
		Vector3 currentPosition = transform.position;
		Vector3 deltaPosition = currentPosition - lastPosition;
		Vector3 currentVelocity = (currentTime - lastTime > 0f) ? deltaPosition / (currentTime - lastTime) : velocity;
		velocity = velocity * (1f - velocityFilter) + velocityFilter * currentVelocity;
		lastTime = currentTime;
		lastPosition = currentPosition;

		Vector3 normalDirection = gameObject.transform.TransformDirection(projectorLocalDirection).normalized;
		float normalSpeed = Vector3.Dot(velocity, normalDirection);
		Vector3 normalVelocity = normalDirection * normalSpeed;

		Vector3 spinDirection = gameObject.transform.TransformDirection(spinLocalDirection).normalized;
		float sidewaysSpeed = Vector3.Dot(velocity, spinDirection);
		Vector3 sidewaysVelocity = spinDirection * sidewaysSpeed;

		Vector3 forwardVelocity = velocity - normalVelocity - sidewaysVelocity;
		float forwardSpeed = forwardVelocity.magnitude;

		Ray ray = new Ray(gameObject.transform.position + normalDirection * projectorBaseDistance, normalDirection);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, projectorMaxDistance - projectorBaseDistance)) {
			float distance = hit.distance;
			if (parentRigidbody != null) {
				Vector3 totalForce = Vector3.zero;
				float separatorDistance = distance / (projectorMaxDistance - projectorBaseDistance);
				if (debugVerbose) Debug.Log(gameObject.name + " BasicGears: separator 0-1 = " + separatorDistance);
				float separatorForceMix = Mathf.Pow(1f - separatorDistance, separatorForceExponent);
				if (separatorForce) {
					float separatorForceAmmount = separatorForceMix * (separatorMaxForce - separatorMinForce) + separatorMinForce;
					totalForce -= normalDirection * separatorForceAmmount * Mathf.Pow(parentRigidbody.mass, separatorMassExponent);
				}

				if (forwardSpeed > forwardFrictionMaxSpeed) forwardVelocity = forwardVelocity.normalized * forwardFrictionMaxSpeed;
				if (sidewaysSpeed > sidewaysFrictionMaxSpeed) sidewaysVelocity = sidewaysVelocity.normalized * sidewaysFrictionMaxSpeed;

				float forwardFrictionMultiplier = separatorForceMix * (forwardFrictionMaxMultiplier - forwardFrictionMinMultiplier) + forwardFrictionMinMultiplier;
				float sidewaysFrictionMultiplier = separatorForceMix * (sidewaysFrictionMaxMultiplier - sidewaysFrictionMinMultiplier) + sidewaysFrictionMinMultiplier;

				totalForce -= forwardVelocity * forwardFrictionMultiplier * Mathf.Pow(parentRigidbody.mass, forwardFrictionMassExponent);
				totalForce -= sidewaysVelocity * sidewaysFrictionMultiplier * Mathf.Pow(parentRigidbody.mass, sidewaysFrictionMassExponent);

				parentRigidbody.AddForceAtPosition(totalForce, gameObject.transform.position);
			}
		}

		if (activationPlaceHere != null) activationPlaceHere.transform.position = gameObject.transform.position + activationPlaceHereDelta;

		if (debug != null) debug.transform.position = gameObject.transform.position + spinDirection;
	}
}

