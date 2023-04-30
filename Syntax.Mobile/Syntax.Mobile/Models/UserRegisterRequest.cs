using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Syntax.Mobile.Models
{
    internal class UserRegisterRequest
    {

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [EmailAddress(ErrorMessage = "O Campo {0} é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigtório")]
        [StringLength(50, ErrorMessage = "O Campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "O conteúdo das senhas devem ser iguais !")]
        public string ReEntryPassword { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string Role { get; set; }
    }
}
