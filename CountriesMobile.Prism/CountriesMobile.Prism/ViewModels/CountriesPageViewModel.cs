using CountriesMobile.Common.Models;
using CountriesMobile.Common.Services;
using CountriesMobile.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Essentials;

namespace CountriesMobile.Prism.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private bool _isRunning;

        private string _search;
        private List<CountryResponse> _myCountries;
        private DelegateCommand _searchCommand;

        private ObservableCollection<CountryItemViewModel> _countries;

        /// <summary>
        /// Constructs the ViewModel
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="apiService"></param>
        public CountriesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;

            Title = "Countries";
            LoadCountriesAsync();
        }

        /// <summary>
        /// Defines the DelegateCommand with the name SearchCommand
        /// </summary>
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCountries));

        /// <summary>
        /// Sets and Gets the Search property
        /// </summary>
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
            }
        }


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        /// <summary>
        /// If there is no Internet Connection displays an error message
        /// </summary>
        private async void LoadCountriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "ConnectionError", "Accept");
                return;
            }

            IsRunning = true;

            // Gets the list of objects from the API which is defined by the given parameters
            Response response = await _apiService.GetListAsync<CountryResponse>(
                "https://restcountries.eu",
                "/rest/v2",
                "/all");

            IsRunning = false;

            // Displays an error message
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            _myCountries = (List<CountryResponse>)response.Result;
            ShowCountries();
        }

        /// <summary>
        /// Builds a new ObservableCollection with the given ViewModels
        /// Checks for null or empty values and replaces them with a default value
        /// </summary>
        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(c =>
                new CountryItemViewModel(_navigationService)
                {
                    Name = (string.IsNullOrEmpty(c.Name) ? "N/A" : c.Name),
                    TopLevelDomain = c.TopLevelDomain,
                    Alpha2Code = (string.IsNullOrEmpty(c.Alpha2Code) ? "N/A" : c.Alpha2Code),
                    Alpha3Code = (string.IsNullOrEmpty(c.Alpha3Code) ? "N/A" : c.Alpha3Code),
                    CallingCodes = c.CallingCodes,
                    Capital = (string.IsNullOrEmpty(c.Capital) ? "N/A" : c.Capital),
                    AltSpellings = c.AltSpellings,
                    Region = (string.IsNullOrEmpty(c.Region) ? "N/A" : c.Region),
                    Subregion = (string.IsNullOrEmpty(c.Subregion) ? "N/A" : c.Subregion),
                    Population = c.Population,
                    Latlng = c.Latlng,
                    Demonym = (string.IsNullOrEmpty(c.Demonym) ? "N/A" : c.Demonym),
                    Area = c.Area,
                    Gini = c.Gini,
                    Timezones = c.Timezones,
                    Borders = c.Borders,
                    NativeName = (string.IsNullOrEmpty(c.NativeName) ? "N/A" : c.NativeName),
                    NumericCode = (string.IsNullOrEmpty(c.NumericCode) ? "N/A" : c.NumericCode),
                    Currencies = c.Currencies,
                    Languages = c.Languages,
                    Translations = c.Translations,
                    Flag = c.Flag,
                    RegionalBlocs = c.RegionalBlocs,
                    Cioc = (string.IsNullOrEmpty(c.Cioc) ? "N/A" : c.Cioc)
                })
                    .ToList());
            }
            else
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(c =>
                new CountryItemViewModel(_navigationService)
                {
                    Name = (string.IsNullOrEmpty(c.Name) ? "N/A" : c.Name),
                    TopLevelDomain = c.TopLevelDomain,
                    Alpha2Code = (string.IsNullOrEmpty(c.Alpha2Code) ? "N/A" : c.Alpha2Code),
                    Alpha3Code = (string.IsNullOrEmpty(c.Alpha3Code) ? "N/A" : c.Alpha3Code),
                    CallingCodes = c.CallingCodes,
                    Capital = (string.IsNullOrEmpty(c.Capital) ? "N/A" : c.Capital),
                    AltSpellings = c.AltSpellings,
                    Region = (string.IsNullOrEmpty(c.Region) ? "N/A" : c.Region),
                    Subregion = (string.IsNullOrEmpty(c.Subregion) ? "N/A" : c.Subregion),
                    Population = c.Population,
                    Latlng = c.Latlng,
                    Demonym = (string.IsNullOrEmpty(c.Demonym) ? "N/A" : c.Demonym),
                    Area = c.Area,
                    Gini = c.Gini,
                    Timezones = c.Timezones,
                    Borders = c.Borders,
                    NativeName = (string.IsNullOrEmpty(c.NativeName) ? "N/A" : c.NativeName),
                    NumericCode = (string.IsNullOrEmpty(c.NumericCode) ? "N/A" : c.NumericCode),
                    Currencies = c.Currencies,
                    Languages = c.Languages,
                    Translations = c.Translations,
                    Flag = c.Flag,
                    RegionalBlocs = c.RegionalBlocs,
                    Cioc = (string.IsNullOrEmpty(c.Cioc) ? "N/A" : c.Cioc)
                })
                    .Where(c => c.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
