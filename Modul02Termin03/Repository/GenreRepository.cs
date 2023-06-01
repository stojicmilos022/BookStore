using Microsoft.Extensions.Configuration;
using Modul02Termin03.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.Design;

namespace Modul02Termin03.Repository
{
    public class GenreRepository
    {

        IConfiguration Configuration;

        public GenreRepository(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void Create(Genre genre)
        {
            string ConnectionString = Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "INSERT INTO Genre(GenreName) values (@GenreName)";

            SqlCommand command = connection.CreateCommand();

            command.CommandText = query;
            DataTable dt = new DataTable("Genre");
            command.Parameters.AddWithValue("GenreName", genre.GenreName);


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();


        }
        public List<Genre> GetAll()
        {
            string ConnectionString = Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "Select * from Genre where Deleted=0";

            SqlCommand command = connection.CreateCommand();

            command.CommandText = query;
            DataTable dt = new DataTable("Genre");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<Genre> genres = new List<Genre>();
            foreach (DataRow dr in dt.Rows)
            {
                Genre g = new Genre();
                g.Id = int.Parse(dr["Id"].ToString());
                g.GenreName = dr["GenreName"].ToString();

                genres.Add(g);
            }

            command.Dispose();
            connection.Close();

            return genres;
        }

        public List<Genre> GetAllDeleted()
        {
            string ConnectionString = Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "Select * from Genre where Deleted=1";

            SqlCommand command = connection.CreateCommand();

            command.CommandText = query;
            DataTable dt = new DataTable("Genre");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            List<Genre> genres = new List<Genre>();
            foreach (DataRow dr in dt.Rows)
            {
                Genre g = new Genre();
                g.Id = int.Parse(dr["Id"].ToString());
                g.GenreName = dr["GenreName"].ToString();

                genres.Add(g);
            }

            command.Dispose();
            connection.Close();

            return genres;
        }

        public Genre GetOne(int Id)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "select * from genre where id=@id";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("id", Id);

            DataTable dt = new DataTable("Genre");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            Genre g = new Genre();
            foreach (DataRow dr in dt.Rows)
            {
                g.Id = int.Parse(dr["Id"].ToString());
                g.GenreName = dr["GenreName"].ToString();

            }

            command.Dispose();
            connection.Close();

            return g;
        }

        public void Update(string genreName, int Id)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "UPDATE Genre SET GenreName=@genreName WHERE id=@Id";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", Id);
            command.Parameters.AddWithValue("genreName", genreName);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        public void Delete(int Id)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "update   Genre set deleted=1 WHERE Id=@Id";

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

            string query = "update   Genre set deleted=0 WHERE Id=@Id";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("Id", Id);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }


        public void Update(Genre genre, int oldId)
        {
            string ConnectionString = this.Configuration.GetConnectionString("MyConnection");
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            string query = "UPDATE Genre SET GenreName=@GenreName WHERE Id=@oldId";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("oldId", oldId);
            command.Parameters.AddWithValue("GenreName", genre.GenreName);


            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
    }
}
