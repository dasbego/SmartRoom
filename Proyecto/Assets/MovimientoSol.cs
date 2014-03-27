using UnityEngine;
using System.Collections;

public class MovimientoSol : MonoBehaviour {

	GameObject sun;
	public Vector3 center;
	Vector3 position;
	public float radius;
	public float timeScale;
	GameObject sunLight;
	float angle;

	// Use this for initialization
	void Start () {
		sun = this.gameObject;
		position = center;
		angle = 0;
		sunLight = this.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (angle >= 2*Mathf.PI)
			angle = 0;
		angle += Time.deltaTime * timeScale;
		position.x = center.x + Mathf.Cos (angle) * radius;
		position.y = center.y + Mathf.Sin (angle) * radius;
		position.z = center.z;
		sun.transform.position = position;
		if (angle > Mathf.PI)
			sunLight.light.enabled = false;
		else
			sunLight.light.enabled = true;

	}
}
