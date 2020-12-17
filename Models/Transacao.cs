using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using prova.Validation;

#nullable disable


namespace prova.Models
{
    public partial class Transacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Id do produto não pode ser vazio")]
        [Range(1, int.MaxValue, ErrorMessage = "Id de produto inválida")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Valor de venda inválido")]
        [Range(0, 99999999.99, ErrorMessage = "Valor de venda inválido")]
        public decimal ValorVenda { get; set; }

        public DateTime? DataVenda { get; set; }

        [ValidEstadoCompra(ErrorMessage = "Estado da transação inválido")]
        public string EstadoCompra { get; set; }
        
        public virtual Produto Produto { get; set; }
    }
}
