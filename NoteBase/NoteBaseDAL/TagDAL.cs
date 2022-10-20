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

        public DALResponse<TagDTO> Create(TagDTO _tag)
        {
            DALResponse<TagDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"INSERT INTO Tag (Title) VALUES (@Title)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _tag.Title);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "TagDAL.Create(" + _tag.Title + ") ERROR: Could not Create Tag";
                            }
                        }
                    }
                }
            }
            //het opvangen van een mogelijke error
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Create(" + _tag.Title + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Create(" + _tag.Title + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<TagDTO> Delete(int _tagId)
        {
            DALResponse<TagDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"DELETE From Tag WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _tagId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "TagDAL.Delete(" + _tagId + ") ERROR: Could not delete Tag";
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Delete(" + _tagId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Delete(" + _tagId + ") ERROR: " + e.Message,
                };
            }

            return response;
        }

        public DALResponse<TagDTO> Get(int _tagId)
        {
            DALResponse<TagDTO> response = new(true);

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
                            TagDTO tripDTO = new TagDTO(reader.GetInt32(0), reader.GetString(1));

                            response.AddItem(tripDTO);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Get(" + _tagId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Get(" + _tagId + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<TagDTO> Get(string _userMail)
        {
            DALResponse<TagDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title FROM NoteTags WHERE UserMail = @UserMail";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserMail", _userMail);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            TagDTO tripDTO = new TagDTO(reader.GetInt32(0), reader.GetString(1));

                            response.AddItem(tripDTO);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Get(" + _userMail + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Get(" + _userMail + ") ERROR: " + e.Message,
                };
            }

            return response;
        }

        public DALResponse<TagDTO> GetFromNote(int _noteId)
        {
            DALResponse<TagDTO> response = new(true);

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

                            response.AddItem(tripDTO);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Get(" + _noteId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Get(" + _noteId + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<TagDTO> Update(int _tagId, TagDTO _tag)
        {
            DALResponse<TagDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"UPDATE Tag SET Title = @Title WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _tag.Title);
                        command.Parameters.AddWithValue("@ID", _tagId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "TagDAL.Update(" + _tagId + ",TagDTO) ERROR: Could not update Tag";
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Update(" + _tagId + ", TagDTO) ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "TagDAL.Update(" + _tagId + ", TagDTO) ERROR: " + e.Message
                };
            }

            return response;
        }
        public DALResponse<TagDTO> GetByTitle(string _Title)
        {
            DALResponse<TagDTO> response = new(true);

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
                            TagDTO tripDTO = new TagDTO(reader.GetInt32(0), reader.GetString(1));

                            response.AddItem(tripDTO);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "TagDAL.GetByTitle(" + _Title + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "TagDAL.GetByTitle(" + _Title + ") ERROR: " + e.Message
                };
            }

            return response;
        }
    }
}
