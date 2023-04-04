using DatabaseConnector;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.DTO.PostDTO
{
    public class CreatePostDTO
    {

        public int UserId { get; set; }

        public string? Category { get; set; }

        //Content Properties__
        public string? Description { get; set; } = string.Empty;


    }
}
