using UnityEngine;
using System.Collections;

public class CambioCamara : MonoBehaviour {

	public GameObject firstPersonController;
	public GameObject camera;
	public GameObject overCamera;
	bool firstPerson;

	// Use this for initialization
	void Start () {
		//firstPersonController = this.transform.FindChild("First Person Controller").gameObject;
		//camera = firstPersonController.transform.FindChild("Main Camera").gameObject;
		//overCamera = this.transform.FindChild("Camera").gameObject;
		firstPerson = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) 
		{
			firstPerson = !firstPerson;
			if(firstPerson)
			{
				firstPersonController.SetActive(true);
				camera.SetActive(true);
			}
			else
			{
				firstPersonController.SetActive(false);
				camera.SetActive(false);
			}
		}
		if (!firstPerson && Input.GetKey(KeyCode.W) && overCamera.transform.position.z < 1) {
			overCamera.transform.Translate(0,1.0f,0);
		}
		if (!firstPerson && Input.GetKey(KeyCode.S) && overCamera.transform.position.z > -9) {
			overCamera.transform.Translate(0,-1.0f,0);
		}
	}
}
