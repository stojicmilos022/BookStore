using Microsoft.Extensions.Configuration;
using Modul02Termin03.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Modul02Termin03.Repository
{
    public class BookRepository
    {
        IConfiguration Configuration { get; set; }

        public BookRepository(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public List<Book> GetAll()
        {
            string ConnectionString = Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "Select * from Book join Genre on Book.GenreId=Genre.Id and Book.Deleted=0";

            SqlCommand command = connection.CreateCommand();

            command.CommandText = query;
            DataTable dt = new DataTable("Book");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<Book> books = new List<Book>();
            foreach (DataRow dr in dt.Rows)
            {
                Book b = new Book();
                b.Id = int.Parse(dr["Id"].ToString());
                b.BookName = dr["BookName"].ToString();
                b.Price = double.Parse(dr["Price"].ToString());
                b.Genre = new Genre();
                b.Genre.GenreName = dr["GenreName"].ToString();
                books.Add(b);
            }

            command.Dispose();
            connection.Close();

            return books;
        }

        public List<Book> GetAllDeleted()
        {
            string ConnectionString = Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "Select * from Book join Genre on Book.GenreId=Genre.Id and Deleted=1";

            SqlCommand command = connection.CreateCommand();

            command.CommandText = query;
            DataTable dt = new DataTable("Book");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<Book> books = new List<Book>();
            foreach (DataRow dr in dt.Rows)
            {
                Book b = new Book();
                b.Id = int.Parse(dr["Id"].ToString());
                b.BookName = dr["BookName"].ToString();
                b.Price = double.Parse(dr["Price"].ToString());
                b.Genre = new Genre();
                b.Genre.GenreName = dr["GenreName"].ToString();
                books.Add(b);
            }

            command.Dispose();
            connection.Close();

            return books;
        }


        public void Create(Book book)
        {
            string ConnectionString = Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "INSERT INTO Book(BookName,Price,GenreId) values (@BookName,@Price,@GenreId)";

            SqlCommand command = connection.CreateCommand();

            command.CommandText = query;
            DataTable dt = new DataTable("Book");
            command.Parameters.AddWithValue("BookName", book.BookName);
            command.Parameters.AddWithValue("Price", book.Price);
            command.Parameters.AddWithValue("GenreId", book.Genre.Id);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();


        }

        public void Delete(int Id)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "update   Book set deleted=1 WHERE Id=@Id";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", Id);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        public void Restore(int Id)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "update   Book set deleted=0 WHERE Id=@Id";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", Id);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        public void Update(Book book, int oldId)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "UPDATE Book SET BookName=@BookName, Price=@Price, GenreId=@Genre WHERE Id=@oldId";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("oldId", oldId);
            command.Parameters.AddWithValue("BookName", book.BookName);
            command.Parameters.AddWithValue("Price", book.Price);
            command.Parameters.AddWithValue("Genre", book.Genre.Id);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        public Book GetOne(int Id)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "SELECT * FROM Book JOIN Genre ON Book.GenreId = Genre.Id WHERE Book.Id = @Id";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", Id);

            DataTable dt = new DataTable("Book");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            Book b = new Book();
            foreach (DataRow dr in dt.Rows)
            {
                b.Id = int.Parse(dr["Id"].ToString());
                b.BookName = dr["BookName"].ToString();
                b.Price = double.Parse(dr["Price"].ToString());
                b.Genre = new Genre();
                b.Genre.Id = int.Parse(dr["GenreId"].ToString());
                b.Genre.GenreName = dr["GenreName"].ToString();
            }

            command.Dispose();
            connection.Close();

            return b;
        }


    }
}
