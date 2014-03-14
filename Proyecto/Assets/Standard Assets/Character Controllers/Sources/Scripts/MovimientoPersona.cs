using UnityEngine;
using System.Collections;

public class MovimientoPersona : MonoBehaviour {

	public GameObject[] positions;
	
	Time t;
	public float timeSit;
	public float timeScale;

	int currentObject;
	int nextObject;

	bool direction; //Va entrando o va saliendo
	bool moving; //Indica si esta sentado

	GameObject person;
	Vector3 position;

	// Use this for initialization
	void Start () {
		person = this.gameObject;
		position.y = person.transform.position.y;
		nextObject = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (decimal.Round((decimal)person.transform.position.x, 2) == decimal.Round((decimal)positions[nextObject].transform.position.x, 2)
		    && decimal.Round((decimal)person.transform.position.z, 2) == decimal.Round((decimal)positions[nextObject].transform.position.z, 2)) {
			nextObject++;
		} else {
			position.x = interpolate(person.transform.position.x, positions [nextObject].transform.position.x, timeScale);
			position.z = interpolate(person.transform.position.z, positions [nextObject].transform.position.z, timeScale);
			person.transform.position = position;
		}
	}

	float interpolate(float a,float b,float mu)
	{
		double mu2;
		mu2 = (1- Mathf.Cos((float)mu*Mathf.PI))/2;
		return (float)(a*(1-mu2)+b*mu2);
	}
}
