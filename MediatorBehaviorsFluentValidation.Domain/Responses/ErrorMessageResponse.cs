using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorBehaviorsFluentValidation.Domain.Responses
{
    public class ErrorMessageResponse
    {
        public string? PropertyName { get; set; }
        public string? ErrorMessage { get; set; }

        public ErrorMessageResponse(string? propertyName, string? errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}
