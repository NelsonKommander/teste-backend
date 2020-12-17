using Newtonsoft.Json;

namespace prova.Validation
{
    public class ValidationError
    {
        // Se o campo for nulo ele não vai aparecer no json resposta
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}

