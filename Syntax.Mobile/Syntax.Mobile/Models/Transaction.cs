using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax.Mobile.Models
{
    public enum EventTypeTransaction
    {
        Despesas,
        Renda
    }
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EventTypeTransaction Type { get; set; }
        public string IdUser { get; set; }
        public int IdTransactionClass { get; set; }
        public string TransactionClassNavigation { get; set; }
        public string UserNavigation { get; set; }


    }

}
