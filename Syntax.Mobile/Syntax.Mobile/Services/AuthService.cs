using Newtonsoft.Json;
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
using Newtonsoft.Json.Linq;
using static Android.Media.Session.MediaSession;
using System.IdentityModel.Tokens.Jwt;
using Android.Content.Res;

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
            string uri = "https://syntaxapi.azurewebsites.net/api/user/login";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                string token = result.Token;
                Application.Current.Properties["SyntaxToken"] = token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                if (Application.Current.Properties.ContainsKey("SyntaxToken"))
                {

                    await Shell.Current.Navigation.PopToRootAsync();

                    await Shell.Current.Navigation.PushAsync(new HomePage(), true);
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
