using UnityEngine;
using System.Collections;

public class FanAnimation : MonoBehaviour {

	public int speed;
	GameObject fan;

	// Use this for initialization
	void Start () {
		fan = this.gameObject;
		fan.transform.localPosition = Vector3.zero;
		fan.transform.Translate (0, 0, -3.8f);
	}
	
	// Update is called once per frame
	void Update () {
		fan.transform.Rotate(0,0,speed);
	}
}
