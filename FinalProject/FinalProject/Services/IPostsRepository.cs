using FinalProject.Data;

namespace FinalProject.Services
{
    public interface IPostsRepository : IRepository<Posts, int>
    {
        IList<Posts> GetByClientId(string id);
    }
}
