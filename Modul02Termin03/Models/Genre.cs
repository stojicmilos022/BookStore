using System.ComponentModel.DataAnnotations;

namespace Modul02Termin03.Models
{
    public class Genre
    {   
        public int Id { get; set; }
        [Required(ErrorMessage = "Genre name cannot be empty")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name has to be at least 2 characters long and 50 characters max length")]
        public string GenreName {get; set;}
    }
}
