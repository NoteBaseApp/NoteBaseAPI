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
    public class CategoryDAL : ICategoryDAL
    {
        private readonly string ConnString;

        public CategoryDAL(string _connString)
        {
            ConnString = _connString;
        }

        public int Create(CategoryDTO _cat)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"INSERT INTO Category (Title, PersonId) VALUES (@Title, @PersonId)";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _cat.Title);
                        command.Parameters.AddWithValue("@PersonId", _cat.PersonId);
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
        public CategoryDTO GetById(int _catId)
        {
            CategoryDTO result = new(0, "", 0);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title, PersonId FROM Category WHERE ID = @catId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@catId", _catId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
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

        public List<CategoryDTO> GetByPerson(int _personId)
        {
            List<CategoryDTO> result = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title, PersonId FROM Category WHERE PersonId = @PersonId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", _personId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            CategoryDTO categoryDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));

                            result.Add(categoryDTO);
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

        public CategoryDTO GetByTitle(string _title)
        {
            CategoryDTO result = new(0, "", 0);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title, PersonId FROM Category WHERE Title = @Title";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _title);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
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

        public int Update(CategoryDTO _cat)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"UPDATE Category SET Title = @Title WHERE ID = @catId";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _cat.Title);
                        command.Parameters.AddWithValue("@catId", _cat.ID);
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

        public int Delete(int _catId)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"DELETE FROM Category WHERE ID = @catId";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@catId", _catId);
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

    }
}
