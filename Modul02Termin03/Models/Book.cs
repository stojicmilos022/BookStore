using System.ComponentModel.DataAnnotations;

namespace Modul02Termin03.Models
{
    public class Book
    {

        public int Id { get;   set; }
        [Required(ErrorMessage = "Book name cannot be empty")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Name has to be at least 2 characters long and 50 characters max length")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Price cannot be empty")]
        [Range(0.01,double.MaxValue,ErrorMessage="Price must be grater than zero")]
        public double? Price { get; set; }
        public  Genre Genre{ get; set; }
        
        public bool Deleted { get; set; }

        public Book()
        {
            
        }


    }
}
