﻿namespace FinalProject.Models
{
    public class AccountDto
    {
        public Guid AccountId { get; set; }
        public string EMail { get; set; }
        public bool Locked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
    }
}
