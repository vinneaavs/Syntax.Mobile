using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syntax.Mobile.Models;

namespace Syntax.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailPage 
    {
        public TransactionDetailPage(Transaction transaction)
        {
            InitializeComponent();

           

            var stackLayout = new StackLayout();

            var valueLabel = new Label
            {
                Text = $"Value: R$: {transaction.Value}",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            };
            stackLayout.Children.Add(valueLabel);

            var descriptionLabel = new Label
            {
                Text = $"Description: {transaction.Description}",
                FontSize = 16
            };
            stackLayout.Children.Add(descriptionLabel);

            var dateLabel = new Label
            {
                Text = $"Date: {transaction.Date:dd/MM/yyyy}",
                FontSize = 16
            };
            stackLayout.Children.Add(dateLabel);

            Content = stackLayout;
        }
    }
}
