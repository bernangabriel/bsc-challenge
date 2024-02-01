using BSC.Business.Engines.Contracts;
using BSC.Business.Entities.DTOs;
using BSC.Business.Entities.Enums;
using BSC.Core.Common.Contracts;
using BSC.Web.Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BSC.Web.Api.Controllers;

[Route("api/main")]
[Authorize]
public class MainApiController : ApiControllerBase
{
    private readonly IBusinessEngineFactory _businessEngineFactory;

    public MainApiController(IBusinessEngineFactory businessEngineFactory)
    {
        _businessEngineFactory = businessEngineFactory;
    }

    #region Users

    [HttpGet("users")]
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var mainEngine = _businessEngineFactory.GetBusinessEngine<IMainEngine>();

        var result = await mainEngine.GetUsersAsync();

        await mainEngine.SaveEventLogAsync(new SaveEventLogInputDto
        {
            UserId = UserId,
            EventTypeId = EventTypes.ConsultaUsuario
        });

        return result;
    }

    [HttpGet("user")]
    public async Task<UserDto> GetUser([FromQuery] Guid userId) =>
        await _businessEngineFactory.GetBusinessEngine<IMainEngine>()
            .GetUserAsync(userId);

    [HttpPost("save-user")]
    public async Task<bool> SaveUser([FromBody] UserDto input)
    {
        var mainEngine = _businessEngineFactory.GetBusinessEngine<IMainEngine>();

        var result = await mainEngine.SaveUserAsync(input);

        if (result)
        {
            await mainEngine.SaveEventLogAsync(new SaveEventLogInputDto
            {
                UserId = UserId,
                EventTypeId = input.UserId.HasValue
                    ? EventTypes.EditarUsuario
                    : EventTypes.CrearUsuario,
                Payload = "Set Additional User Info"
            });
        }

        return result;
    }

    [HttpDelete("remove-user")]
    public async Task<bool> RemoveUser([FromBody] RemoveUserInputDto input)
    {
        var mainEngine = _businessEngineFactory.GetBusinessEngine<IMainEngine>();

        var result = await mainEngine.RemoveUserAsync(input.UserId);

        if (result)
        {
            await mainEngine.SaveEventLogAsync(new SaveEventLogInputDto
            {
                UserId = UserId,
                EventTypeId = EventTypes.EliminarUsuario
            });
        }

        return result;
    }

    #endregion

    #region EventLogs

    [HttpGet("event-logs")]
    public async Task<IEnumerable<EventLogResultDto>> GetEventLogs() =>
        await _businessEngineFactory.GetBusinessEngine<IMainEngine>()
            .GetEventsAsync();

    #endregion
}