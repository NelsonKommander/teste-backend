using System;
using System.ComponentModel.DataAnnotations;
using prova.Validation;

#nullable disable


namespace prova.Models
{
    public partial class Cartao
    {
        [Required(ErrorMessage = "Nome do titular inválido")]
        public string Titular { get; set; }

        [Required(ErrorMessage = "Número do cartão inválido")]
        [CreditCard]
        public string Numero { get; set; }

        [DataExpiracao(ErrorMessage = "Data de expiração inválida")]
        public string Data_Expiracao { get; set; }

        [Required(ErrorMessage = "Bandeira inválida")]
        public string Bandeira { get; set; }

        [Required(ErrorMessage = "CVV inválido")]
        [StringLength(3, ErrorMessage = "CVV inválido")]
        public string Cvv { get; set; }

    }
}
