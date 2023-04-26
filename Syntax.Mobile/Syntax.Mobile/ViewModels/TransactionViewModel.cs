using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Syntax.Mobile.Models;
using Xamarin.Forms;

namespace Syntax.Mobile.ViewModels
{
    public class TransactionsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();

        public async Task GetTransactionsAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string token = (string)Application.Current.Properties["SyntaxToken"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var userEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var displayName = jwtToken.Claims.FirstOrDefault(c => c.Type == "displayName")?.Value;

            string uri = $"http://10.0.2.2:5069/api/transaction/user/{idUser}";

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var transactions = JsonConvert.DeserializeObject<ObservableCollection<Transaction>>(content);

                Transactions = transactions;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
