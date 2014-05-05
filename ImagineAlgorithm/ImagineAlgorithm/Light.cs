using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Fuzzy.Library;

namespace ImagineAlgorithm
{
    public class Light
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

        public String Regulate(float hora, float luzExt )
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
}
