using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Syntax.Mobile.ViewModels
{
    public enum EventTypeTransaction
    {
        Despesas,
        Renda
    }
    internal class InputExpenseViewModel
    {
        public InputExpenseViewModel() { }

        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public EventTypeTransaction Type { get; set; }
        public string IdUser { get; set; }
        public int IdTransactionClass { get; set; }

        public string UserNavigation { get; set; }

        public string TransactionClassNavigation { get; set; }
    }
}
