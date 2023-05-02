using Syntax.Mobile.ViewModels;
using Syntax.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Syntax.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(InputExpensePage), typeof(InputExpensePage));
            Routing.RegisterRoute(nameof(UserRequestRegisterPage), typeof(UserRequestRegisterPage));



            Application.Current.MainPage = new NavigationPage(new HomePage());


        }


    }
}
