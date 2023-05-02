using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Syntax.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Syntax.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRequestRegisterPage : ContentPage
    {
        public UserRequestRegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterRequestButton_Clicked(object sender, EventArgs e)
        {
            // Crie uma instância de UserRegisterRequest e preencha os campos com os valores inseridos pelo usuário
            var userRegisterRequest = new UserRegisterRequest
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text,
                ReEntryPassword = confirmPasswordEntry.Text,
                Name = nameEntry.Text,
                LastName = lastNameEntry.Text,
                Role = null,
                CreationDate = DateTime.Now
            };

            if (!ValidateFields(userRegisterRequest))
                return;

            var httpClient = new HttpClient();
            string uri = $"https://syntaxapi.azurewebsites.net/api/user/register";

            var jsonContent = new StringContent(JsonConvert.SerializeObject(userRegisterRequest), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Registration successful!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                var errorInfo = JsonConvert.DeserializeObject<ErrorInfo>(responseContent);

                if (errorInfo != null && errorInfo.Errors != null && errorInfo.Errors.Count > 0)
                {
                    var errorMessage = string.Join(Environment.NewLine, errorInfo.Errors.SelectMany(i => i.Value));
                    await DisplayAlert("Error", errorMessage, "OK");
                }
                else
                {
                    await DisplayAlert("Error", "An error occurred during registration.", "OK");
                }
            }
        }

        private bool ValidateFields(UserRegisterRequest userRegisterRequest)
        {
            if (string.IsNullOrWhiteSpace(userRegisterRequest.Email) ||
                string.IsNullOrWhiteSpace(userRegisterRequest.Password) ||
                string.IsNullOrWhiteSpace(userRegisterRequest.ReEntryPassword))
            {
                // Preencha todos os campos obrigatórios
                DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return false;
            }

            if (userRegisterRequest.Password != userRegisterRequest.ReEntryPassword)
            {
                // As senhas não coincidem
                DisplayAlert("Error", "Passwords do not match.", "OK");
                return false;
            }

            return true;
        }

       
    }
}
