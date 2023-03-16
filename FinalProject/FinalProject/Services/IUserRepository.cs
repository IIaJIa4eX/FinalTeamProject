using FinalProject.Data;

namespace FinalProject.Services
{
    public interface IUserRepository : IRepository<User, string>
    {
        IList<User> GetByClientId(string id);
    }
}
