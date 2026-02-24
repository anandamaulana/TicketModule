using KAppraisal.TicketModule.Exceptions;
using KAppraisal.TicketModule.Enums;
using KAppraisal.TicketModule.Helpers;
using KAppraisal.TicketModule.Models;
using KAppraisal.TicketModule.Queries;
using KAppraisal.TicketModule.Repositories;
using KAppraisal.TicketModule.Specifications;
using KAppraisal.TicketModule.ViewModels;
using MapsterMapper;

namespace KAppraisal.TicketModule.Services;

public class UserService(
    IRepository<User> userRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IUserService
{
    private string GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.Request.Headers["X-USER-ID"].ToString();
        AppException.ThrowIfTrue(string.IsNullOrEmpty(userId), ExceptionType.Unauthorized, "X-USER-ID header is required.");
        return userId!;
    }

    public async Task<PaginatedResponse<UserDto>> GetAllAsync(UserQuery query)
    {
        var spec = new UserSpecification(query);
        var items = await userRepository.ListAsync(spec);
        var total = await userRepository.CountAsync(spec);
        var dtos = mapper.Map<List<UserDto>>(items);
        return PaginationHelper.Page(query, dtos, total);
    }

    public async Task<UserDto> GetByIdAsync(string id)
    {
        var user = await userRepository.GetByIdAsync(id);
        AppException.ThrowIfNull(user, ExceptionType.NotFound, $"User with ID={id} not found.");
        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(UserActionDto dto)
    {
        var user = mapper.Map<User>(dto);
        var result = await userRepository.AddAsync(user);
        return mapper.Map<UserDto>(result);
    }

    public async Task<UserDto> GetMeAsync()
    {
        var userId = GetUserId();
        var user = await userRepository.GetByIdAsync(userId);
        AppException.ThrowIfNull(user, ExceptionType.NotFound, "Current user not found. Please check X-USER-ID header.");
        return mapper.Map<UserDto>(user);
    }
}
