using System;
using Microsoft.Phone.Controls;
using Windows.Networking.Proximity;
using System.Diagnostics;
using Windows.Networking.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Microsoft.WindowsAzure.MobileServices;
using NetduinoWP8.Models;
using System.Collections.Generic;

namespace NetduinoWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        StreamSocket _socket;
        string _receivedBuffer = "";
        public static MobileServiceClient MobileService = new MobileServiceClient(
            "https://sasp.azure-mobile.net/",
            "oTgtoILdgGLwLWxNZQdzXLwDTjSSlV53"
        );
        private static IMobileServiceTable<Sensor> table;
        Sensor s;

        public MainPage()
        {
            InitializeComponent();
            initializeModel();
            temperatureText.Text = "-";
            TryConnect();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public async void initializeModel()
        {
            table = MobileService.GetTable<Sensor>();
            string uid = "HC-06";
            s = new Sensor();
            List<Sensor> lista = await table.Where(it => it.uid == uid).ToListAsync();
            if (lista.Count == 0)
            {
                s = new Sensor();
                s.uid = uid;
                s.dato = "0";
                s.activo = "";
                try
                {
                    await table.InsertAsync(s);
                }
                catch
                {
                }
            }
            else
                s = lista[0];
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private async void TryConnect()
        {
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
            var pairedDevices = await PeerFinder.FindAllPeersAsync();
            try
            {
                if (pairedDevices.Count == 0)
                {
                    Debug.WriteLine("No paired devices were found.");
                }
                else
                {
                    PeerInformation selectedDevice = pairedDevices[0];
                    _socket = new StreamSocket();
                    await _socket.ConnectAsync(selectedDevice.HostName, "1");
                    WaitForData(_socket);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                TryConnect();
            }
        }

        async private void WaitForData(StreamSocket socket)
        {
            try
            {
                byte[] bytes = new byte[sizeof(double)];
                await socket.InputStream.ReadAsync(bytes.AsBuffer(), sizeof(double), InputStreamOptions.Partial);
                double data = BitConverter.ToDouble(bytes, 0);
                Debug.WriteLine(data);
                if (data > 1)
                {
                    temperatureText.Text = data.ToString() + "°C";
                    s.dato = data.ToString();
                    s.activo = "activo";
                    await table.UpdateAsync(s);
                }
            }
            catch
            {
                temperatureText.Text = "-";
                TryConnect();
            }
            System.Threading.Thread.Sleep(500);
            WaitForData(socket);
        }

    }
}