using Newtonsoft.Json;
using Syntax.Mobile.Models;
using Syntax.Mobile.Services;
using Syntax.Mobile.ViewModels;
using Syntax.Mobile.Views;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

public class LoginViewModel : BaseViewModel
{
    public Login Login { get; set; }
    public Command LoginCommand { get; }

    public LoginViewModel()
    {
        Login = new Login() { Email = "", Password = "" };
        LoginCommand = new Command(OnLoginClicked);
    }


    private async void OnLoginClicked(object obj)
    {
     

        if (string.IsNullOrWhiteSpace(Login.Email))
        {
            await Application.Current.MainPage.DisplayAlert("Erro", "O campo de email é obrigatório", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(Login.Password))
        {
            await Application.Current.MainPage.DisplayAlert("Erro", "O campo de senha é obrigatório", "Ok");
            return;
        }
        var login = new Login { Email = Login.Email, Password = Login.Password };

        var navigation = Application.Current.MainPage.Navigation;
        var authService = new AuthService(new HttpClient(), navigation);
        var response = await authService.Login(login);




    }
}
