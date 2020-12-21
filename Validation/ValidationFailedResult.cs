using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace prova.Validation
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) 
            : base(new ValidationResult(modelState))
        {
            // De acordo com a especificação do desafio os erros de
            // validação devem retornar código 412 com a mensagem
            // "Os valores informados não são válidos"
            StatusCode = StatusCodes.Status412PreconditionFailed;
        }
    }
}