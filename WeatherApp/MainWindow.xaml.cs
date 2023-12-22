using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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

        //API ключ OpenWeatherMap
        private const string _KEY = "d27b83f5e1880dfe4cc0fc20525fddba";
        private const string _ENDPOINT = "https://api.openweathermap.org/data/2.5/weather";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Перемещаем окно
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            //Сворачиваем окно
            WindowState = WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //Закрываем приложение
            Application.Current.Shutdown();
        }

        private async void getWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем название города из текстового поля
            string city = cityTextbox.Text;

            // Проверяем, введено ли название города
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Введите название города!");
                return;
            }

            try
            {
                // Получаем данные о погоде по городу
                WeatherData weatherData = await GetWeatherData(city);

                // Обновляем интерфейс с полученными данными
                UpdateUI(weatherData);
            }
            catch (Exception ex)
            {                
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private async Task<WeatherData> GetWeatherData(string city)
        {
            // Используем HttpClient для выполнения HTTP-запроса
            using (HttpClient client = new HttpClient())
            {
                // Формируем URL для запроса с учетом города и API-ключа
                string apiUrl = $"{_ENDPOINT}?q={city}&appid={_KEY}&units=metric&lang=ru";
                // Отправляем GET-запрос и получаем ответ
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                // Проверяем успешность ответа
                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа как строку
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Десериализуем JSON в объект WeatherData
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
                    return weatherData;
                }
                else
                {
                    throw new Exception($"Не удалось получить данные о погоде\n{response.StatusCode}");
                }
            }
        }

        private void UpdateUI(WeatherData weatherData)
        {
            // Обновляем текстовые блоки с данными о погоде
            cityTextblock.Text = cityTextbox.Text;            
            descriptionTextblock.Text = $"Описание: {weatherData.Weather[0].Description}";
            tempTextblock.Text = $"{weatherData.Main.Temperature} °C";
            windTextblock.Text = $"{weatherData.Wind.Speed} м/с"; 
        }
    }
}
