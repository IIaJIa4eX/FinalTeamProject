using Microsoft.VisualBasic;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.Globalization;

namespace FinalProject.Models.Requests
{
    public class CreateUserRequest
    {
        public string? NickName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
