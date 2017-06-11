using UnityEngine;
using System.Collections;

public class water1: MonoBehaviour {
	
	public Vector3 direction1 = Vector3.right;
	public float direction1random = 0.02f;
	public Vector3 direction1random_mul = 2f * Vector3.one;
	public Vector3 direction1random_ofs = -1f * Vector3.one;
	[System.NonSerializedAttribute]public Vector3 direction1result = Vector3.zero;
	public float direction1filter = 0.001f;
	public float direction1speed = 0.04f;
	public float direction1amplitude = 0.04f;
	public float direction1offset = 0.1f;
	public float direction1frequency = 3f;
	public Vector3 direction2 = Vector3.up;
	public float direction2random = 0.02f;
	public Vector3 direction2random_mul = 2f * Vector3.one;
	public Vector3 direction2random_ofs = -1f * Vector3.one;
	[System.NonSerializedAttribute]public Vector3 direction2result = Vector3.zero;
	public float direction2filter = 0.001f;
	public float direction2speed = 0.01f;
	public float direction2amplitude = 0.04f;
	public float direction2offset = 0.1f;
	public float direction2frequency = 2f;
	
	void Update() {
		if (gameObject.GetComponent<Renderer>() != null) {
			if (gameObject.GetComponent<Renderer>().material != null) {
				if (Random.value < direction1random) {
					direction1 = new Vector3(direction1random_mul.x * Random.value + direction1random_ofs.x, direction1random_mul.y * Random.value + direction1random_ofs.y, direction1random_mul.z * Random.value + direction1random_ofs.z);
					//Debug.Log("new direction1: " + direction1);
				}
				direction1result = direction1result * (1f - direction1filter) + direction1filter * direction1;

				if (Random.value < direction2random) {
					direction2 = new Vector3(direction2random_mul.x * Random.value + direction2random_ofs.x, direction2random_mul.y * Random.value + direction2random_ofs.y, direction2random_mul.z * Random.value + direction2random_ofs.z);
					//Debug.Log("new direction2: " + direction2);
				}
				direction2result = direction2result * (1f - direction2filter) + direction2filter * direction2;
				
				float amplitude1 = Mathf.Cos(Time.timeSinceLevelLoad * direction1frequency) * direction1amplitude + direction1offset;
				float amplitude2 = Mathf.Sin(Time.timeSinceLevelLoad * direction2frequency) * direction2amplitude + direction2offset;
				Vector2 offset1 = gameObject.GetComponent<Renderer>().material.GetTextureOffset("_BumpMap");
				Vector2 offset2 = gameObject.GetComponent<Renderer>().material.GetTextureOffset("_BumpMap2");
				//Debug.Log("offset1 = " + offset1 + "; offset2 = " + offset2 + ";");
				offset1 += ((Vector2)direction1result) * Time.deltaTime * direction1speed;
				offset2 += ((Vector2)direction2result) * Time.deltaTime * direction2speed;
				gameObject.GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", offset1);
				gameObject.GetComponent<Renderer>().material.SetTextureOffset("_BumpMap2", offset2);
				gameObject.GetComponent<Renderer>().material.SetFloat("_NormalMultiplier", amplitude1);
				gameObject.GetComponent<Renderer>().material.SetFloat("_NormalMultiplier2", amplitude2);
			}
		}
	}
}
