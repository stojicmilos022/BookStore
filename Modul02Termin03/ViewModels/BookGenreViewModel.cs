using Modul02Termin03.Models;
using System.Collections.Generic;

namespace Modul02Termin03.ViewModels
{
    public class BookGenreViewModel
    {
        public Book Book { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
