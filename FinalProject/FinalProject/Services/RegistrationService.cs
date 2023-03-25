using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models;
using FinalProject.Models.Requests;
using FinalProject.Utils;
using Microsoft.AspNetCore.Identity;

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
            User user = new User
            {
                NickName = registrationRequest.Nickname,
                Email = registrationRequest.Email,
                IsBanned = false,
                PasswordSalt = PasswordUtils.CreatePasswordHash(registrationRequest.Password).passwordSalt,
                PasswordHash = PasswordUtils.CreatePasswordHash(registrationRequest.Password).passwordHash
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
