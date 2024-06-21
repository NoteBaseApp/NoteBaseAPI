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

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"INSERT INTO Person (Name, Email) output INSERTED.ID VALUES (@Name, @Email)";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", _name);
                    command.Parameters.AddWithValue("@Email", _email);
                    connection.Open();

                    result = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return result;
        }

        public PersonDTO GetByEmail(string _personEmail)
        {
            PersonDTO result = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "", "");

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Name, Email FROM Person WHERE Email = @Email";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", _personEmail);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                    }

                    connection.Close();

                }
            }

            return result;
        }

        public int Update(Guid _id, string _name, string _email)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid _personId)
        {
            throw new NotImplementedException();
        }
    }
}
