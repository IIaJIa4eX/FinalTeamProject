using FinalProject.Data;

namespace FinalProject.Services.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly FinalProjectDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(FinalProjectDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public string Create(User data)
        {
            _context.Users.Add(data);
            _context.SaveChanges();
            return data.UserId.ToString();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<User> GetByClientId(string id)
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            throw new NotImplementedException();
        }

        public int Update(User data)
        {
            throw new NotImplementedException();
        }
    }
}
