using Newtonsoft.Json;
using Syntax.Mobile.Models;
using Syntax.Mobile.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Syntax.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputExpensePage : ContentPage
    {
        private IEnumerable<TransactionClass> transactions;
        private Label errorLabel;
        public TransactionClass SelectedTransaction { get; set; }
        public int SelectedTransactionId { get; set; }
        public int SelectedTransactionTypeId { get; set; }
        public string SelectedType { get; set; }

        private readonly INavigationService _navigationService;



        public InputExpensePage()
        {
            InitializeComponent();
            LoadDropdownData();

            NavigationPage.SetHasBackButton(this, false);




        }


        private async void LoadDropdownData()
        {
            var transactionList = await GetTc();
            if (transactionList != null)
            {
                var transactionNames = transactionList.Select(t => t.Name).ToList();
                typePicker.ItemsSource = transactionNames;
            }
        }

        private async Task<IEnumerable<TransactionClass>> GetTc()
        {
            var httpClient = new HttpClient();
            string uri = "https://syntaxapi.azurewebsites.net/api/transactionclass";

            try
            {
                var response = await httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Exibir o conteúdo JSON da resposta
                    Console.WriteLine(responseContent);

                    var transactions = JsonConvert.DeserializeObject<IEnumerable<TransactionClass>>(responseContent);
                    return transactions;
                }
                else
                {
                    errorLabel.Text = "Ocorreu um erro ao obter os dados da API.";
                    return null;
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = "Ocorreu um erro ao obter os dados da API.";
                return null;
            }
        }
        


        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedType = typePicker1.SelectedItem as string;
            SelectedTransactionId = GetIdItem(SelectedType);

        }
        private int GetIdItem(string selectedType)
        {
            switch (selectedType)
            {
                case "Despesas":
                    return 1;
                case "Renda":
                    return 2;
                default:
                    return 0;
            }
        }
        private async void TypePickerTC_Tapped(object sender, EventArgs e)
        {
            var transactionList = await GetTc();
            if (transactionList != null)
            {
                var transactionNames = transactionList.Select(t => t.Name).ToList();
                var selectedTransactionName = await DisplayActionSheet("Select Type", "Cancel", null, transactionNames.ToArray());
                var selectedTransaction = transactionList.FirstOrDefault(t => t.Name == selectedTransactionName);

                if (selectedTransaction != null)
                {
                    typePicker.SelectedItem = selectedTransaction;
                    SelectedTransactionTypeId = selectedTransaction.Id;

                }
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            string token = (string)Application.Current.Properties["SyntaxToken"];
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
                string idUser = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
                string displayName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "displayName")?.Value;
                try
                {
                    var addT = new Transaction();
                    addT.Date = DateTime.Now;
                    addT.Value = decimal.Parse(valueEntry.Text);
                    addT.Description = descriptionEntry.Text;
                    addT.IdTransactionClass = SelectedTransactionId;
                    addT.Type = (EventTypeTransaction)Enum.Parse(typeof(EventTypeTransaction), SelectedType);
                    addT.IdUser = idUser;

                    var httpClient = new HttpClient();
                    string uri = $"https://syntaxapi.azurewebsites.net/api/transaction";

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(addT), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(uri, jsonContent);
                    var responseContent = await response.Content.ReadAsStringAsync();


                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Success", "Registration successful!", "OK");
                        await Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Erro ao adicionar!", "Cancel");

                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var homePage = new HomePage();
            Navigation.PushAsync(homePage);
        }
    }
}