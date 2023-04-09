using DatabaseConnector.DTO;

namespace DatabaseConnector.Extensions;

public static class UserEx
{
    public static UserDto Remap(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Birthday = user.Birthday,
            Email = user.Email,
            FirstName = user.FirstName,
            IsBanned = user.IsBanned,
            LastName = user.LastName,
            NickName = user.NickName,
            Patronymic = user.Patronymic
        };
    }
}
