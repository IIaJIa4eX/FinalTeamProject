using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using FinalProject.Models;
using FinalProject.Models.Requests;
using FinalProject.Utils;

namespace FinalProject.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RegistrationService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public RegistrationResponse Registration(RegistrationRequest registrationRequest)
        {
 
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            Context context = scope.ServiceProvider.GetService<Context>();
            User email = context.Users.FirstOrDefault(x => x.Email.Equals(registrationRequest.Email));
            if (email != null)
            {
                return new RegistrationResponse
                {
                    Status = RegistrationStatus.UserFound
                };
            }

            User nickNamefound = context.Users.FirstOrDefault(x => x.NickName.Equals(registrationRequest.Nickname));
            if (nickNamefound != null)
            {
                return new RegistrationResponse
                {
                    Status = RegistrationStatus.NickNameFound
                };
            }

            (string passSalt, string passHash) result = PasswordUtils.CreatePasswordHash(registrationRequest.Password);
            User user = new User
            {
                NickName = registrationRequest.Nickname,
                Email = registrationRequest.Email,
                IsBanned = false,
                PasswordSalt = result.passSalt,
                PasswordHash = result.passHash
            };

            context.Users.Add(user);

            context.SaveChanges();

            return new RegistrationResponse
            {
                Status = RegistrationStatus.Success
            };
        }
    }
}
