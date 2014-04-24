using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagineAlgorithm
{
    class Test
    {
        static bool _ac_state;
        static Dictionary<string, bool> _horario;

        public static void Main()
        {
            const float temperaturaSensor = 35.0f;
            float temperaturaAC = TestTemperature(temperaturaSensor);
            TestDoorsAndWindows(temperaturaSensor, temperaturaAC);

            //test light
            //hora 1 y 60 de lux
            float hora = 5.0f; //1 -> 7am hasta 14 -> 20pm
            float lux = 80.0f; //lux extererior
            TestLight(hora, lux);


            Console.ReadLine();
        }

        public static float TestTemperature(float tempSensor)
        { //test para probar
            Temperature t = new Temperature();
            string regulada = t.Regulate(tempSensor, 10);
            Dictionary<string, bool> _horario = new Dictionary<string, bool>();
            _horario.Add("6:00-7:00", false);
            _horario.Add("7:00-8:00", false);
            _horario.Add("9:00-10:00", true);
            _horario.Add("10:00-11:00", true);
            _horario.Add("11:00-12:00", false);
            _horario.Add("12:00-13:00", false);
            _horario.Add("14:00-15:00", true);

            //determinar si el AC debe estar apagado o prendido
            //t.on_off determina si debe estar prendido de acuerdo al horario

            string strVal = "";
            bool ap_en = t.on_off(_horario, tempSensor); //debe estar prendido/apagado dependiendo clase

            if (float.Parse(regulada) >= 16 && ap_en == true) strVal = "Encendido";
            else strVal = "Apagado";

            Console.WriteLine("Son las " + DateTime.Now.ToString(" HH:mm ") +
                " y el AC debería estar: " + strVal + " a " + regulada + " grados");

            return float.Parse(regulada);
        }

        public static void TestDoorsAndWindows(float temperaturaSensor, float temperaturaAC)
        {
            //Obtengo temperatura del ac paara saber si esta on o off sabiendo que <16 esta off
            Temperature t = new Temperature();
            DoorWindow dw = new DoorWindow();
            string state;

            if (float.Parse(dw.open_close(temperaturaSensor, temperaturaAC)) <= 5) state = "Cerradas";
            else state = "Abiertas";
            Console.WriteLine("Las puertas están " + state);
        }

        public static void TestLight(float hora, float luzExt)
        {
            Light luz = new Light();
            string result = luz.Regulate(hora, luzExt);

            Console.WriteLine("Hora: " + hora + ", Intensidad de Luz Exterior: " + luzExt + ", Luz Interior:" + result);
        }
    }
}
