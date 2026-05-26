using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantEmployeeApi.Application.Commands.CreateEmployee;
using MultiTenantEmployeeApi.Application.Commands.DeleteEmployee;
using MultiTenantEmployeeApi.Application.Commands.UpdateEmployee;
using MultiTenantEmployeeApi.Application.Queries.GetEmployeeById;
using MultiTenantEmployeeApi.Application.Queries.GetEmployees;

namespace MultiTenantEmployeeApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
           var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById),new { id = result.ID },result);
        }
          

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetEmployeesQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
            => Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateEmployeeCommand command)
            => Ok(await _mediator.Send(command with { Id = id }));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => Ok(await _mediator.Send(new DeleteEmployeeCommand(id)));

    }
}
