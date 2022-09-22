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
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Delete(int _tagId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Get(int _tagId)
        {
            DALResponse<TagDTO> response = new(200, "");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    // * should be replaced with the collum names
                    string query = @"Select * From Tag WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _tagId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        //while the read() returns a value repeat
                        if (reader.Read())
                        {
                            TagDTO tripDTO = new TagDTO(reader.GetInt32(0), reader.GetString(1));

                            response.AddItem(tripDTO);
                        }
                    }
                }
            }
            //het opvangen van een mogelijke error
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
                    // * should be replaced with the collum names
                    string query = @"SELECT ID, Title FROM UserTags WHERE UserMail = @UserMail";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserMail", _userMail);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        //while the read() returns a value repeat
                        while (reader.Read())
                        {
                            TagDTO tripDTO = new TagDTO(reader.GetInt32(0), reader.GetString(1));

                            response.AddItem(tripDTO);
                        }
                    }
                }
            }
            //het opvangen van een mogelijke error
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
            throw new NotImplementedException();
        }
    }
}
