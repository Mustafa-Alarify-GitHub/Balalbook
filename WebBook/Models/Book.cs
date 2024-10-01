using System.ComponentModel.DataAnnotations.Schema;

namespace WebBook.Models
{
    public class Book
    {
        public int Id { get; set; }
       
        public string? Book_Name { get; set; }
        public string? Blender_Name { get; set; } 
        public string? Version { get; set; }


    }
}
