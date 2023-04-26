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

            Application.Current.MainPage = new LoginPage();


        }


    }
}
