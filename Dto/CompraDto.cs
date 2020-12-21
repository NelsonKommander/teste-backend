using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using prova.Models;

#nullable disable


namespace prova.Dto
{
    public partial class CompraDto
    {
        [Required(ErrorMessage = "Id inválido")]
        [Range(1, int.MaxValue, ErrorMessage = "Id inválido")]
        public int Produto_Id { get; set; }
        
        [Required(ErrorMessage = "Quantidade inválida")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade inválida")]
        public int Qtde_Comprada { get; set; }

        [Required(ErrorMessage = "Cartão de crédito inválido")]
        public Cartao Cartao { get; set; }

    }
}
