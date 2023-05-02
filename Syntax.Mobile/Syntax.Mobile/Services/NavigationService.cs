using Syntax.Mobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Syntax.Mobile.Services
{
    public interface INavigationService
    {
        Task NavigateToHomePage();
        Task NavigateBack();
    }

    public class NavigationService : INavigationService
    {
        public async Task NavigateToHomePage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        public async Task NavigateBack()
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                // Implemente a lógica de sair do aplicativo ou mostrar uma mensagem ao usuário, se necessário
            }
        }
    }
}
