using NoteBaseDAL;
using NoteBaseDALFactory;
using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class NoteProcessor : IProcessor<Note>
    {
        private readonly IDAL<NoteDTO> NoteDAL;
        public NoteProcessor(string _connString)
        {
            NoteDAL = Factory.CreateNoteDAL(_connString);
        }

        public NoteProcessor(IDAL<NoteDTO> _noteDAL)
        {
            NoteDAL = _noteDAL;
        }

        public Response<Note> Create(Note _note)
        {
            throw new NotImplementedException();
        }

        public Response<Note> Get(int _noteId)
        {
            throw new NotImplementedException();
        }

        public Response<Note> Get(string _UserMail)
        {
            DALResponse<NoteDTO> DALreponse = NoteDAL.Get(_UserMail);
            Response<Note> response = new(DALreponse.Status, DALreponse.Message);

            foreach (NoteDTO noteDTO in DALreponse.Data)
            {
                Category cat = new(noteDTO.Category.ID, noteDTO.Category.Title);
                Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.MainBody, cat);

                foreach (TagDTO tagDTO in noteDTO.TagList)
                {
                    Tag tag = new(tagDTO.ID, tagDTO.Title);

                    note.TryAddTag(tag);
                }

                response.AddItem(note);
            }

            return response;
        }

        public Response<Note> Update(int _noteId, Note _note)
        {
            throw new NotImplementedException();
        }

        public Response<Note> Delete(int _noteId)
        {
            throw new NotImplementedException();
        }
    }
}