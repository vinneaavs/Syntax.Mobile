using Syntax.Mobile.Models;
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
        public ObservableCollection<Transaction> Transactions { get; set; }

        public TransactionsPage(TransactionsViewModel transactionsViewModel)
        {
            InitializeComponent();

            Transactions = transactionsViewModel.Transactions;
            BindingContext = this;
        }
    }
}
