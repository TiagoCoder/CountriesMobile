using CountriesMobile.Common.Models;
using CountriesMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace CountriesMobile.Prism.ItemViewModels
{
    public class CountryItemViewModel : CountryResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        /// <summary>
        /// Gets the navigation parameters and navigates to defined page
        /// </summary>
        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "country", this }
            };

            await _navigationService.NavigateAsync(nameof(CountryDetailPage), parameters);
        }
    }
}
