using Microsoft.AspNetCore.Mvc.Filters;

namespace prova.Validation
{
    // Classe responsável por validar os modelos e gerar os erros
    // em formato customizado
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}