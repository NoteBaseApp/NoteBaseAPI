using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System.Data.SqlClient;

namespace NoteBaseDAL
{
    public class NoteDAL : INoteDAL
    {
        private readonly string ConnString;
        private TagDAL tagDAL;

        public NoteDAL(string _connString)
        {
            ConnString = _connString;
            tagDAL = new TagDAL(_connString);
        }

        public DALResponse<NoteDTO> Create(NoteDTO _note)
        {
            DALResponse<NoteDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"INSERT INTO Note (Title, Text, CategoryID, PersonId) VALUES (@Title, @Text, @CategoryID, @PersonId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _note.Title);
                        command.Parameters.AddWithValue("@Text", _note.Text);
                        command.Parameters.AddWithValue("@CategoryID", _note.Category.ID);
                        command.Parameters.AddWithValue("@PersonId", _note.PersonId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "NoteDAL.Create(" + _note.Title + ") ERROR: Could not Create Note";
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
                    Message = "NoteDAL.Create(" + _note.Title + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "NoteDAL.Create(" + _note.Title + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<NoteDTO> CreateNoteTag(int _noteId, int _tagId)
        {
            DALResponse<NoteDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"INSERT INTO NoteTag (NoteID, TagID) VALUES (@NoteID, @TagID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NoteID", _noteId);
                        command.Parameters.AddWithValue("@TagID", _tagId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int result = reader.GetInt32(0);
                            if (result == 0)
                            {
                                response.Succeeded = false;
                                response.Message = "NoteDAL.CreateNoteTag(" + _noteId + ", " + _tagId + ") ERROR: Could not Create Tag";
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
                    Message = "NoteDAL.CreateNoteTag(" + _noteId + ", " + _tagId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "NoteDAL.CreateNoteTag(" + _noteId + ", " + _tagId + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<NoteDTO> GetById(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> GetByPerson(int _personId)
        {
            DALResponse<NoteDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title, Text, CategoryId FROM Note WHERE PersonId = @PersonId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", _personId);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), new(0, "", 0));

                            DALResponse<TagDTO> DALResponse = tagDAL.GetFromNote(noteDTO.ID);

                            foreach (TagDTO tagDTO in DALResponse.Data)
                            {
                                noteDTO.TryAddTagDTO(tagDTO);
                            }

                            response.AddItem(noteDTO);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "NoteDAL.Get(" + _personId + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "NoteDAL.Get(" + _personId + ") ERROR: " + e.Message
                };
            }

            return response;
        }

        public DALResponse<NoteDTO> GetByTitle(string _Title)
        {
            DALResponse<NoteDTO> response = new(true);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title, Text, CategoryID From Note WHERE Title = @Title";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _Title);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), new(0, "", 0));

                            DALResponse<TagDTO> DALResponse = tagDAL.GetFromNote(noteDTO.ID);

                            foreach (TagDTO tagDTO in DALResponse.Data)
                            {
                                noteDTO.TryAddTagDTO(tagDTO);
                            }

                            response.AddItem(noteDTO);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                response = new(false)
                {
                    Message = "NoteDAL.GetByTitle(" + _Title + ") ERROR: " + e.Message,
                    Code = e.Number
                };
            }
            catch (Exception e)
            {
                response = new(false)
                {
                    Message = "NoteDAL.GetByTitle(" + _Title + ") ERROR: " + e.Message,
                };
            }

            return response;
        }

        public DALResponse<NoteDTO> Update(NoteDTO _note)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Delete(int _noteId)
        {
            throw new NotImplementedException();
        }
    }
}