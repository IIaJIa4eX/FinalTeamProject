using DatabaseConnector;
using FinalProject.DataBaseContext;

namespace FinalProject.Services
{
    public class UserRepository
    {
        private readonly Context _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public Guid Create(User data)
        {
            _context.Users.Add(data);
            _context.SaveChanges();
            return new Guid();
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Update(User data)
        {
            throw new NotImplementedException();
        }
    }
}
