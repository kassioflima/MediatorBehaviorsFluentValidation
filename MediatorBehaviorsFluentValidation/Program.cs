using FluentValidation;
using MediatorBehaviorsFluentValidation.Domain.Interfaces;
using MediatorBehaviorsFluentValidation.Domain.PipeLineBehaviors;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Validation.Customer;
using MediatorBehaviorsFluentValidation.Extensions;
using MediatorBehaviorsFluentValidation.Repository.Repository;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerConfig();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies([typeof(Program).Assembly, typeof(GetAllCustomerQuery).Assembly]); });
builder.Services.AddValidatorsFromAssembly(typeof(CreateCustomerCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Configuração do Problem Details apenas para erros 500
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    {
        var problemDetails = context.ProblemDetails;
        problemDetails.Instance = context.HttpContext.Request.Path;
        
        // Adiciona informações de trace se disponível
        if (context.HttpContext.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true)
        {
            problemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        }
    };
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Mediator Behaviors Fluent Validation");
});

// Configuração do middleware de tratamento de exceções
app.UseExceptionHandler();

// Middleware customizado para FluentValidation
MediatorBehaviorsFluentValidation.Extensions.ApplicationBuilderExtensions.UseFluentValidationExceptionHalder(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
