using System.Collections.Generic;
using System.Threading.Tasks;
using prova.Data;
using prova.Models;
using prova.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using prova.Dto;
using System;

namespace prova.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    [ValidateModel]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ProdutoDto>>> Get([FromServices] BackendTestContext context)
        {
            try
            {
                var produtos = await context.Produtos.Select(p => new ProdutoDto()
                                            {
                                                Id = p.Id,
                                                Nome = p.Nome,
                                                QtdeEstoque = p.QtdeEstoque,
                                                ValorUnitario = p.ValorUnitario
                                            })
                                .ToListAsync();
                return Ok(produtos);
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ProdutoDetalhadoDto>> GetById([FromServices] BackendTestContext context, int id)
        {
            try
            {
                ProdutoDetalhadoDto produtoDetalhado = await (from produto in context.Produtos
                                                        join transacao in context.Transacoes on produto.Id equals transacao.ProdutoId into aux
                                                        from entry in aux.DefaultIfEmpty()
                                                        where produto.Id == id
                                                        orderby entry.DataVenda descending
                                                        select new ProdutoDetalhadoDto()
                                                                {
                                                                    Id = produto.Id,
                                                                    Nome = produto.Nome,
                                                                    QtdeEstoque = produto.QtdeEstoque,
                                                                    ValorUnitario = produto.ValorUnitario,
                                                                    ValorVenda = entry.ValorVenda,
                                                                    DataVenda = entry.DataVenda
                                                                }).FirstOrDefaultAsync();

                if (produtoDetalhado == null) {
                    return BadRequest("Ocorreu um erro desconhecido");
                }

                return Ok(produtoDetalhado);
            }
            catch
            {
                
                return BadRequest("Ocorreu um erro desconhecido");
            }
            
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(
            [FromServices] BackendTestContext context,
            [FromBody] ProdutoDto model)
        {
            try
            {
                DateTime now = DateTime.Now;

                Produto produto = new Produto()
                                    {
                                        Nome = model.Nome,
                                        QtdeEstoque = model.QtdeEstoque,
                                        ValorUnitario = model.ValorUnitario,
                                        DataCriacao = now,
                                        DataAtualizacao = now
                                    };

                context.Produtos.Add(produto);
                await context.SaveChangesAsync();

                return Ok("Produto cadastrado");
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido");
            }
           
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromServices] BackendTestContext context, int id)
        {
            var produtoToDelete = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

            try
            {
                context.Produtos.Remove(produtoToDelete);
                await context.SaveChangesAsync();
                return Ok("Produto excluído com sucesso");
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido");
            }
        }
    }
}