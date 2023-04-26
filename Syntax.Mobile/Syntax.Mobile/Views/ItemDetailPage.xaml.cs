using Syntax.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Syntax.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}