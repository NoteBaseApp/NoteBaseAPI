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

        public CategoryDTO Create(string _title, int _personId)
        {
            CategoryDTO result = new(0, _title, _personId);

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"INSERT INTO Category (Title, PersonId) VALUES (@Title, @PersonId); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _title);
                    command.Parameters.AddWithValue("@PersonId", _personId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new((Int32)reader.GetDecimal(0), _title, _personId);
                    }

                    connection.Close();
                }
            }

            return result;
        }
        public CategoryDTO GetById(int _catId)
        {
            CategoryDTO result = new(0, "", 0);

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, PersonId FROM Category WHERE ID = @catId";

                using (SqlCommand command = new(query, connection))
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

            return result;
        }

        public List<CategoryDTO> GetByPerson(int _personId)
        {
            List<CategoryDTO> result = new();

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, PersonId FROM Category WHERE PersonId = @PersonId";

                using (SqlCommand command = new(query, connection))
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

            return result;
        }

        public CategoryDTO GetByTitle(string _title)
        {
            CategoryDTO result = new(0, "", 0);

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, PersonId FROM Category WHERE Title = @Title";

                using (SqlCommand command = new(query, connection))
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

            return result;
        }

        public CategoryDTO Update(int _id, string _title, int _personId)
        {
            CategoryDTO result = new(_id, _title, _personId);

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"UPDATE Category SET Title = @Title WHERE ID = @catId";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _title);
                    command.Parameters.AddWithValue("@catId", _id);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return result;
        }

        public void Delete(int _catId)
        {

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"DELETE FROM Category WHERE ID = @catId";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@catId", _catId);
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    //is dit nodig?
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Category could not be deleted");
                    }

                    connection.Close();
                }
            }
        }

    }
}
