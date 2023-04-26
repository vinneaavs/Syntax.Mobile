using Syntax.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Syntax.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        private TransactionsViewModel _viewModel;

        public TransactionsPage(ObservableCollection<Models.Transaction> transactions)
        {
            InitializeComponent();
            _viewModel = new TransactionsViewModel();
            BindingContext = _viewModel;
            _viewModel.Transactions = transactions;
        }
    }
}