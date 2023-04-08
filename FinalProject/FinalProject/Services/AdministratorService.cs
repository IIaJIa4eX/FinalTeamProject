using DatabaseConnector;
using DatabaseConnector.Interfaces;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using DatabaseConnector.DTO;
using DatabaseConnector.DTO.Post;
using System.Linq.Expressions;

namespace FinalProject.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly EFGenericRepository<User> _users;
        private readonly EFGenericRepository<Post> _posts;
        private readonly EFGenericRepository<Comment> _comments;
        private readonly EFGenericRepository<Content> _contents;
        private readonly EFGenericRepository<Issue> _issues;
        public AdministratorService(
            EFGenericRepository<User> users,
            EFGenericRepository<Post> posts,
            EFGenericRepository<Comment> comments,
            EFGenericRepository<Content> contents,
            EFGenericRepository<Issue> issues)
        {
            _users = users;
            _posts = posts;
            _comments = comments;
            _contents = contents;
            _issues = issues;
        }
        public IEnumerable<UserDto> GetAllUsers()
        {
            return MapUser(_users.Get());

        }
        public IEnumerable<UserDto> GetUsers(Expression<Func<User, bool>> predicate)
        {
            return MapUser(_users.Get(predicate));
        }
        private IEnumerable<UserDto> MapUser(IEnumerable<User> array)
        {
            List<UserDto> result = new List<UserDto>(array.Count());
            foreach (var user in array)
            {
                result.Add(new UserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    NickName = user.NickName,
                    Email = user.Email,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    Birthday = user.Birthday,
                    IsBanned = user.IsBanned
                });
            }
            return result;
        }

    }
}
