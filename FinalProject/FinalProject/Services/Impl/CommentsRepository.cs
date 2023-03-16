using FinalProject.Data;

namespace FinalProject.Services.Impl
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly FinalProjectDbContext _context;
        private readonly ILogger<CommentsRepository> _logger;
        public CommentsRepository(FinalProjectDbContext context, ILogger<CommentsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int Create(Comments data)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Comments> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Comments> GetByClientId(string id)
        {
            throw new NotImplementedException();
        }

        public Comments GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Comments data)
        {
            throw new NotImplementedException();
        }
    }
}
