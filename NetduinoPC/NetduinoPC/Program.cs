using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetduinoPC
{
    class Program
    {
        static SerialPort serial;
        static void Main(string[] args)
        {
            serial = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            // open the serial-port, so we can send & receive data
            serial.Open();
            // add an event-handler for handling incoming data
            serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
            while (true)
            {
            }
        }

        static void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("DATA RECEIVED");
            // wait a little for the buffer to fill
            System.Threading.Thread.Sleep(200);

            // create an array for the incoming bytes
            byte[] bytes = new byte[serial.BytesToRead];
            // read the bytes
            serial.Read(bytes, 0, bytes.Length);

            // write the received bytes, as a string, to the console
            System.Console.WriteLine(BitConverter.ToDouble(bytes,0));
        }

    }
}
