using System;
using Microsoft.Phone.Controls;
using Windows.Networking.Proximity;
using System.Diagnostics;
using Windows.Networking.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

namespace NetduinoWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        StreamSocket _socket;
        string _receivedBuffer = "";

        public MainPage()
        {
            InitializeComponent();
            TryConnect();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
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
                temperatureText.Text = data.ToString() + "°C";
            }
            catch
            {
                TryConnect();
            }
            WaitForData(socket);
        }

    }
}