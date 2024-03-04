using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Maps.MapControl.WPF;

namespace wpfapp1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async Task<JObject> Dane(double Latitude, double Longitude)
        {
            string apiKey = "1f5e197a2c6a52c7c68315b0d1f058bd";
            string url = $"http://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longitude}&appid={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();
                JObject pogoda = JObject.Parse(result);
                return pogoda;
            }
        }
        public async void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(mapa);
            var location = mapa.ViewportPointToLocation(mousePosition);
            var pogoda = await Dane(location.Latitude, location.Longitude);
            double tempKelvin = (double)pogoda["main"]["temp"];
            double tempKelvin1 = (double)pogoda["main"]["feels_like"];
            double tempCelsius = tempKelvin - 273.15;
            double tempCelsius1 = tempKelvin1 - 273.15;
            pogoda1.Content = $"Pogoda: {pogoda["weather"][0]["description"]}";
            temperatura.Content = $"Temperatura: {tempCelsius1:0.#}°C";
            odczuwalna.Content = $"Odczuwalna temperatura: {tempCelsius:0.#}°C";
            wilgotnosc.Content = $"Wilgotność: {pogoda["main"]["humidity"]}%";
        }

    }
}