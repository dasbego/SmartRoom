using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

static class helperClass
{
	//Horario de clases del salón
	private static Dictionary<String, bool> _horario = new Dictionary<String, bool>(){
		{"7:00-8:00", false},
		{"9:00-10:00", true},
		{"10:00-11:00", true},
		{"11:00-12:00", false},
		{"12:00-13:00", false},
		{"14:00-15:00", false},
		{"15:00-16:00", true},
		{"16:00-17:00", true},
		{"17:00-18:00", false},
		{"18:00-19:00", true},
		{"19:00-20:00", true}
	};
	
	/// <summary>
	/// Devuelve la cantidad de luz en luxes(int) dada la hora especificada
	/// </summary>
	/// <param name="dateTimeNow">String de hora en formato "HH:mm"</param>
	/// <returns></returns>
	public static int getLuxExt(String dateTimeNow)
	{
		System.Random tempAbs = new System.Random();
		String[] time = dateTimeNow.Split(':');
		int hr = -1;
		if (!Int32.TryParse(time[0], out hr))
			return -1;
		
		if (hr >= 7 && hr <= 8)
		{
			return tempAbs.Next(300, 500); // Salida o puesta de sol en un día despejado : 400 lux
		}
		
		if (hr >= 8 && hr <= 10)
		{
			return tempAbs.Next(400, 700); // Salida o puesta de sol en un día despejado : 400 lux
		}
		if (hr >= 11 && hr <= 12)
		{
			return tempAbs.Next(800, 1200);
		}
		if (hr >= 12 && hr <= 18)
			return tempAbs.Next(1300, 1800);
		
		if (hr >= 18 && hr <= 19)
			return tempAbs.Next(1000, 1600);
		
		if (hr >= 19 && hr <= 20)
			return tempAbs.Next(300, 500);

		return -1;
	}

	/// <summary>
	/// Devuelve la temperatura externa en grados centigrados
	/// </summary>
	/// <param name="dateTimeNow">String de hora en formato "HH:mm"</param>
	/// <param name="mes">1..12</param>
	/// <returns></returns>
	//info: http://es.wikipedia.org/wiki/Cuernavaca
	public static int getTempExt(String dateTimeNow, int mes)
	{
		System.Random tempAbs = new System.Random();
		String[] time = dateTimeNow.Split(':');
		int minTemp, maxTemp;
		if (mes >= 1 && mes <= 3)
		{
			minTemp = tempAbs.Next(10, 14);
			maxTemp = tempAbs.Next(27, 32);
		}
		else if (mes >= 4 && mes <= 10)
		{
			minTemp = tempAbs.Next(14, 17);
			maxTemp = tempAbs.Next(27, 36);
		}
		else
		{
			minTemp = tempAbs.Next(11, 13);
			maxTemp = tempAbs.Next(25, 27);
		}
		
		int hr =-1;
		if (!Int32.TryParse(time[0], out hr))
			return -1;
		
		if (hr >= 7 && hr <= 10)
		{
			return minTemp;
		}
		
		if (hr >= 11 && hr <= 16)
		{
			return tempAbs.Next(minTemp, maxTemp);
		}
		if (hr >= 17 && hr <= 20)
			return minTemp;
		
		return -1;
	}
	
	//llega la hora actual con formato HH:mm :  DateTime.Now.ToString("HH:mm") ó hora en formato HH:mm
	
	/// <summary>
	/// Devuelve la cantidad de gente que hay dentro del salón dependiendo del horario de clases
	/// </summary>
	/// <param name="dateTimeNow">String de hora en formato "HH:mm"</param>
	/// <returns></returns>
	public static int getPeopleCant( String dateTimeNow)
	{
		System.Random tempRand = new System.Random();
		if (schedulClass(_horario, dateTimeNow))
			return tempRand.Next(6, 20);
		else
			return tempRand.Next(0,2);
	}
	
	//Llega el horario declarado arriba y hora actual
	private static bool schedulClass(Dictionary<String,bool> schedule, String actualHour)
	{          
		foreach(KeyValuePair<String,bool> entry in schedule)
		{
			string[] times = entry.Key.ToString().Split('-');
			times[0] = times[0].Trim();
			times[1] = times[1].Trim();             
			int result1 = DateTime.Compare(Convert.ToDateTime(actualHour), Convert.ToDateTime(times[0]));
			int result2 = DateTime.Compare(Convert.ToDateTime(actualHour), Convert.ToDateTime(times[1]));
			if (result1 > 0 && result2 < 0)
			{
				if (entry.Value.Equals(true))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		return false;
	}
}

public class ControlDia : MonoBehaviour {

	public SmartAlgorithm script;

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
	DateTime lastTimeUpdated;
	TimeSpan deltaTime;
	int speed;

	//Gente
	int numeroGente;
	bool lecture;
	public GameObject People;

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
	public GameObject Ventana1;
	public GameObject Ventana2;

	// Use this for initialization
	void Start () {
		script = GetComponent<SmartAlgorithm> ();
		//Tiempo
		hora = new DateTime ();
		hora = hora.AddHours (6);
		hora = hora.AddMinutes (30);
		lastTimeUpdated = hora;
		deltaTime = new TimeSpan ();
		speed = 5;
		deltaTime = TimeSpan.FromSeconds(speed);
		updateData ();
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
		if (hora.Hour > 20 && hora.Minute > 30)
			hora =  hora.AddHours (10);
		timeText.text = hora.ToShortTimeString();

		if (hora.Subtract (lastTimeUpdated).Minutes >= 10) {
			updateData();
			lastTimeUpdated = hora;
		}

		//Gente
		peopleText.text = numeroGente.ToString();
		setPeople();

		//Luz
		//Externa
		lightIntensity = new Color (intensidadLuz/3200.0f,intensidadLuz/3200.0f,intensidadLuz/3200.0f,0);
		//lightIntensity = new Color (0.0f,0.0f,0.0f,0);
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
		internalLightIntensity = new Color (intensidadLuzInterna/50.0f,intensidadLuzInterna/50.0f,intensidadLuzInterna/50.0f,0);
		light1.light.color = internalLightIntensity;
		light2.light.color = internalLightIntensity;
		light3.light.color = internalLightIntensity;
		light4.light.color = internalLightIntensity;

		//Temperatura
		temperatureText.text = externalTemperature + "°C";
		if (internalTemperature > 0.0f) {
			AC1.transform.FindChild ("Particles").particleSystem.emissionRate = (30.0f - internalTemperature) * 20.0f;
			AC2.transform.FindChild ("Particles").particleSystem.emissionRate = (30.0f - internalTemperature) * 20.0f;
		}

		//Puertas
		if (puertasAbiertas) {
			Ventana1.SetActive(false);
			Ventana2.SetActive(false);
		} else {
			Ventana1.SetActive(true);
			Ventana2.SetActive(true);
		}
	}

	public void setPeople()
	{
		for (int i = 0; i < 32; i++) {
			if(i < numeroGente) People.transform.GetChild(i).gameObject.SetActive(true);
			else People.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void updateData()
	{
		string time = hora.Hour + ":" + hora.Minute;
		//Gente
		numeroGente = helperClass.getPeopleCant(time);
		if (numeroGente > 2)
			lecture = true;
		else
			lecture = false;
		//Luz
		intensidadLuz = helperClass.getLuxExt(time);
		SmartAlgorithm.Luz l = new SmartAlgorithm.Luz ();
		intensidadLuzInterna = float.Parse(l.Regulate(hora.Hour,intensidadLuz/18.0f));
		//Debug.Log (intensidadLuzInterna);
		//Temperatura
		externalTemperature = helperClass.getTempExt (time, 5);
		SmartAlgorithm.Temperature t = new SmartAlgorithm.Temperature ();
		internalTemperature = float.Parse(t.Regulate (externalTemperature,numeroGente));
		//Debug.Log (internalTemperature);
		//Puertas
		SmartAlgorithm.DoorWindow d = new SmartAlgorithm.DoorWindow ();
		float result = float.Parse(d.open_close(externalTemperature,internalTemperature));
		Debug.Log (result);
		if (result <= 5)
			puertasAbiertas = false;
		else
			puertasAbiertas = true;
	}
}
