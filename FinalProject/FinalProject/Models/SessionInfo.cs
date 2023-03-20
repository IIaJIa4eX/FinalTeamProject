﻿using FinalProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

public class SessionInfo
{
    public Guid SessionId { get; set; }
    public string SessionToken { get; set; }
    public AccountDto Account { get; set; }
}
