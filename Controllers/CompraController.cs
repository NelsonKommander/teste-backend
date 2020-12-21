using System.Threading.Tasks;
using prova.Data;
using prova.Models;
using prova.Validation;
using Microsoft.AspNetCore.Mvc;
using prova.Dto;
using System;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace prova.Controllers
{
    [ApiController]
    [Route("api/compras")]
    [ValidateModel]
    public class CompraController : ControllerBase
    {
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(
            [FromServices] BackendTestContext context,
            [FromBody] CompraDto model)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // O gateway fornecido (http://sv-dev-01.pareazul.com.br:8080/api/gateways/compras) não conseguiu ser acessado
                // e por tanto decidi por usar a api de pagamentos implementada no item 6 como gateway
                Uri link = new Uri("http://127.0.0.1:5000/api/pagamento/compras");
                var client = new HttpClient(clientHandler);

                Produto produto = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == model.Produto_Id);

                if (produto == null || produto.QtdeEstoque < model.Qtde_Comprada)
                {
                    return BadRequest("Ocorreu um erro desconhecido");
                }

                PagamentoDto pagamento = new PagamentoDto()
                                                {
                                                    Valor = produto.ValorUnitario*model.Qtde_Comprada,
                                                    Cartao = model.Cartao
                                                };
                var jsonInString = JsonConvert.SerializeObject(pagamento);

                var httpResponse = await client.PostAsync(link, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                httpResponse.EnsureSuccessStatusCode();

                string respostaEmString = await httpResponse.Content.ReadAsStringAsync();
                dynamic gatewayJson = JsonConvert.DeserializeObject(respostaEmString);

                DateTime now = DateTime.Now;

                // Atualizando a quantidade de estoque do produto e a data de atualização
                produto.QtdeEstoque = produto.QtdeEstoque - model.Qtde_Comprada;
                produto.DataAtualizacao = now;

                context.Entry(produto).State = EntityState.Modified;

                // Cadastrando a transação
                Transacao transacao = new Transacao()
                                            {
                                                ProdutoId = model.Produto_Id,
                                                ValorVenda = gatewayJson.valor,
                                                DataVenda = now,
                                                Estado = gatewayJson.estado
                                            };

                context.Transacoes.Add(transacao);

                await context.SaveChangesAsync();

                return Ok("Venda realizada com sucesso");
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido");
            }
           
        }

    }
}