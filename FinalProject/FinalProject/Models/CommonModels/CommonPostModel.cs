using DatabaseConnector;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.CommonModels
{
    public class CommonPostModel
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsVisible { get; set; }

        public string? Category { get; set; }

        //Content Properties__
        public string? Description { get; set; } = string.Empty;


    }
}
