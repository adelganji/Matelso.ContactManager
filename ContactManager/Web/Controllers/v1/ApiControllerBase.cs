using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Primitives.Result;

namespace Web.Controllers.v1;

//[ApiController]
//[Route("api/[controller]")]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
    }

    protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
    {
        return await _mediator.Send(command);
    }

    protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
    {
        return await _mediator.Send(query);
    }

    protected async Task<IActionResult> CheckException(Exception ex)
    {
        if (ex.Message.Contains("duplicate key") && ex.Message.Contains("email_key"))
            return StatusCode(StatusCodes.Status400BadRequest, Result.Failed("Email is unique, This Email has been taken."));
        return StatusCode(StatusCodes.Status400BadRequest, Result.Failed(ex.Message));
    }
}
