using CountriesMobile.Common.Models;
using CountriesMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace CountriesMobile.Prism.ViewModels
{
    public class CountryDetailPageViewModel : ViewModelBase
    {
        private CountryResponse _countryResponse;

        private DelegateCommand _selectTravelAdviceCommand;

        public DelegateCommand SelectTravelAdviceCommand => _selectTravelAdviceCommand ?? (_selectTravelAdviceCommand = new DelegateCommand(ShowTravelAdvice));

        private readonly INavigationService _navigationService;

        public CountryDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Sets and Gets the CountryResponse property
        /// </summary>
        public CountryResponse CountryResponse
        {
            get => _countryResponse;
            set => SetProperty(ref _countryResponse, value);
        }

        /// <summary>
        /// Overrides the onNavigatedTo method. Defines the parameters received when navigated to the page
        /// </summary>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                CountryResponse = parameters.GetValue<CountryResponse>("country");
                Title = CountryResponse.Name;
            }
        }

        /// <summary>
        /// Navigates to the TravelAdvicePage and passes the received parameters by functions that replace problematic values
        /// </summary>
        private void ShowTravelAdvice()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "url", ReplaceRegion(CountryResponse.Region.ToLower()) + "/" + ReplaceSpecialCharacters(ReplaceAndCombo(CountryResponse.Translations.Pt.ToLower())) }
            };

            _navigationService.NavigateAsync(nameof(TravelAdvicePage), parameters);
        }

        /// <summary>
        /// Replaces problematic strings in the Region parameter
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceRegion(string str)
        {
            if(str == "europe")
            {
                return "europa";
            }
            else if(str == "americas")
            {
                return "america";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Replaces a problematic value with a defined value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceAndCombo(string str)
        {
            if (!str.Equals("sao-tome-e-principe") && str.Contains("-e-"))
            {
                str = str.Replace("-e-", "-");
            }
            return str;
        }

        /// <summary>
        /// Replaces special characters with their matched normal characters 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceSpecialCharacters(string str)
        {
            string bad = "áàãâéèêíìîóòôúç ";

            string good = "aeiouc";

            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < bad.Length; j++)
                {
                    if (str[i] == bad[j])
                    {
                        if (j <= 3)
                        {
                            str = str.Replace(str[i], good[0]);
                        }
                        else if (j <= 6)
                        {
                            str = str.Replace(str[i], good[1]);
                        }
                        else if (j <= 9)
                        {
                            str = str.Replace(str[i], good[2]);
                        }
                        else if (j <= 12)
                        {
                            str = str.Replace(str[i], good[3]);
                        }
                        else if (j == 13)
                        {
                            str = str.Replace(str[i], good[4]);
                        }
                        else if(j == 14)
                        {
                            str = str.Replace(str[i], good[5]);
                        }
                        else
                        {
                            str = str.Replace(str[i], '-');
                        }
                    }
                }
            }

            return str;
        }
    }
}
