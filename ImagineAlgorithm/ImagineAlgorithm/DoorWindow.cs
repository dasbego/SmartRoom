using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Fuzzy.Library;

namespace ImagineAlgorithm
{
    internal class DoorWindow
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

        public String open_close(float t, float ac)
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
}
