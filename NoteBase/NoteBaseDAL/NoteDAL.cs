using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System.Data.SqlClient;

namespace NoteBaseDAL
{
    public class NoteDAL : INoteDAL
    {
        private readonly string ConnString;
        private readonly TagDAL TagDAL;

        public NoteDAL(string _connString)
        {
            ConnString = _connString;
            TagDAL = new(_connString);
        }

        public NoteDTO Create(string _title, string _text, Guid _categoryId, Guid _personId)
        {
            NoteDTO noteDTO = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), _title, _text, _categoryId, _personId);

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"INSERT INTO Note (Title, Text, CategoryID, PersonId) output INSERTED.ID VALUES (@Title, @Text, @CategoryID, @PersonId);";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _title);
                    command.Parameters.AddWithValue("@Text", _text);
                    command.Parameters.AddWithValue("@CategoryID", _categoryId);
                    command.Parameters.AddWithValue("@PersonId", _personId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        noteDTO = new(reader.GetGuid(0), _title, _text, _categoryId, _personId);
                    }

                    connection.Close();
                }
            }

            return noteDTO;
        }

        public NoteDTO GetById(Guid _noteId)
        {
            NoteDTO noteDTO = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "", "", Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("00000000-0000-0000-0000-000000000000"));

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, Text, CategoryID, PersonId From Note WHERE ID = @ID";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", _noteId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        noteDTO = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3), reader.GetGuid(4));

                        List<TagDTO> tagDTOList = TagDAL.GetByNote(noteDTO.ID);

                        foreach (TagDTO tagDTO in tagDTOList)
                        {
                            noteDTO.tagList.Add(tagDTO);
                        }
                    }
                    connection.Close();
                }
            }

            return noteDTO;
        }

        public List<NoteDTO> GetByPerson(Guid _personId)
        {
            List<NoteDTO> noteDTOList = new();

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, Text, CategoryId, PersonId FROM Note WHERE PersonId = @PersonId";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonId", _personId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        NoteDTO noteDTO = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3), reader.GetGuid(4));

                        List<TagDTO> tagDTOList = TagDAL.GetByNote(noteDTO.ID);

                        foreach (TagDTO tagDTO in tagDTOList)
                        {
                            noteDTO.tagList.Add(tagDTO);
                        }

                        noteDTOList.Add(noteDTO);
                    }
                    connection.Close();
                }
            }

            return noteDTOList;
        }

        public NoteDTO GetByTitle(string _title)
        {
            NoteDTO noteDTO = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "", "", Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("00000000-0000-0000-0000-000000000000"));

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, Text, CategoryID, PersonId From Note WHERE Title = @Title";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _title);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        noteDTO = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3), reader.GetGuid(4));

                        List<TagDTO> tagDTOList = TagDAL.GetByNote(noteDTO.ID);

                        foreach (TagDTO tagDTO in tagDTOList)
                        {
                            noteDTO.tagList.Add(tagDTO);
                        }
                    }
                    connection.Close();
                }
            }

            return noteDTO;
        }

        public List<NoteDTO> GetByCategory(Guid _catId)
        {
            List<NoteDTO> noteDTOList = new();

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, Text, CategoryId, PersonId FROM Note WHERE CategoryId = @categoryId";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", _catId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        NoteDTO noteDTO = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3), reader.GetGuid(4));

                        List<TagDTO> tags = TagDAL.GetByNote(noteDTO.ID);

                        foreach (TagDTO tagDTO in tags)
                        {
                            noteDTO.tagList.Add(tagDTO);
                        }

                        noteDTOList.Add(noteDTO);
                    }
                    connection.Close();
                }
            }

            return noteDTOList;
        }

        public List<NoteDTO> GetByTag(Guid _tagId)
        {
            List<NoteDTO> noteDTOList = new();

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"SELECT ID, Title, Text, CategoryId, PersonId FROM TagNotes WHERE TagID = @tagId";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@tagId", _tagId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        NoteDTO noteDTO = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetGuid(3), reader.GetGuid(4));

                        List<TagDTO> tags = TagDAL.GetByNote(noteDTO.ID);

                        foreach (TagDTO tagDTO in tags)
                        {
                            noteDTO.tagList.Add(tagDTO);
                        }

                        noteDTOList.Add(noteDTO);
                    }
                    connection.Close();
                }
            }

            return noteDTOList;
        }

        public NoteDTO Update(Guid _id, string _title, string _text, Guid _categoryId)
        {
            NoteDTO NoteDTO = new(_id, _title, _text, _categoryId, Guid.Parse("00000000-0000-0000-0000-000000000000"));

            using (SqlConnection connection = new(ConnString))
            {
                string query = @"UPDATE Note SET Title = @Title, Text = @Text, CategoryID = @CategoryID WHERE ID = @ID;";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", _title);
                    command.Parameters.AddWithValue("@Text", _text);
                    command.Parameters.AddWithValue("@CategoryID", _categoryId);
                    command.Parameters.AddWithValue("@ID", _id);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return NoteDTO;
        }

        public void Delete(Guid _noteId)
        {
            using (SqlConnection connection = new(ConnString))
            {
                string query = @"DELETE FROM Note WHERE ID = @ID";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", _noteId);
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    //is dit nodig?
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Note could not be deleted");
                    }

                    connection.Close();
                }
            }
        }
    }
}