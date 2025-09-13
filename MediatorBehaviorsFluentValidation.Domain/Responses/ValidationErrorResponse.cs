namespace MediatorBehaviorsFluentValidation.Domain.Responses
{
    /// <summary>
    /// Representa um erro de validação com código e mensagem
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Código do erro
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string Message { get; set; } = string.Empty;

        public ValidationError()
        {
        }

        public ValidationError(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    /// <summary>
    /// Resposta de erro de validação contendo lista de erros
    /// </summary>
    public class ValidationErrorResponse
    {
        /// <summary>
        /// Lista de erros de validação
        /// </summary>
        public List<ValidationError> Errors { get; set; } = new();

        public ValidationErrorResponse()
        {
        }

        public ValidationErrorResponse(List<ValidationError> errors)
        {
            Errors = errors;
        }
    }
}
