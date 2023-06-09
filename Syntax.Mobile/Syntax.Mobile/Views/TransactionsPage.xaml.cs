﻿using Syntax.Mobile.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http.Json;
using System.Transactions;
using Newtonsoft.Json;
using Syntax.Mobile.Models;
using Xamarin.Forms.Internals;
using System.ComponentModel;
using System.Globalization;

namespace Syntax.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        private HttpClient _httpClient;

        public decimal Balance { get; set; }



        public TransactionsPage()
        {
            InitializeComponent();

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LoadTransactions();
        }

        private async void LoadTransactions()
        {
            if (Application.Current.Properties.ContainsKey("SyntaxToken"))
            {
                string token = (string)Application.Current.Properties["SyntaxToken"];
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                if (tokenHandler.CanReadToken(token))
                {
                    JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
                    string idUser = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
                    string displayName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "displayName")?.Value;

                    if (!string.IsNullOrEmpty(idUser))
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        string uri = $"https://syntaxapi.azurewebsites.net/api/transaction/user/{idUser}";

                        try
                        {
                            HttpResponseMessage response = await _httpClient.GetAsync(uri);

                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                Console.WriteLine(content);
                                var transactions = JsonConvert.DeserializeObject<IEnumerable<Models.Transaction>>(content);
                                decimal balance = 0;

                                transactions.ForEach(transaction =>
                                {
                                    balance += transaction.Type == EventTypeTransaction.Despesas ? -transaction.Value : transaction.Value;
                                    Balance = balance;
                                });

                                balanceLabel.Text = $"Balance: R$:{Balance}";

                                transactionsCollectionView.ItemsSource = transactions;
                                nomeUsuarioLabel.Text = $"Welcome {displayName}";
                            }
                            else
                            {
                                var error = await response.Content.ReadFromJsonAsync<ApiError>();
                                string errorMessage = error?.Message ?? "Unknown error";
                            }
                        }
                        catch (Exception ex)
                        {
                            string errorMessage = ex.Message;
                        }
                    }
                    else
                    {
                        await Navigation.PopToRootAsync();
                    }
                }
                else
                {

                    await Navigation.PopToRootAsync();
                }
            }
            else
            {

                await Navigation.PopToRootAsync();
            }
        }
        private Models.Transaction _selectedTransaction;
        public Models.Transaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set
            {
                if (_selectedTransaction != value)
                {
                    _selectedTransaction = value;
                    OnPropertyChanged(nameof(SelectedTransaction));

                    if (_selectedTransaction != null)
                    {
                        var detailPage = new TransactionDetailPage(_selectedTransaction);
                        Navigation.PushModalAsync(detailPage);

                        // Limpar a seleção após navegar para o modal de detalhes
                        SelectedTransaction = null;
                    }
                }
            }
        }

        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Models.Transaction selectedTransaction)
            {
                bool result = await DisplayAlert("Transaction Details", $"Value: {selectedTransaction.Value:C}\nDescription: {selectedTransaction.Description}\nDate: {selectedTransaction.Date:dd/MM/yyyy}", "OK", "Cancel");

                if (result)
                {
                }
                else
                {
                    // Código para lidar com o botão Cancel pressionado
                }

                transactionsCollectionView.SelectedItem = null; // Limpar a seleção
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());

        }

        
    }
}

