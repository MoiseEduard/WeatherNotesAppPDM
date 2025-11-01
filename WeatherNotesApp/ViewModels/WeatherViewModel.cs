using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherNotesApp.Models;
using WeatherNotesApp.Services;

namespace WeatherNotesApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService = new();
        private string _city;
        private WeatherInfo _weather;
        private bool _isBusy;
        public bool HasWeather => Weather != null;

        public string City
        {
            get => _city;
            set { _city = value; OnPropertyChanged(); }
        }

        public WeatherInfo Weather
        {
            get => _weather;
            set
            {
                _weather = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasWeather)); // notificăm și HasWeather
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public ICommand GetWeatherCommand { get; }

        public WeatherViewModel()
        {
            GetWeatherCommand = new Command(async () => await GetWeatherAsync());
        }

        private async Task GetWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(City)) return;

            IsBusy = true;
            Weather = await _weatherService.GetWeatherAsync(City);
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
