using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace prova.Validation
{
    public class ValidationResult
    {
        public string Message { get; } 

        public List<ValidationError> Errors { get; }

        public ValidationResult(ModelStateDictionary modelState)
        {
            // De acordo com a especificação do desafio os erros de
            // validação devem retornar código 412 com a mensagem
            // "Os valores informados não são válidos"
            Message = "Os valores informados não são válidos";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}