using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseDAL
{
    public class PersonDAL : IPersonDAL
    {
        private readonly string ConnString;

        public PersonDAL(string _connString)
        {
            ConnString = _connString;
        }

        public int Create(string _name, string _email)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"INSERT INTO Person (Name, Email) VALUES (@Name, @Email)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", _name);
                        command.Parameters.AddWithValue("@Email", _email);
                        connection.Open();

                        result = command.ExecuteNonQuery();

                        connection.Close();
                    }
                }
            }
            //het opvangen van een mogelijke error
            catch (SqlException e)
            {
                throw new Exception("de volgende error is opgetreden " + e.Number + "\n" + e.Message);
            }

            return result;
        }

        public PersonDTO GetByEmail(string _personEmail)
        {
            PersonDTO result = new(0, "", "");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Name, Email FROM Person WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", _personEmail);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        }

                        connection.Close();

                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception("de volgende error is opgetreden " + e.Number + "\n" + e.Message);
            }

            return result;
        }

        public int Update(int _id, string _name, string _email)
        {
            throw new NotImplementedException();
        }

        public int Delete(int _personId)
        {
            throw new NotImplementedException();
        }
    }
}
