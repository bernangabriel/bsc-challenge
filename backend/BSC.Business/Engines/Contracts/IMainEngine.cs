using BSC.Business.Entities;
using BSC.Business.Entities.DTOs;
using BSC.Core.Common.Contracts;

namespace BSC.Business.Engines.Contracts;

public interface IMainEngine : IBusinessEngine
{
    Task<User> SignInAsync(string userName, string password);
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserAsync(Guid userId);
    Task<UserProfileInfoDto> GetUserProfileInformationAsync(Guid userId);
    Task<bool> SaveUserAsync(UserDto input);
    Task<bool> RemoveUserAsync(Guid userId);
    Task<IEnumerable<EventLogResultDto>> GetEventsAsync();
    Task SaveEventLogAsync(SaveEventLogInputDto input);
}