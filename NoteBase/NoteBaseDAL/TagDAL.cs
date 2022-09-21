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
                    string query = @"Select * From Trip WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _tagId);
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
                response = new(e.Number, e.Message);
            }

            return response;
        }

        public DALResponse<TagDTO> Get()
        {
            DALResponse<TagDTO> response = new(200, "");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    // * should be replaced with the collum names
                    string query = @"Select * From Tag";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
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
                response = new(e.Number, e.Message);
            }

            return response;
        }

        public DALResponse<TagDTO> Update(int _tagId, TagDTO _tag)
        {
            throw new NotImplementedException();
        }
    }
}
