using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

// Criar atributo de validação que represente o Required mas traga a mensagem em portugês

namespace prova.Models
{
    public partial class Transacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id de produto inválida")]
        public int ProdutoId { get; set; }

        [Required]
        [Range(0, 99999999.99, ErrorMessage = "Valor de venda inválido")]
        public decimal ValorVenda { get; set; }

        public DateTime? DataVenda { get; set; }

        // Criar atributo de validação que represente o Enum!
        public string EstadoCompra { get; set; }
        
        public virtual Produto Produto { get; set; }
    }
}
