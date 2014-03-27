using UnityEngine;
using System.Collections;

public class MovimientoPersona : MonoBehaviour {

	public GameObject[] positions;
	
	Time t;
	public float timeSit;
	float actualTimeSit;
	public float timeScale;

	int currentObject;
	int nextObject;

	int direction; //Indica la direccion que se mueve, 1 hacia adentro, 0 no se mueve y -1 hacia afuera

	GameObject person;
	Vector3 position;

	// Use this for initialization
	void Start () {
		person = this.gameObject;
		position.y = person.transform.position.y;
		nextObject = 0;
		actualTimeSit = 0;
		direction = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (decimal.Round((decimal)person.transform.position.x, 1) == decimal.Round((decimal)positions[nextObject].transform.position.x, 1)
		    && decimal.Round((decimal)person.transform.position.z, 1) == decimal.Round((decimal)positions[nextObject].transform.position.z, 1)) {
			if(direction == 0)
			{
				if(actualTimeSit < timeSit) actualTimeSit = interpolate(actualTimeSit,timeSit,timeScale);
				else
				{
					direction = -1;
				}
			}
			if(nextObject == positions.Length-1 && direction == 1){//Ultimo lugar
				direction = 0;
			}
			if(nextObject == 0 && direction == -1){//Primer lugar
				Destroy(this.transform.gameObject);
			}
			nextObject+=direction;
		} else {
			position.x = interpolate(person.transform.position.x, positions [nextObject].transform.position.x, timeScale);
			position.z = interpolate(person.transform.position.z, positions [nextObject].transform.position.z, timeScale);
			person.transform.position = position;
		}
	}

	float interpolate(float a,float b,float mu)
	{
		double mu2 = mu;
		//mu2 = (1- Mathf.Cos((float)mu*Mathf.PI))/2;
		double result;
		if (a > b) {
			result = a - mu;
			if (result < b)result = b;
		}
		else if (b > a){
			result = a + mu;
			if(result > b) result = b;
		}
		else result = b;
		//result = (float)(a*(1-mu2)+b*mu2);
		Debug.Log ("A :" + a + " B:" + b + " Result:" + result);
	   	return (float)result;
	}
}
