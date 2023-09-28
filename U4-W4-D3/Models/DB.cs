using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace U4_W4_D3.Models
{
    public static class DB
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
        public static SqlConnection conn = new SqlConnection(connectionString);

        public static List<Scarpa> getProdotti()
        {
            List<Scarpa> lista = new List<Scarpa>();
            SqlCommand cmd = new SqlCommand("select * from Scarpe", conn);
            SqlDataReader sqlDataReader;
            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Scarpa scarpa = new Scarpa();
                scarpa.IdProdotto = Convert.ToInt32(sqlDataReader["IdProdotto"]);
                scarpa.Nome = sqlDataReader["Nome"].ToString();
                scarpa.Descrizione = sqlDataReader["Descrizione"].ToString();
                scarpa.Prezzo = Convert.ToDecimal(sqlDataReader["Prezzo"]);
                scarpa.Image = sqlDataReader["Image"].ToString();
                scarpa.Image1 = sqlDataReader["Image1"].ToString();
                scarpa.Image2 = sqlDataReader["Image2"].ToString();
                scarpa.Disponibile = Convert.ToBoolean(sqlDataReader["Disponibile"]);
                lista.Add(scarpa);

            }
            conn.Close();
            return lista;
        }

        public static List<Utente> getUsers() 
        {
            List<Utente> userList = new List<Utente>();
            SqlCommand cmd = new SqlCommand("select * from Utenti", conn);
            SqlDataReader sqlDataReader;
            conn.Open();
            sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Utente user = new Utente();
                user.IdUtente = Convert.ToInt32(sqlDataReader["IdUtente"]);
                user.Username = sqlDataReader["Username"].ToString();
                user.Password = sqlDataReader["Password"].ToString();
                userList.Add(user);

            }
            conn.Close();
            return userList;
        }

        public static void insertUser(string UsernameL, string PasswordL)
        {
            SqlCommand cmd = new SqlCommand("Insert INTO Utenti values(@Username, @Password)", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("Username", UsernameL);
            cmd.Parameters.AddWithValue("Password", UsernameL);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void insertScarpa(Scarpa s)
        {
            SqlCommand cmd = new SqlCommand("Insert INTO Scarpe Values(@Nome, @Prezzo, @Descrizione, @Image, @Image1, @Image2, @Disponibile)", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("Nome", s.Nome);
            cmd.Parameters.AddWithValue("Prezzo", s.Prezzo);
            cmd.Parameters.AddWithValue("Descrizione", s.Descrizione);
            cmd.Parameters.AddWithValue("Image", s.Image);
            cmd.Parameters.AddWithValue("Image1", s.Image1);
            cmd.Parameters.AddWithValue("Image2", s.Image2);
            cmd.Parameters.AddWithValue("Disponibile", s.Disponibile);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void editScarpa(Scarpa s)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Scarpe SET Nome = @Nome, Prezzo = @Prezzo, Descrizione = @Descrizione, Image = @Image, Image1 = @Image1, Image2 = @Image2, Disponibile = @Disponibile where IdProdotto = @IdProdotto", conn);
            cmd.Parameters.AddWithValue("IdProdotto", s.IdProdotto);
            cmd.Parameters.AddWithValue("Nome", s.Nome);
            cmd.Parameters.AddWithValue("Prezzo", s.Prezzo);
            cmd.Parameters.AddWithValue("Descrizione", s.Descrizione);
            cmd.Parameters.AddWithValue("Image", s.Image);
            cmd.Parameters.AddWithValue("Image1", s.Image1);
            cmd.Parameters.AddWithValue("Image2", s.Image2);
            cmd.Parameters.AddWithValue("Disponibile", s.Disponibile);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void deleteScarpa(int id)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Delete from Scarpe where IdProdotto = @IdProdotto", conn);
            cmd.Parameters.AddWithValue("IdProdotto", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}