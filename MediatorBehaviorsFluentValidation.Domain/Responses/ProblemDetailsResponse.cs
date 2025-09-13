using System.Text.Json.Serialization;

namespace MediatorBehaviorsFluentValidation.Domain.Responses
{
    /// <summary>
    /// Representa uma resposta de erro seguindo o padrão RFC 7807 Problem Details
    /// </summary>
    public class ProblemDetailsResponse
    {
        /// <summary>
        /// URI que identifica o tipo de problema
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = "about:blank";

        /// <summary>
        /// Título curto e legível do tipo de problema
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Código de status HTTP específico para este problema
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// Descrição legível específica para esta ocorrência do problema
        /// </summary>
        [JsonPropertyName("detail")]
        public string? Detail { get; set; }

        /// <summary>
        /// URI que identifica a instância específica do problema
        /// </summary>
        [JsonPropertyName("instance")]
        public string? Instance { get; set; }

        /// <summary>
        /// Extensões adicionais para o problema
        /// </summary>
        [JsonPropertyName("extensions")]
        public Dictionary<string, object> Extensions { get; set; } = new();

        public ProblemDetailsResponse()
        {
        }

        public ProblemDetailsResponse(string title, int status, string? detail = null, string? instance = null)
        {
            Title = title;
            Status = status;
            Detail = detail;
            Instance = instance;
        }

        /// <summary>
        /// Adiciona uma extensão ao problema
        /// </summary>
        /// <param name="key">Chave da extensão</param>
        /// <param name="value">Valor da extensão</param>
        public void AddExtension(string key, object value)
        {
            Extensions[key] = value;
        }
    }
}
