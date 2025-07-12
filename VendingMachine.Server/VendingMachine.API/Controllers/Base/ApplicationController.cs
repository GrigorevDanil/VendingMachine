using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.Models;

namespace VendingMachine.API.Controllers.Base;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}