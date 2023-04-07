using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using FinalProject.Models;
using FinalProject.Models.DTO;
using FinalProject.Models.Requests;
using FinalProject.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace FinalProject.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        public const string SecretKey = "kYp3s6v9y/B?E(H+";
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Dictionary<string, SessionInfo> _sessions = new Dictionary<string, SessionInfo>();
        public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public SessionInfo GetSessionInfo(string sessionToken)
        {
            SessionInfo sessionInfo;
            lock (_sessions)
            {
                _sessions.TryGetValue(sessionToken, out sessionInfo);
            }
            if (sessionInfo == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                Context context = scope.ServiceProvider.GetService<Context>();
                AccountSession session = context.AccountSessions.FirstOrDefault(item => item.SessionToken == sessionToken);
                if (session == null)
                    return null;
                User account = context.Users.FirstOrDefault(item => item.Id == session.AccountId);
                sessionInfo = GetSessionInfo(account, session);
                if (sessionInfo != null)
                {
                    lock (_sessions)
                    {
                        _sessions[sessionToken] = sessionInfo;
                    }
                }
            }
            return sessionInfo;
        }
        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            Context context = scope.ServiceProvider.GetService<Context>();
            User account = !string.IsNullOrWhiteSpace(authenticationRequest.Email) ? FindAccountByLogin(context, authenticationRequest.Email) : null;
            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.UserNotFound
                };
            }
            if (!PasswordUtils.VerifyPassword(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.InvalidPassword
                };
            }
            AccountSession session = new AccountSession
            {
                AccountId = account.Id,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeLastRequest = DateTime.Now,
                IsClosed = false,
                TimeClosed = DateTime.Now.AddMinutes(15)
            };
            context.AccountSessions.Add(session);
            context.SaveChanges();
            SessionInfo sessionInfo = GetSessionInfo(account, session);
            lock (_sessions)
            {
                _sessions[sessionInfo.SessionToken] = sessionInfo;
            }
            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                SessionInfo = sessionInfo
            };
        }
        private SessionInfo GetSessionInfo(User account, AccountSession accountSession)
        {
            return new SessionInfo
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Account = new UserDto
                {
                    Id = account.Id,
                    NickName = account.NickName,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Patronymic = account.Patronymic,
                    Birthday = account.Birthday,
                    Email = account.Email,
                    IsBanned = account.IsBanned
                }
            };
        }
        private string CreateSessionToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new
            SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    }),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private User FindAccountByLogin(Context context, string login)
        {
            return context.Users.FirstOrDefault(account => account.Email == login);
        }

    }
}
