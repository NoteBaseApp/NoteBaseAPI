using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NoteBaseDAL
{
    public class TagDAL : ITagDAL
    {
        private readonly string ConnString;

        public TagDAL(string _connString)
        {
            ConnString = _connString;
        }

        public int Create(TagDTO _tag)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"INSERT INTO Tag (Title) VALUES (@Title)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _tag.Title);
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

        public TagDTO GetById(int _tagId)
        {
            TagDTO result = new(0, "");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title From Tag WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _tagId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = new TagDTO(reader.GetInt32(0), reader.GetString(1));
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

        public List<TagDTO> GetByPerson(int _PersonId)
        {
            List<TagDTO> result = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title FROM NoteTags WHERE PersonId = @PersonId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", _PersonId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            TagDTO tripDTO = new TagDTO(reader.GetInt32(0), reader.GetString(1));

                            result.Add(tripDTO);
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

        public List<TagDTO> GetByNote(int _noteId)
        {
            List<TagDTO> result = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title FROM NoteTags WHERE NoteID = @NoteId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoteId", _noteId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            TagDTO tripDTO = new TagDTO(reader.GetInt32(0), reader.GetString(1));

                            result.Add(tripDTO);
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

        public TagDTO GetByTitle(string _Title)
        {
            TagDTO result = new(0, "");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title From Tag WHERE Title = @Title";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _Title);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                           result = new TagDTO(reader.GetInt32(0), reader.GetString(1));
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

        public int Update(TagDTO _tag)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"UPDATE Tag SET Title = @Title WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _tag.Title);
                        command.Parameters.AddWithValue("@ID", _tag.ID);
                        connection.Open();

                        result = command.ExecuteNonQuery();

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

        public int Delete(int _tagId)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"DELETE From Tag WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _tagId);
                        connection.Open();

                        result = command.ExecuteNonQuery();

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
    }
}
