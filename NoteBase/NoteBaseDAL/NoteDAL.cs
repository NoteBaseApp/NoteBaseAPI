using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System.Data.SqlClient;

namespace NoteBaseDAL
{
    public class NoteDAL : INoteDAL
    {
        private readonly string ConnString;
        private TagDAL TagDAL;

        public NoteDAL(string _connString)
        {
            ConnString = _connString;
            TagDAL = new TagDAL(_connString);
        }

        public int Create(NoteDTO _note)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"INSERT INTO Note (Title, Text, CategoryID, PersonId) VALUES (@Title, @Text, @CategoryID, @PersonId)";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _note.Title);
                        command.Parameters.AddWithValue("@Text", _note.Text);
                        command.Parameters.AddWithValue("@CategoryID", _note.CategoryId);
                        command.Parameters.AddWithValue("@PersonId", _note.PersonId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
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

        public int CreateNoteTag(int _noteId, int _tagId)
        {
            int result = 0;

            try
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
                            result = reader.GetInt32(0);
                        }
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

        public NoteDTO GetById(int _noteId)
        {
            NoteDTO result = new(0, "", "", 0);

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"SELECT ID, Title, Text, CategoryID From Note WHERE ID = @ID";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _noteId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 0);

                            List<TagDTO> tags = TagDAL.GetByNote(noteDTO.ID);

                            foreach (TagDTO tagDTO in tags)
                            {
                                noteDTO.TryAddTagDTO(tagDTO);
                            }

                            result = noteDTO;
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

        public List<NoteDTO> GetByPerson(int _personId)
        {
            List<NoteDTO> result = new();

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"SELECT ID, Title, Text, CategoryId FROM Note WHERE PersonId = @PersonId";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", _personId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 0);

                            List<TagDTO> tags = TagDAL.GetByNote(noteDTO.ID);

                            foreach (TagDTO tagDTO in tags)
                            {
                                noteDTO.TryAddTagDTO(tagDTO);
                            }

                            result.Add(noteDTO);
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

        public NoteDTO GetByTitle(string _title)
        {
            NoteDTO result = new(0, "", "", 0);

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"SELECT ID, Title, Text, CategoryID From Note WHERE Title = @Title";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _title);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 0);

                            List<TagDTO> tags = TagDAL.GetByNote(noteDTO.ID);

                            foreach (TagDTO tagDTO in tags)
                            {
                                noteDTO.TryAddTagDTO(tagDTO);
                            }

                            result = noteDTO;
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

        public List<NoteDTO> GetByCategory(int _catId)
        {
            List<NoteDTO> result = new();

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"SELECT ID, Title, Text, CategoryId FROM Note WHERE CategoryId = @categoryId";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@categoryId", _catId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 0);

                            List<TagDTO> tags = TagDAL.GetByNote(noteDTO.ID);

                            foreach (TagDTO tagDTO in tags)
                            {
                                noteDTO.TryAddTagDTO(tagDTO);
                            }

                            result.Add(noteDTO);
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

        public List<NoteDTO> GetByTag(int _tagId)
        {
            List<NoteDTO> result = new();

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"SELECT ID, Title, Text, CategoryId FROM TagNotes WHERE TagID = @tagId";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@tagId", _tagId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 0);

                            List<TagDTO> tags = TagDAL.GetByNote(noteDTO.ID);

                            foreach (TagDTO tagDTO in tags)
                            {
                                noteDTO.TryAddTagDTO(tagDTO);
                            }

                            result.Add(noteDTO);
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

        public int Update(NoteDTO _note)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"UPDATE Note SET Title = @Title, Text = @Text, CategoryID = @CategoryID WHERE ID = @ID";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _note.Title);
                        command.Parameters.AddWithValue("@Text", _note.Text);
                        command.Parameters.AddWithValue("@CategoryID", _note.CategoryId);
                        command.Parameters.AddWithValue("@ID", _note.ID);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
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

        public int Delete(int _noteId)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"DELETE FROM Note WHERE ID = @ID";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _noteId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
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

        public int DeleteNoteTag(int _noteId)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new(ConnString))
                {
                    string query = @"DELETE FROM NoteTag WHERE NoteID = @NoteID";

                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoteID", _noteId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
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