using System.Threading.Tasks;
using prova.Data;
using prova.Models;
using prova.Validation;
using Microsoft.AspNetCore.Mvc;
using prova.Dto;
using System;
using System.Text.RegularExpressions;

namespace prova.Controllers
{
    [ApiController]
    [Route("api/pagamento")]
    [ValidateModel]
    public class PagamentoController : ControllerBase
    {
        
        [HttpPost]
        [Route("compras")]
        public async Task<IActionResult> Post(
            [FromServices] BackendTestContext context,
            [FromBody] PagamentoDto model)
        {
            try
            {
                int mes, ano;
                string estado;
                Regex regex = new Regex(@"(?<Mes>\d{2})/(?<Ano>\d{4})");
                Match match = regex.Match(model.Cartao.Data_Expiracao);

                mes = Int32.Parse(match.Groups["Mes"].Value);
                ano = Int32.Parse(match.Groups["Ano"].Value);

                DateTime now = DateTime.Now;
                // Checando o valor e a data de vencimento do cartão
                if (model.Valor > 100 && (ano > now.Year || (ano == now.Year && mes >= now.Month))){
                    estado = "APROVADO";
                } else {
                    estado = "REJEITADO";
                }

                var resposta = new {Valor = model.Valor, Estado = estado};
                return Ok(resposta);
            }
            catch
            {
                return BadRequest("Ocorreu um erro desconhecido");
            }
           
        }

    }
}