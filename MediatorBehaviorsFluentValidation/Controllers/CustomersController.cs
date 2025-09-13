using MediatorBehaviorsFluentValidation.Domain.Commands;
using MediatorBehaviorsFluentValidation.Domain.Queries.Customers;
using MediatorBehaviorsFluentValidation.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MediatorBehaviorsFluentValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Produces(MediaTypeNames.Application.Json)]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IMediator _mediator;

        public CustomersController(ILogger<CustomersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// List All Customers
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all customer</response>
        /// <response code="500">If ocurred a internal server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new GetAllCustomerQuery();
            _logger.LogInformation("listing get all custormer.");
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Custommer By CustomerId
        /// </summary>
        /// <param name="custormerId"></param>
        /// <returns>Return a exit customer by id</returns>
        /// <response code="200">Returns the found customer</response>
        /// <response code="404">If the custumer not found</response>
        /// <response code="500">If ocurred a internal server error.</response>
        [HttpGet("{custormerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsResponse))]
        public async Task<IActionResult> GetByAsync(int custormerId)
        {
            var query = new GetByIdCustomerQuery(custormerId);
            _logger.LogInformation("listing get by custormerId.");
            var result = await _mediator.Send(query);
            if (result is null)
            {
                var problemDetails = new ProblemDetailsResponse(
                    title: "Customer Not Found",
                    status: 404,
                    detail: $"Customer with ID {custormerId} was not found.",
                    instance: Request.Path
                );
                return NotFound(problemDetails);
            }

            return Ok(result);
        }


        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A newly created Customer</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Customer
        ///     {
        ///        "firstName": "Item #1",
        ///        "lastName": "Item #1",
        ///        "email": "Item #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If the custumer is null</response>
        /// <response code="500">If ocurred a internal server error.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetailsResponse))]
        public async Task<IActionResult> CreateCustomerAsync(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(Url?.Action(nameof(GetAllAsync)), new { customerId = result }, result);
        }
    }
}
