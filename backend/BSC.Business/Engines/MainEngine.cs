using BSC.Business.Engines.Contracts;
using BSC.Business.Entities;
using BSC.Business.Entities.DTOs;
using BSC.Business.Entities.Enums;
using BSC.Core.Common.Contracts;

namespace BSC.Business.Engines;

public class MainEngine : IMainEngine
{
    private readonly IDataRepositoryFactory _dataRepositoryFactory;
    private readonly ICryptProvider _cryptProvider;

    public MainEngine(IDataRepositoryFactory dataRepositoryFactory, ICryptProvider cryptProvider)
    {
        _dataRepositoryFactory = dataRepositoryFactory;
        _cryptProvider = cryptProvider;
    }

    public async Task<User> SignInAsync(string userName, string password)
    {
        var encryptedPass = _cryptProvider.Encrypt(password);

        var result = await _dataRepositoryFactory.GetDataRepository<User>()
            .GetSingleAsync(x => x.Select(s => s),
                x => x.UserName == userName && x.Password == encryptedPass && x.IsActiveFlag && !x.IsDeleteFlag);

        if (result != null)
        {
            await SaveEventLogAsync(new SaveEventLogInputDto
                { UserId = result.UserId, EventTypeId = EventTypes.InicioSesion });
        }

        return result;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync() =>
        await _dataRepositoryFactory.GetDataRepository<User>()
            .GetAllAsync(x => x
                .Select(s => new UserDto
                {
                    UserId = s.UserId,
                    UserName = s.UserName,
                    Name = s.Name,
                    LastName = s.LastName,
                    Email = s.Email,
                    CreatedDate = s.CreatedDate,
                    IsActive = s.IsActiveFlag
                }).OrderByDescending(o => o.CreatedDate), x => !x.IsDeleteFlag);

    public async Task<UserDto> GetUserAsync(Guid userId) =>
        await _dataRepositoryFactory.GetDataRepository<User>()
            .GetSingleAsync(x => x.Select(s => new UserDto
            {
                UserId = s.UserId,
                UserName = s.UserName,
                Name = s.Name,
                LastName = s.LastName,
                Email = s.Email,
                CreatedDate = s.CreatedDate,
                IsActive = s.IsActiveFlag
            }).OrderByDescending(o => o.CreatedDate), x => x.UserId == userId && x.IsActiveFlag);

    public async Task<UserProfileInfoDto> GetUserProfileInformationAsync(Guid userId) =>
        await _dataRepositoryFactory.GetDataRepository<User>()
            .GetSingleAsync(x => x.Select(s => new UserProfileInfoDto()
            {
                UserId = s.UserId,
                UserName = s.UserName,
                Email = s.Email,
                FullName = s.Name + " " + s.LastName
            }), x => x.UserId == userId);

    public async Task<bool> SaveUserAsync(UserDto input)
    {
        var userRepo = _dataRepositoryFactory.GetDataRepository<User>();
        var isNewUser = !input.UserId.HasValue;

        var user = isNewUser
            ? new User
            {
                UserId = Guid.NewGuid(),
                UserName = input.UserName,
                Password = _cryptProvider.Encrypt(input.Password),
                CreatedDate = DateTime.Now
            }
            : await userRepo.GetSingleAsync(x => x.Select(s => s), x => x.UserId == input.UserId);

        user.Name = input.Name;
        user.LastName = input.LastName;
        user.Email = input.Email;
        user.IsActiveFlag = input.IsActive;

        if (isNewUser)
            await userRepo.AddAsync(user);
        else
            await userRepo.UpdateAsync(user);

        return true;
    }

    public async Task<bool> RemoveUserAsync(Guid userId)
    {
        var userRepo = _dataRepositoryFactory.GetDataRepository<User>();

        var user = await userRepo
            .GetSingleAsync(x => x.Select(s => s), x => x.UserId == userId);

        if (user == null)
            throw new InvalidOperationException("El usuario no fue encontrado.");

        user.IsDeleteFlag = true;

        await userRepo.UpdateAsync(user);

        return true;
    }

    public async Task<IEnumerable<EventLogResultDto>> GetEventsAsync()
    {
        return await _dataRepositoryFactory.GetDataRepository<EventLog>()
            .GetAllAsync(x => x
                .Select(s => new EventLogResultDto
                {
                    Id = s.Id,
                    UserName = s.User.UserName,
                    FullName = s.User.Name + " " + s.User.LastName,
                    EventTypeName = s.EventType.Name,
                    EventDate = s.EventDate,
                    Payload = s.Payload,
                }).OrderByDescending(o => o.EventDate));
    }

    public async Task SaveEventLogAsync(SaveEventLogInputDto input)
    {
        await _dataRepositoryFactory.GetDataRepository<EventLog>()
            .AddAsync(new EventLog
            {
                UserId = input.UserId,
                EventTypeId = (int)input.EventTypeId,
                Payload = !string.IsNullOrWhiteSpace(input.Payload) ? input.Payload : "N/A",
                EventDate = DateTime.Now,
                CreatedDate = DateTime.Now,
            });
    }
}