using UnityEngine;
using System.Collections;
using AI.Fuzzy.Library;
using System.Collections.Generic;
using System;

public class SmartAlgorithm : MonoBehaviour {
	
	public class Luz
	{
		public MamdaniFuzzySystem _lightControl = null;
		
		private MamdaniFuzzySystem CreateSystem()
		{
			//Create empty Mamdani Fuzzy System
			MamdaniFuzzySystem intensidadInt = new MamdaniFuzzySystem();
			
			//Create Fuzzy variables for the system
			FuzzyVariable hora = new FuzzyVariable("hora", 0, 15);
			hora.Terms.Add(new FuzzyTerm("Manana", new TriangularMembershipFunction(1.0, 1.0, 7.0)));
			hora.Terms.Add(new FuzzyTerm("Tarde", new TriangularMembershipFunction(5.0, 8.0, 14.0)));
			hora.Terms.Add(new FuzzyTerm("Noche", new TriangularMembershipFunction(10.0, 14.0, 15.0)));
			intensidadInt.Input.Add(hora);
			
			//Create Fuzzy variables for the system
			FuzzyVariable luxExt = new FuzzyVariable("luzExt", 0, 100);
			luxExt.Terms.Add(new FuzzyTerm("Debil", new TriangularMembershipFunction(0.0, 0.0, 50.0)));
			luxExt.Terms.Add(new FuzzyTerm("Media", new TriangularMembershipFunction(0.0, 50.0, 100.0)));
			luxExt.Terms.Add(new FuzzyTerm("Fuerte", new TriangularMembershipFunction(0.0, 100.0, 100.0)));
			intensidadInt.Input.Add(luxExt);
			
			//Create variable for output
			FuzzyVariable luzInt = new FuzzyVariable("luzInt", 0.0, 100.0);
			luzInt.Terms.Add(new FuzzyTerm("Apagada", new  TrapezoidMembershipFunction(0.0, 0.0, 20.0, 20.0)));
			luzInt.Terms.Add(new FuzzyTerm("Debil", new TriangularMembershipFunction(20.0, 20.0, 50.0)));
			luzInt.Terms.Add(new FuzzyTerm("Media", new TriangularMembershipFunction(20.0, 50.0, 100.0)));
			luzInt.Terms.Add(new FuzzyTerm("Fuerte", new TriangularMembershipFunction(50.0, 100.0, 100.0)));
			intensidadInt.Output.Add(luzInt);
			
			//Create fuzzy rules
			try
			{
				//para abrirlas
				MamdaniFuzzyRule rule1 = intensidadInt.ParseRule("if (hora is Manana) and (luzExt is Debil) then luzInt is Fuerte");
				MamdaniFuzzyRule rule2 = intensidadInt.ParseRule("if (hora is Tarde) and (luzExt is Debil) then luzInt is Fuerte");
				MamdaniFuzzyRule rule3 = intensidadInt.ParseRule("if (hora is Noche) and (luzExt is Debil) then luzInt is Fuerte");
				MamdaniFuzzyRule rule4 = intensidadInt.ParseRule("if (hora is Manana) and (luzExt is Media) then luzInt is Media");
				MamdaniFuzzyRule rule5 = intensidadInt.ParseRule("if (hora is Tarde) and (luzExt is Media) then luzInt is Debil");
				MamdaniFuzzyRule rule6 = intensidadInt.ParseRule("if (hora is Noche) and (luzExt is Media) then luzInt is Media");
				MamdaniFuzzyRule rule7 = intensidadInt.ParseRule("if (hora is Manana) and (luzExt is Fuerte) then luzInt is Apagada");
				MamdaniFuzzyRule rule8 = intensidadInt.ParseRule("if (hora is Tarde) and (luzExt is Fuerte) then luzInt is Apagada");
				MamdaniFuzzyRule rule9 = intensidadInt.ParseRule("if (hora is Noche) and (luzExt is Fuerte) then luzInt is Apagada");
				
				intensidadInt.Rules.Add(rule1);
				intensidadInt.Rules.Add(rule2);
				intensidadInt.Rules.Add(rule3);
				intensidadInt.Rules.Add(rule4);
				intensidadInt.Rules.Add(rule5);
				intensidadInt.Rules.Add(rule6);
				intensidadInt.Rules.Add(rule7);
				intensidadInt.Rules.Add(rule8);
				intensidadInt.Rules.Add(rule9);
			}
			catch (Exception ex)
			{
				Console.WriteLine("No se pudo crear el sistema");
				Console.WriteLine(string.Format("Parsing exception: {0}", ex.Message));
			}
			return intensidadInt;
		}
		
		public string Regulate(float hora, float luzExt )
		{
			if (_lightControl == null)
			{
				_lightControl = CreateSystem();
				if (_lightControl == null)
				{
					Console.WriteLine("No se pudo crear el sistema");
					return "";
				}
			}
			
			//Get variables from the system
			FuzzyVariable horas = _lightControl.InputByName("hora");
			FuzzyVariable lightExt = _lightControl.InputByName("luzExt");
			FuzzyVariable lightInt = _lightControl.OutputByName("luzInt");
			
			//Associate given values with input variables
			Dictionary<FuzzyVariable, double> inputValues = new Dictionary<FuzzyVariable, double>();
			inputValues.Add(horas, (double)hora);
			inputValues.Add(lightExt, (double)luzExt);
			
			//Calculate result
			Dictionary<FuzzyVariable, double> result = _lightControl.Calculate(inputValues);
			
			return result[lightInt].ToString("f1");
		}
	}

	public class Temperature
	{
		public MamdaniFuzzySystem _temperatureControl = null;
		
		//Uses mamdani method to control the temperature of the AC.
		public string Regulate(float temperatura, int personas)
		{
			if (_temperatureControl == null)
			{
				_temperatureControl = CreateSystem();
				if (_temperatureControl == null)
				{
					//Console.WriteLine("No se pudo crear el sistema");
					return "";
				}
			}
			
			//Get variables from the system
			FuzzyVariable sensor = _temperatureControl.InputByName("TempSensor");
			FuzzyVariable people = _temperatureControl.InputByName("NumPersonas");
			FuzzyVariable ac = _temperatureControl.OutputByName("AirCond");
			
			//Associate given values with input variables
			Dictionary<FuzzyVariable, double> inputValues = new Dictionary<FuzzyVariable, double>();
			inputValues.Add(sensor, (double) temperatura);
			inputValues.Add(people, (double) personas);
			
			//Calculate result
			Dictionary<FuzzyVariable, double> result = _temperatureControl.Calculate(inputValues);
			
			return result[ac].ToString("f1");
		}
		
		private MamdaniFuzzySystem CreateSystem()
		{
			//Create empty Mamdani Fuzzy System
			MamdaniFuzzySystem temperatureControl = new MamdaniFuzzySystem();
			
			
			//Create Fuzzy variables for the system
			FuzzyVariable tempSensor = new FuzzyVariable("TempSensor", -20, 50);
			tempSensor.Terms.Add(new FuzzyTerm("MuyBaja", new TriangularMembershipFunction(-20.0, 0.0, 20.0)));
			tempSensor.Terms.Add(new FuzzyTerm("Baja", new TriangularMembershipFunction(-10.0, 5.0, 25.0)));
			tempSensor.Terms.Add(new FuzzyTerm("Media", new TriangularMembershipFunction(0.0, 20.0, 40.0)));
			tempSensor.Terms.Add(new FuzzyTerm("Alta", new TriangularMembershipFunction(10.0, 35.0, 45.0)));
			tempSensor.Terms.Add(new FuzzyTerm("MuyAlta", new TriangularMembershipFunction(15.0, 40.0, 50.0)));
			temperatureControl.Input.Add(tempSensor);
			
			
			FuzzyVariable personas = new FuzzyVariable("NumPersonas", 0, 100);
			personas.Terms.Add(new FuzzyTerm("Nula", new TriangularMembershipFunction(0.0, 0.0, 0.0)));
			personas.Terms.Add(new FuzzyTerm("Poca", new TriangularMembershipFunction(0.0, 5.0, 20.0)));
			personas.Terms.Add(new FuzzyTerm("Mucha", new TriangularMembershipFunction(0.0, 30.0, 100.0)));
			temperatureControl.Input.Add(personas);
			
			
			//create variables for output
			FuzzyVariable ac = new FuzzyVariable("AirCond", -5, 28);
			ac.Terms.Add(new FuzzyTerm("Apagado", new TriangularMembershipFunction(-5.0, 0.0, 16.0)));
			ac.Terms.Add(new FuzzyTerm("Baja", new TriangularMembershipFunction(16.0, 18.0, 24.0)));
			ac.Terms.Add(new FuzzyTerm("Media", new TriangularMembershipFunction(16.0, 22.0, 24.0)));
			ac.Terms.Add(new FuzzyTerm("Alta", new TriangularMembershipFunction(22.0, 27.0, 28.0)));
			temperatureControl.Output.Add(ac);
			
			//Create fuzzy rules
			//try
			//{
			MamdaniFuzzyRule rule1 = temperatureControl.ParseRule("if (TempSensor is MuyBaja) and (NumPersonas is Nula) then AirCond is Apagado");
			MamdaniFuzzyRule rule2 = temperatureControl.ParseRule("if (TempSensor is MuyBaja) and (NumPersonas is Poca) then AirCond is Apagado");
			MamdaniFuzzyRule rule3 = temperatureControl.ParseRule("if (TempSensor is MuyBaja) and (NumPersonas is Mucha) then AirCond is Apagado");
			MamdaniFuzzyRule rule4 = temperatureControl.ParseRule("if (TempSensor is Baja) and (NumPersonas is Nula) then AirCond is Apagado");
			MamdaniFuzzyRule rule5 = temperatureControl.ParseRule("if (TempSensor is Baja) and (NumPersonas is Poca) then AirCond is Alta");
			MamdaniFuzzyRule rule6 = temperatureControl.ParseRule("if (TempSensor is Baja) and (NumPersonas is Mucha) then AirCond is Alta");
			MamdaniFuzzyRule rule7 = temperatureControl.ParseRule("if (TempSensor is Media) and (NumPersonas is Nula) then AirCond is Apagado");
			MamdaniFuzzyRule rule8 = temperatureControl.ParseRule("if (TempSensor is Media) and (NumPersonas is Poca) then AirCond is Media");
			MamdaniFuzzyRule rule9 = temperatureControl.ParseRule("if (TempSensor is Media) and (NumPersonas is Mucha) then AirCond is Baja");
			MamdaniFuzzyRule rule10 = temperatureControl.ParseRule("if (TempSensor is Alta) and (NumPersonas is Nula) then AirCond is Apagado");
			MamdaniFuzzyRule rule11 = temperatureControl.ParseRule("if (TempSensor is Alta) and (NumPersonas is Poca) then AirCond is Media");
			MamdaniFuzzyRule rule12 = temperatureControl.ParseRule("if (TempSensor is Alta) and (NumPersonas is Mucha) then AirCond is Baja");
			MamdaniFuzzyRule rule13 = temperatureControl.ParseRule("if (TempSensor is MuyAlta) and (NumPersonas is Nula) then AirCond is Apagado");
			MamdaniFuzzyRule rule14 = temperatureControl.ParseRule("if (TempSensor is MuyAlta) and (NumPersonas is Poca) then AirCond is Baja");
			MamdaniFuzzyRule rule15 = temperatureControl.ParseRule("if (TempSensor is MuyAlta) and (NumPersonas is Mucha) then AirCond is Baja");
			
			temperatureControl.Rules.Add(rule1);
			temperatureControl.Rules.Add(rule2);
			temperatureControl.Rules.Add(rule3);
			temperatureControl.Rules.Add(rule4);
			temperatureControl.Rules.Add(rule5);
			temperatureControl.Rules.Add(rule6);
			temperatureControl.Rules.Add(rule7);
			temperatureControl.Rules.Add(rule8);
			temperatureControl.Rules.Add(rule9);
			temperatureControl.Rules.Add(rule10);
			temperatureControl.Rules.Add(rule11);
			temperatureControl.Rules.Add(rule12);
			temperatureControl.Rules.Add(rule13);
			temperatureControl.Rules.Add(rule14);
			temperatureControl.Rules.Add(rule15);
			/*}
		catch (Exception ex)
		{
			Console.WriteLine(string.Format("Parsing exception: {0}", ex.Message));
		}*/
			return temperatureControl;
		}
		
		//tells if the AC should be turned on or off
		public bool on_off(Dictionary<string,bool> schedule, float temperaturaSensor)
		{
			//time format in schedule is ' HH:MM p.m./a.m. - HH:MM p.m./a.m.'
			foreach(KeyValuePair<string,bool> entry in schedule)
			{
				string[] times = entry.Key.ToString().Split('-');
				times[0] = times[0].Trim();
				times[1] = times[1].Trim();
				string now = DateTime.Now.ToString("HH:mm");
				int result1 = DateTime.Compare(Convert.ToDateTime(now), Convert.ToDateTime(times[0]));
				int result2 = DateTime.Compare(Convert.ToDateTime(now), Convert.ToDateTime(times[1]));
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

	public class DoorWindow
	{
		private MamdaniFuzzySystem doorsWindows = null;
		
		private MamdaniFuzzySystem CreateSystem()
		{
			//Create empty Mamdani Fuzzy System
			MamdaniFuzzySystem doorwindow = new MamdaniFuzzySystem();
			
			
			//Create Fuzzy variables for the system
			FuzzyVariable tempSensor = new FuzzyVariable("sensor", -20, 50);
			tempSensor.Terms.Add(new FuzzyTerm("MuyBaja", new TriangularMembershipFunction(-20.0, 0.0, 20.0)));
			tempSensor.Terms.Add(new FuzzyTerm("Baja", new TriangularMembershipFunction(-10.0, 5.0, 25.0)));
			tempSensor.Terms.Add(new FuzzyTerm("Media", new TriangularMembershipFunction(0.0, 20.0, 40.0)));
			tempSensor.Terms.Add(new FuzzyTerm("Alta", new TriangularMembershipFunction(10.0, 35.0, 45.0)));
			tempSensor.Terms.Add(new FuzzyTerm("MuyAlta", new TriangularMembershipFunction(15.0, 40.0, 50.0)));
			doorwindow.Input.Add(tempSensor);
			
			FuzzyVariable ac = new FuzzyVariable("ac", 0.0, 28.0);
			ac.Terms.Add(new FuzzyTerm("Apagado", new TrapezoidMembershipFunction(0.0, 0.0, 16.0, 16.0)));
			ac.Terms.Add(new FuzzyTerm("Encendido", new TrapezoidMembershipFunction(16.0, 16.0, 28.0, 28.0)));
			doorwindow.Input.Add(ac);
			
			//Create variable for output
			FuzzyVariable dwExit = new FuzzyVariable("dwExit", 0.0, 10.0);
			dwExit.Terms.Add(new FuzzyTerm("Cerradas", new TrapezoidMembershipFunction(0.0,0.0,5.0,5.0)));
			dwExit.Terms.Add(new FuzzyTerm("Abiertas", new TrapezoidMembershipFunction(5.0,5.0, 10.0, 10.0)));
			doorwindow.Output.Add(dwExit);
			
			//Create fuzzy rules
			try
			{
				//para abrirlas
				MamdaniFuzzyRule rule1 = doorwindow.ParseRule("if (sensor is MuyBaja) and (ac is Apagado) then dwExit is Cerradas");
				MamdaniFuzzyRule rule2 = doorwindow.ParseRule("if (sensor is MuyBaja) and (ac is Encendido) then dwExit is Cerradas");
				
				MamdaniFuzzyRule rule3 = doorwindow.ParseRule("if (sensor is Baja) and (ac is Apagado) then dwExit is Cerradas");
				MamdaniFuzzyRule rule4 = doorwindow.ParseRule("if (sensor is Baja) and (ac is Encendido) then dwExit is Cerradas");
				
				MamdaniFuzzyRule rule5 = doorwindow.ParseRule("if (sensor is Media) and (ac is Apagado) then dwExit is Abiertas");
				MamdaniFuzzyRule rule6 = doorwindow.ParseRule("if (sensor is Media) and (ac is Encendido) then dwExit is Cerradas");
				
				MamdaniFuzzyRule rule7 = doorwindow.ParseRule("if (sensor is Alta) and (ac is Apagado) then dwExit is Abiertas");
				MamdaniFuzzyRule rule8 = doorwindow.ParseRule("if (sensor is Alta) and (ac is Encendido) then dwExit is Cerradas");
				
				MamdaniFuzzyRule rule9 = doorwindow.ParseRule("if (sensor is MuyAlta) and (ac is Apagado) then dwExit is Abiertas");
				MamdaniFuzzyRule rule10 = doorwindow.ParseRule("if (sensor is MuyAlta) and (ac is Encendido) then dwExit is Cerradas");
				
				doorwindow.Rules.Add(rule1);
				doorwindow.Rules.Add(rule2);
				doorwindow.Rules.Add(rule3);
				doorwindow.Rules.Add(rule4);
				doorwindow.Rules.Add(rule5);
				doorwindow.Rules.Add(rule6);
				doorwindow.Rules.Add(rule7);
				doorwindow.Rules.Add(rule8);
				doorwindow.Rules.Add(rule9);
				doorwindow.Rules.Add(rule10);
			}
			catch (Exception ex)
			{
				Console.WriteLine("No se pudo crear el sistema");
				Console.WriteLine(string.Format("Parsing exception: {0}", ex.Message));
			}
			return doorwindow;
		}
		
		public string open_close(float t, float ac)
		{
			if (doorsWindows == null)
			{
				doorsWindows = CreateSystem();
			}
			
			//Get variables from the system
			FuzzyVariable sensorInput = doorsWindows.InputByName("sensor");
			FuzzyVariable acInput = doorsWindows.InputByName("ac");
			FuzzyVariable exit = doorsWindows.OutputByName("dwExit");
			
			//Associate given values with input variables
			Dictionary<FuzzyVariable, double> inputValues = new Dictionary<FuzzyVariable, double>();
			inputValues.Add(sensorInput, (double)t);
			inputValues.Add(acInput, (double)ac);
			
			
			//Calculate result
			Dictionary<FuzzyVariable, double> result = doorsWindows.Calculate(inputValues);
			return result[exit].ToString("f1");
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}