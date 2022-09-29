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
    public class TagDAL : IDAL<TagDTO>
    {
        private readonly string ConnString;

        public TagDAL(string _connString)
        {
            ConnString = _connString;
        }

        public DALResponse<TagDTO> Create(TagDTO _tag)
        {
            DALResponse<TagDTO> response = new(200, "");

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
                                response.Status = 409;
                                response.Message = "TagDAL.Create(" + _tag.Title + ") ERROR: Could not Create Tag";
                            }
                        }
                    }
                }
            }
            //het opvangen van een mogelijke error
            catch (SqlException e)
            {
                response = new(e.Number, "TagDAL.Create(" + _tag.Title + ") ERROR: " + e.Message);
            }
            catch (Exception e)
            {
                response = new(409, "TagDAL.Create(" + _tag.Title + ") ERROR: " + e.Message);
            }

            return response;
        }

        public DALResponse<TagDTO> Delete(int _tagId)
        {
            DALResponse<TagDTO> response = new(200, "");

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
                                response.Status = 409;
                                response.Message = "TagDAL.Delete(" + _tagId + ") ERROR: Could not delete Tag";
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(e.Number, "TagDAL.Delete(" + _tagId + ") ERROR: " + e.Message);
            }
            catch (Exception e)
            {
                response = new(409, "TagDAL.Delete(" + _tagId + ") ERROR: " + e.Message);
            }

            return response;
        }

        public DALResponse<TagDTO> Get(int _tagId)
        {
            DALResponse<TagDTO> response = new(200, "");

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
                response = new(e.Number, "TagDAL.Get(" + _tagId + ") ERROR: " + e.Message);
            }
            catch (Exception e)
            {
                response = new(409, "TagDAL.Get(" + _tagId + ") ERROR: " + e.Message);
            }

            return response;
        }

        public DALResponse<TagDTO> Get(string _userMail)
        {
            DALResponse<TagDTO> response = new(200, "");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title FROM UserTags WHERE UserMail = @UserMail";

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
                response = new(e.Number, "TagDAL.Get() ERROR: " + e.Message);
            }
            catch (Exception e)
            {
                response = new(409, "TagDAL.Get() ERROR: " + e.Message);
            }

            return response;
        }

        public DALResponse<TagDTO> Update(int _tagId, TagDTO _tag)
        {
            DALResponse<TagDTO> response = new(200, "");

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
                                response.Status = 409;
                                response.Message = "TagDAL.Update(" + _tagId + ",TagDTO) ERROR: Could not update Tag";
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(e.Number, "TagDAL.Update(" + _tagId + ", TagDTO) ERROR: " + e.Message);
            }
            catch (Exception e)
            {
                response = new(409, "TagDAL.Update(" + _tagId + ",TagDTO) ERROR: " + e.Message);
            }

            return response;
        }
    }
}
