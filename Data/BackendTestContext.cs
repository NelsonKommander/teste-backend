using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using prova.Models;
#nullable disable

namespace prova.Data
{
    public partial class BackendTestContext : DbContext
    {
        public BackendTestContext()
        {
        }

        public BackendTestContext(DbContextOptions<BackendTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("produtos", "backend_test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataAtualizacao).HasColumnName("data_atualizacao");

                entity.Property(e => e.DataCriacao).HasColumnName("data_criacao");

                entity.Property(e => e.DataExclusao).HasColumnName("data_exclusao");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("nome");

                entity.Property(e => e.QtdeEstoque).HasColumnName("qtde_estoque");

                entity.Property(e => e.ValorUnitario)
                    .HasPrecision(10, 2)
                    .HasColumnName("valor_unitario");
            });

            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.ToTable("transacoes", "backend_test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataVenda).HasColumnName("data_venda");

                entity.Property(e => e.ProdutoId).HasColumnName("produto_id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.ValorVenda)
                    .HasPrecision(10, 2)
                    .HasColumnName("valor_venda");

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.Transacoes)
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transacoes_produtos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
