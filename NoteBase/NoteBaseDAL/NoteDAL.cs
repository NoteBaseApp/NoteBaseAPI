using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System.Data.SqlClient;

namespace NoteBaseDAL
{
    public class NoteDAL : IDAL<NoteDTO>
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
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Delete(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Get(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Get(string _userMail)
        {
            DALResponse<NoteDTO> response = new(200, "");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString))
                {
                    string query = @"SELECT ID, Title, MainBody, CategoryID From Note WHERE UserMail = @UserMail";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserMail", _userMail);
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            NoteDTO noteDTO = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), new(0, ""));

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
                response = new(e.Number, "NoteDAL.Get(" + _userMail + ") ERROR: " + e.Message);
            }
            catch (Exception e)
            {
                response = new(409, "NoteDAL.Get(" + _userMail + ") ERROR: " + e.Message);
            }

            return response;
        }

        public DALResponse<NoteDTO> Update(int _noteId, NoteDTO _note)
        {
            throw new NotImplementedException();
        }
    }
}