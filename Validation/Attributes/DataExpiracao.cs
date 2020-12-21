using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace prova.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataExpiracaoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool validacao = false;
            int mes, ano;

            Regex regex = new Regex(@"(?<Mes>\d{2})/(?<Ano>\d{4})");

            if (value != null) {
                Match match = regex.Match((string) value);

                if (match.Success){
                    
                    mes = Int32.Parse(match.Groups["Mes"].Value);
                    ano = Int32.Parse(match.Groups["Ano"].Value);

                    if (mes > 0 && mes <= 12 && ano > 0){
                        validacao = true;
                    } 
                }
            }

            return validacao;
        }
    }
}
