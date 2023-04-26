using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Syntax.Mobile.Models
{
    public class Login
    {
        [EmailAddress(ErrorMessage = "O Campo {0} é Inválido !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo {0} é Obrigatório !")]
        public string Password { get; set; }
    }
}

