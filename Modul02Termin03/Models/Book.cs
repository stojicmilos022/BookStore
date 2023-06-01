namespace Modul02Termin03.Models
{
    public class Book
    {

        public int Id { get;   set; }
        public string BookName { get; set; }
        public double Price { get; set; }
        public  Genre Genre{ get; set; }
        
        public bool Deleted { get; set; }

        public Book()
        {
            
        }


    }
}
