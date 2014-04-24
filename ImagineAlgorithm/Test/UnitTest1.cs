using System;
using System.Collections.Generic;
using ImagineAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTemperature()
        {
            float tempSensor = 20.0f;
            //test para probar
            Temperature t = new Temperature();
            string regulada = t.Regulate(tempSensor, 10);
            Dictionary<string, bool> _horario = new Dictionary<string, bool>();
            _horario.Add("6:00-7:00", false);
            _horario.Add("7:00-8:00", false);
            _horario.Add("9:00-10:00", true);
            _horario.Add("10:00-11:00", true);
            _horario.Add("11:00-12:00", false);
            _horario.Add("12:00-13:00", false);
            _horario.Add("17:00-18:30", true);

            //determinar si el AC debe estar apagado o prendido
            //t.on_off determina si debe estar prendido de acuerdo al horario

            string strVal = "";
            bool ap_en = t.on_off(_horario, tempSensor); //debe estar prendido/apagado dependiendo clase

            if (float.Parse(regulada) >= 16 && ap_en == true) strVal = "Encendido";
            else strVal = "Apagado";

            Assert.AreEqual("Encendido", strVal);
        }
    }
}
