using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using prova.Models;

#nullable disable


namespace prova.Dto
{
    public partial class PagamentoDto
    {
        [Required]
        [Range(0.001, 99999999.99, ErrorMessage = "Valor inválido")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Cartão de crédito inválido")]
        public Cartao Cartao { get; set; }

    }
}
