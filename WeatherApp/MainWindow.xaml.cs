using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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


namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ApiKey = "d27b83f5e1880dfe4cc0fc20525fddba";
        private const string ApiEndpoint = "https://api.openweathermap.org/data/2.5/weather";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text;
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Введите название города!");
                return;
            }

            try
            {
                WeatherData weatherData = await GetWeatherData(city);
                UpdateUI(weatherData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private async Task<WeatherData> GetWeatherData(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{ApiEndpoint}?q={city}&appid={ApiKey}&units=metric&lang=ru";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
                    return weatherData;
                }
                else
                {
                    throw new Exception($"Не удалось получить данные о погоде. \n{response.StatusCode}");
                }
            }
        }

        private void UpdateUI(WeatherData weatherData)
        {
            ResultTextBlock.Text = $"Температура: {weatherData.Main.Temperature} °C\n" +
                              $"Описание: {weatherData.Weather[0].Description}\n" +
                              $"Скорость ветра: {weatherData.Wind.Speed} м/с";
        }
    }

    public class WeatherData
    {
        public MainInfo Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public WindInfo Wind { get; set; }
    }

    public class MainInfo
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
    }

    public class WindInfo
    {
        public float Speed { get; set; }
    }
}
