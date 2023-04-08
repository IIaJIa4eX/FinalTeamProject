using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnector.DTO;

public class IssueDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsVisible { get; set; }
    public short IssueType { get; set; }
    public string? ContentText { get; set; }
    public int ContentId { get; set; }
}
