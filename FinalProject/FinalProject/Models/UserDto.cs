namespace FinalProject.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? NickName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime Birthday { get; set; } = DateTime.MinValue;
        public string? Email { get; set; }
        public bool IsBanned { get; set; }
    }
}
