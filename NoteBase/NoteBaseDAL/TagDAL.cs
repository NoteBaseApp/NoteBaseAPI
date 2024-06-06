using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace NoteBaseDAL
{
    public class TagDAL : ITagDAL
    {
        private readonly string ConnString;

        public TagDAL(string _connString)
        {
            ConnString = _connString;
        }

        public TagDTO Create(string _title)
        {
            TagDTO tagDTO = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), _title);

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"INSERT INTO Tag (Title) VALUES (@Title); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _title);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tagDTO = new(reader.GetGuid(0), _title);
                    }

                    connection.Close();
                }
            }

            return tagDTO;
        }

        public void CreateNoteTag(Guid _noteId, Guid _tagId)
        {
            using (SqlConnection connection = new(ConnString))
            {
                string query = @"INSERT INTO NoteTag (NoteID, TagID) VALUES (@NoteID, @TagID)";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@NoteID", _noteId);
                    command.Parameters.AddWithValue("@TagID", _tagId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int rowsAffected = reader.GetInt32(0);

                        if (rowsAffected == 0)
                        {
                            throw new Exception("NoteTag could not be Created");
                        }
                    }
                    connection.Close();
                }
            }
        }

        public TagDTO GetById(Guid _tagId)
        {
            TagDTO tagDTO = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "");

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title From Tag WHERE ID = @ID";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", _tagId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tagDTO = new TagDTO(reader.GetGuid(0), reader.GetString(1));
                    }

                    connection.Close();
                }
            }

            return tagDTO;
        }

        public List<TagDTO> GetByPerson(Guid _PersonId)
        {
            List<TagDTO> tagDTOList = new();

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title FROM NoteTags WHERE PersonId = @PersonId";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonId", _PersonId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TagDTO tripDTO = new(reader.GetGuid(0), reader.GetString(1));

                        tagDTOList.Add(tripDTO);
                    }

                    connection.Close();
                }
            }

            return tagDTOList;
        }

        public List<TagDTO> GetByNote(Guid _noteId)
        {
            List<TagDTO> tagDTOList = new();

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title FROM NoteTags WHERE NoteID = @NoteId";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@NoteId", _noteId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TagDTO tripDTO = new(reader.GetGuid(0), reader.GetString(1));

                        tagDTOList.Add(tripDTO);
                    }

                    connection.Close();
                }
            }

            return tagDTOList;
        }

        public TagDTO GetByTitle(string _Title)
        {
            TagDTO tagDTO = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "");

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title From Tag WHERE Title = @Title";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _Title);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tagDTO = new TagDTO(reader.GetGuid(0), reader.GetString(1));
                    }

                    connection.Close();
                }
            }

            return tagDTO;
        }

        public TagDTO Update(Guid _tagId, string _title)
        {
            TagDTO tagDTO = new(_tagId, _title);

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"UPDATE Tag SET Title = @Title WHERE ID = @ID";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _title);
                    command.Parameters.AddWithValue("@ID", _tagId);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return tagDTO;
        }

        public void Delete(Guid _tagId)
        {
            using (SqlConnection connection = new(ConnString))
            {
                string query = @"DELETE From Tag WHERE ID = @ID";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", _tagId);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }

        public void DeleteNoteTag(Guid _noteId)
        {
            using (SqlConnection connection = new(ConnString))
            {
                string query = @"DELETE FROM NoteTag WHERE NoteID = @NoteID";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@NoteID", _noteId);
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("NoteTag could not be deleted");
                    }

                    connection.Close();
                }
            }
        }
    }
}
