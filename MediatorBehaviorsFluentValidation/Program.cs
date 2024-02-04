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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies([typeof(Program).Assembly, typeof(GetAllCustomerQuery).Assembly]); });
builder.Services.AddValidatorsFromAssembly(typeof(CreateCustomerCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Mediator Behaviors Fluent Validation");
});

MediatorBehaviorsFluentValidation.Extensions.ApplicationBuilderExtensions.UseFluentValidationExceptionHalder(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
