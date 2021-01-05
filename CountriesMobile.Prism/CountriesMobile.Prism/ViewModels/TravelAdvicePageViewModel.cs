using Prism.Navigation;

namespace CountriesMobile.Prism.ViewModels
{
    public class TravelAdvicePageViewModel : ViewModelBase
    {
        private string _source;

        public string Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        public TravelAdvicePageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        /// <summary>
        /// Overrides the onNavigatedTo method. Defines the source property of the TravelAdvicePage
        /// </summary>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("url"))
            {
                Source = "https://portaldascomunidades.mne.gov.pt/pt/vai-viajar/conselhos-aos-viajantes/" + parameters.GetValue<string>("url");
            }
        }
    }
}