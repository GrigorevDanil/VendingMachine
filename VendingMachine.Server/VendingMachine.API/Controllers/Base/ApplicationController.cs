using Microsoft.AspNetCore.Mvc;
using VendingMachine.Contracts.Models;

namespace VendingMachine.API.Controllers.Base;

[ApiController]
[Route( "api/[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}