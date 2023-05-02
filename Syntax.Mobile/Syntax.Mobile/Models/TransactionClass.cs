using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Syntax.Mobile.Models
{
    public class TransactionClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public DateTime? CreationDate { get; set; }
        public static ObservableCollection<string> GetTransactionNames(IEnumerable<TransactionClass> transactions)
        {
            return new ObservableCollection<string>(transactions.Select(t => t.Name));
        }
    }
}
