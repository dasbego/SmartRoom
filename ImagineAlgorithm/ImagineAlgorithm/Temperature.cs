using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Fuzzy.Library;

namespace ImagineAlgorithm
{
    public class Temperature
    {
        public MamdaniFuzzySystem _temperatureControl = null;

        //Uses mamdani method to control the temperature of the AC.
        public String Regulate(float temperatura, int personas)
        {
            if (_temperatureControl == null)
            {
                _temperatureControl = CreateSystem();
                if (_temperatureControl == null)
                {
                    Console.WriteLine("No se pudo crear el sistema");
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
            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Parsing exception: {0}", ex.Message));
            }
            return temperatureControl;
        }

        //tells if the AC should be turned on or off
        public bool on_off(Dictionary<String,bool> schedule, float temperaturaSensor)
        {
            //time format in schedule is ' HH:MM p.m./a.m. - HH:MM p.m./a.m.'
            int i = 0;

            while (i<schedule.Count)
            {
                string[] times = schedule.Keys.ElementAt(i).ToString().Split('-');
                times[0] = times[0].Trim();
                times[1] = times[1].Trim();
                string now = DateTime.Now.ToString("HH:mm");
                int result1 = DateTime.Compare(Convert.ToDateTime(now), Convert.ToDateTime(times[0]));
                int result2 = DateTime.Compare(Convert.ToDateTime(now), Convert.ToDateTime(times[1]));
                if (result1 > 0 && result2 < 0)
                {
                    if (schedule.ElementAt(i).Value.Equals(true))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
                i++;
            }
            return false;
        }
    }
}
