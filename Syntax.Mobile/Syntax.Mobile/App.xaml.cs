using Syntax.Mobile.Services;
using Syntax.Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Syntax.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<INavigationService, NavigationService>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            await Shell.Current.GoToAsync("//LoginPage");

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
