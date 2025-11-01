using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using WeatherNotesApp.Models;

namespace WeatherNotesApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "01ac7e1a9e973cae6f53ed1a508a6959"; // înlocuiește cu cheia ta
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherInfo?> GetWeatherAsync(string city)
        {
            try
            {
                string url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric&lang=ro";
                var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);

                if (response == null) return null;

                return new WeatherInfo
                {
                    CityName = response.name,
                    Temperature = response.main.temp,
                    Description = response.weather[0].description,
                    Humidity = response.main.humidity,
                    WindSpeed = response.wind.speed
                };
            }
            catch
            {
                return null;
            }
        }

        // clase pentru parsarea JSON-ului OpenWeather
        private class OpenWeatherResponse
        {
            public string name { get; set; }
            public WeatherDesc[] weather { get; set; }
            public MainInfo main { get; set; }
            public WindInfo wind { get; set; }
        }

        private class WeatherDesc { public string description { get; set; } }
        private class MainInfo { public double temp { get; set; } public double humidity { get; set; } }
        private class WindInfo { public double speed { get; set; } }
    }
}
