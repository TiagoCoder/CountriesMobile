using Prism;
using Prism.Ioc;
using CountriesMobile.Prism.ViewModels;
using CountriesMobile.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using CountriesMobile.Common.Services;

namespace CountriesMobile.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/CountriesPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register<IApiService, ApiService>();

            containerRegistry.RegisterForNavigation<TravelAdvicePage, TravelAdvicePageViewModel>();
            containerRegistry.RegisterForNavigation<CountriesPage, CountriesPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryDetailPage, CountryDetailPageViewModel>();
        }
    }
}
