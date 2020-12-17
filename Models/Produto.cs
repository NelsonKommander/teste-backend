﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable


namespace prova.Models
{
    public partial class Produto
    {
        public Produto()
        {
            Transacoes = new HashSet<Transacao>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto deve conter entre 1 e 45 caracteres")]
        [MinLength(1, ErrorMessage = "O nome do produto deve conter entre 1 e 45 caracteres")]
        [MaxLength(1, ErrorMessage = "O nome do produto deve conter entre 1 e 45 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Valor unitário inválido")]
        [Range(0, 99999999.99, ErrorMessage = "Valor unitário inválido")]
        public decimal ValorUnitario { get; set; }

        [Required(ErrorMessage = "Quantidade inválida")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade inválida")]
        public int QtdeEstoque { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public DateTime DataAtualizacao { get; set; }
        
        public DateTime? DataExclusao { get; set; }

        public virtual ICollection<Transacao> Transacoes { get; set; }
    }
}
