using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO.Ports;

namespace Netduino
{
    public class Program
    {
        static SerialPort serial;
        
        public static void Main()
        {
            var dhtSensor = new Dht11Sensor(Pins.GPIO_PIN_D6, Pins.GPIO_PIN_D7, PullUpResistor.Internal);
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            serial = new SerialPort(SerialPorts.COM1, 9600, Parity.None, 8, StopBits.One);
            byte[] buffer;
            // open the serial-port, so we can send & receive data
            serial.Open();

            while (true)
            {
                led.Write(false);
                Thread.Sleep(1000);
                led.Write(true);
                if (dhtSensor.Read())
                {
                    Debug.Print("DHT sensor Read() ok, RH = " + dhtSensor.Humidity.ToString("F1") + "%, Temp = " + dhtSensor.Temperature.ToString("F1") + "°C");
                    buffer = new byte[sizeof(float)];
                    buffer = System.BitConverter.GetBytes((double)dhtSensor.Temperature);
                    serial.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    Debug.Print("DHT sensor Read() failed");
                }
            }

        }

    }
}
