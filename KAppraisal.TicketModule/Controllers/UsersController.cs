using KAppraisal.TicketModule.Queries;
using KAppraisal.TicketModule.Services;
using KAppraisal.TicketModule.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KAppraisal.TicketModule.Controllers;

public class UsersController(IUserService service) : RootController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<UserDto>>> GetAll([FromQuery] UserQuery query)
        => Ok(await service.GetAllAsync(query));

    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetMe()
        => Ok(await service.GetMeAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(string id)
        => Ok(await service.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(UserActionDto dto)
        => Ok(await service.CreateAsync(dto));
}
