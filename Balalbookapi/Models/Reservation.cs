using System.ComponentModel.DataAnnotations.Schema;
namespace Balalbookapi.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Book_Id { get; set; }
        public DateTime date_from_star { get; set; }
        public DateTime date_to_end { get; set; }

        [ForeignKey("Book_Id")]
        public Book? book { get; set; }
    }
}
