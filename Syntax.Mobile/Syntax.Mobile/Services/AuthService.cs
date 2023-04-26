﻿using Newtonsoft.Json;
using Syntax.Mobile.DTO;
using Syntax.Mobile.Models;
using Syntax.Mobile.ViewModels;
using Syntax.Mobile.Views;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syntax.Mobile.ViewModels;


namespace Syntax.Mobile.Services
{
    class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly INavigation _navigation;

        public AuthService(HttpClient httpClient, INavigation navigation)
        {
            _httpClient = httpClient;
            _navigation = navigation;
        }

        public async Task<ApiResponse<LoginResponse>> Login(Login login)
        {
            var client = new HttpClient();
            string uri = "http://10.0.2.2:5069/api/user/login";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                Application.Current.Properties["SyntaxToken"] = result.Token;

                if (Application.Current.Properties.ContainsKey("SyntaxToken"))
                {
                    var transactionsViewModel = new TransactionsViewModel();
                    var transactionsPage = new TransactionsPage(transactionsViewModel);

                    await _navigation.PushAsync(transactionsPage);
                }
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ApiError>();
                return new ApiResponse<LoginResponse>(error);
            }

            return null;
        }
    }
}
