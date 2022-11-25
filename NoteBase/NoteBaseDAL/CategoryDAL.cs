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

        public DALResponse<CategoryDTO> Create(CategoryDTO _cat)
        {
            DALResponse<CategoryDTO> response = new(true);

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

                        SqlDataReader reader = command.ExecuteReader();


                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "CategoryDAL.Create(" + _cat.Title + ") ERROR: Could not Create Category";
                            }
                        }
                        connection.Close();
                    }
                }
            }
            //het opvangen van een mogelijke error
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.Create(" + _cat.Title + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.Create(" + _cat.Title + ") ERROR: " + e.Message
                };
            }

            return response;
        }
        public DALResponse<CategoryDTO> GetById(int _catId)
        {
            DALResponse<CategoryDTO> response = new(true);

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
                            CategoryDTO categoryDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));

                            response.AddItem(categoryDTO);
                        }
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.GetById(" + _catId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.GetById(" + _catId + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<CategoryDTO> GetByPerson(int _personId)
        {
            DALResponse<CategoryDTO> response = new(true);

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

                            response.AddItem(categoryDTO);
                        }
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.GetByPerson(" + _personId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.GetByPerson(" + _personId + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<CategoryDTO> GetByTitle(string _title)
        {
            DALResponse<CategoryDTO> response = new(true);

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
                            CategoryDTO categoryDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));

                            response.AddItem(categoryDTO);
                        }
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.GetByTitle(" + _title + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.GetByTitle(" + _title + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<CategoryDTO> Update(CategoryDTO _cat)
        {
            DALResponse<CategoryDTO> response = new(true);

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

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "CategoryDAL.Update(" + _cat.ID + ") ERROR: Could not Update Category";
                            }
                        }
                        connection.Close();
                    }
                }
            }
            //het opvangen van een mogelijke error
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.Update(" + _cat.ID + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.Update(" + _cat.ID + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<CategoryDTO> Delete(int _catId)
        {
            DALResponse<CategoryDTO> response = new(false);

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"DELETE FROM Category WHERE ID = @catId";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@catId", _catId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "CategoryDAL.Delete(" + _catId + ") ERROR: Could not Delete Category";
                            }
                        }
                        connection.Close();
                    }
                }
            }
            //het opvangen van een mogelijke error
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.Delete(" + _catId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "CategoryDAL.Delete(" + _catId + ") ERROR: " + e.Message
                };
            }

            return response;
        }

    }
}
