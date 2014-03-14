using UnityEngine;
using System.Collections;

public class MovimientoPersona : MonoBehaviour {

	public GameObject[] positions;
	public float timeSit;
	Time t;
	int currentObject;
	int nextObject;
	bool direction; //Va entrando o va saliendo
	bool moving; //Indica si esta sentado

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*if (moving && this.gameObject.transform.position == gameObject[nextObject].transform.position && nextObject == positions.GetLength-1) {//Llegamos al ultimo punto
			moving = false;
			direction = 1;
		}
		*/
	}
}
