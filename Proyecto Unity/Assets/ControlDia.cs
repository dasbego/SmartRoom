using UnityEngine;
using System.Collections;
using System;

public class ControlDia : MonoBehaviour {

	public GUIText timeText;
	public GUIText temperatureText;
	public GUIText lightText;
	public GUIText peopleText;
	
	public Material night;
	public Material morning;
	public Material day;
	public Material afternoon;

	//Tiempo
	DateTime hora;
	TimeSpan deltaTime;
	int speed;

	//Gente
	int numeroGente;

	//Luz
	float intensidadLuz;//0-100
	float intensidadLuzInterna;//0-100
	Color lightIntensity;
	Color internalLightIntensity;
	public GameObject light1;
	public GameObject light2;
	public GameObject light3;
	public GameObject light4;

	//Temperatura
	float internalTemperature;
	float externalTemperature;
	public GameObject AC1;
	public GameObject AC2;

	//Puertas
	bool puertasAbiertas;

	// Use this for initialization
	void Start () {
		//Tiempo
		hora = new DateTime ();
		deltaTime = new TimeSpan ();
		speed = 5;
		deltaTime = TimeSpan.FromSeconds(speed);
		//Gente
		//Luz
		intensidadLuz = 80.0f;
		intensidadLuzInterna = 80.0f;
		RenderSettings.skybox = night;
		//Temperatura
		internalTemperature = 12.0f;
		externalTemperature = 30.0f;

		//Puertas
	}
	
	// Update is called once per frame
	void Update () {
		//Tiempo
		if (Input.GetKeyDown(KeyCode.X)) {
			speed+=5;
		}
		if (Input.GetKeyDown(KeyCode.Z) && speed >= 10) {
			speed-=5;
		}
		deltaTime = TimeSpan.FromSeconds (speed);
		hora = hora + deltaTime;
		timeText.text = hora.ToShortTimeString();

		//Gente

		//Luz
		//Externa
		lightIntensity = new Color (intensidadLuz/200.0f,intensidadLuz/200.0f,intensidadLuz/200.0f,0);
		RenderSettings.ambientLight = lightIntensity;
		if (hora.Hour < 7) {
			RenderSettings.skybox = night;
		} else if (hora.Hour < 16) {
			RenderSettings.skybox = day;
		} else if (hora.Hour < 20) {
			RenderSettings.skybox = afternoon;
		}else{
			RenderSettings.skybox = night;
		}
		lightText.text = intensidadLuz + " lux";

		//Interna
		internalLightIntensity = new Color (intensidadLuzInterna/100.0f,intensidadLuzInterna/100.0f,intensidadLuzInterna/100.0f,0);
		light1.light.color = internalLightIntensity;
		light2.light.color = internalLightIntensity;
		light3.light.color = internalLightIntensity;
		light4.light.color = internalLightIntensity;

		//Temperatura
		temperatureText.text = externalTemperature + "°C";
		if (internalTemperature > 0.0f) {
			AC1.transform.FindChild ("Particles").particleSystem.emissionRate = (30.0f - internalTemperature) * 15.0f;
			AC2.transform.FindChild ("Particles").particleSystem.emissionRate = (30.0f - internalTemperature) * 15.0f;
		}

		//Puertas

	}
}
