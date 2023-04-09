using DatabaseConnector;
using DatabaseConnector.DTO;
using System.Linq.Expressions;

namespace FinalProject.Interfaces;

public interface IAdministratorService
{
    public IEnumerable<UserDto> GetAllUsers();
    public IEnumerable<UserDto> GetUsers(Expression<Func<User, bool>> predicate);
}
