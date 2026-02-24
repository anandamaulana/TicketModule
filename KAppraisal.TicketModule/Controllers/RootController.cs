using KAppraisal.TicketModule.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace KAppraisal.TicketModule.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RootController : ControllerBase
{
    protected async Task<IActionResult> Run(Func<Task> action)
    {
        await action();
        return Ok();
    }
}
