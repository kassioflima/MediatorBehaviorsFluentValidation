namespace MediatorBehaviorsFluentValidation.Domain.Responses
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
