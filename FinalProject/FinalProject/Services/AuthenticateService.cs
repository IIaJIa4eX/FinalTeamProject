using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using DatabaseConnector.Extensions;
using FinalProject.Models.Requests;
using FinalProject.Utils;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProject.Services;

public class AuthenticateService : IAuthenticateService
{
    public const string SecretKey = "kYp3s6v9y/B?E(H+";
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Dictionary<string, SessionInfo> _sessions = new Dictionary<string, SessionInfo>();
    public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
    {
        public const string SecretKey = "kYp3s6v9y/B?E(H+";
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Dictionary<string, SessionInfo> _sessions = new Dictionary<string, SessionInfo>();

        public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _sessions.TryGetValue(sessionToken, out sessionInfo!);
        }
        if (sessionInfo == null)
        {
            SessionInfo sessionInfo;
            var handler = new JwtSecurityTokenHandler();
            var email = handler.ReadJwtToken(sessionToken).Claims.First(claim => claim.Type == "email").Value;

            
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
        }
        return sessionInfo!;
    }
    public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        Context context = scope.ServiceProvider.GetService<Context>()!;
        User account = !string.IsNullOrWhiteSpace(authenticationRequest.Email) ? FindAccountByLogin(context, authenticationRequest.Email) : null!;
        if (account == null)
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
                Status = AuthenticationStatus.UserNotFound
            };
        }
        if (!PasswordUtils.VerifyPassword(authenticationRequest.Password!, account.PasswordSalt, account.PasswordHash))
        {
            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.InvalidPassword
            };
        }
        AccountSession session = new AccountSession
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new
            SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.NickName),
                    new Claim(ClaimTypes.Email, user.Email)
                    
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private User FindAccountByLogin(Context context, string login)
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
            Account = account.Remap()
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
                    new Claim(ClaimTypes.Name, user.NickName!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email!),
                }),
            Expires = DateTime.Now.AddMinutes(15),                
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private User FindAccountByLogin(Context context, string login)
    {
        return context.Users.FirstOrDefault(account => account.Email == login)!;
    }
}