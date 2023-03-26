﻿namespace DatabaseConnector.Interfaces;


public interface IMessage : IEntity
{
    public int Id { get; }
    public int UserId { get; }
    public bool IsVisible { get; }
    public DateTime CreationDate { get; }
    //public Content Content { get;  }
}
