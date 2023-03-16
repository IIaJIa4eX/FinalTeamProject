using FinalProject.Data;

namespace FinalProject.Services
{
    public interface ICommentsRepository : IRepository<Comments, int>
    {
        IList<Comments> GetByClientId(string id);
    }
}
