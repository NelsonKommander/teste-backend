using System;
using System.ComponentModel.DataAnnotations;

namespace prova.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidEstadoCompraAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool validacao = false;

            if ((string) value == "APROVADO" || (string) value == "REJEITADO"){
                validacao = true;
            }

            return validacao;
        }
    }
}
