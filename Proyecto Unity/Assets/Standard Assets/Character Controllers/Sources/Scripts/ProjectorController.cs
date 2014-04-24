using UnityEngine;
using System.Collections;

public class ProjectorController : MonoBehaviour {

	public Light projector;
	bool inRange;

	// Use this for initialization
	void Start () {
		inRange = false;
		Debug.Log ("Start");
	}
	
	// Update is called once per frame
	void Update () {
		if (inRange && Input.GetKey (KeyCode.E)) 
		{
			projector.light.enabled = !projector.light.enabled;
		}
	}

	void onTriggerEnter(Collider other)
	{
		inRange = true;
		Debug.Log ("In Range");
	}

	void onTriggerExit(Collider other)
	{
		inRange = false;
	}
}
