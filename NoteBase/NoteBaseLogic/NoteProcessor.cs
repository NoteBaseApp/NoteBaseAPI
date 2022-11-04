using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseInterface;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class NoteProcessor : INoteProcessor
    {
        private readonly INoteDAL NoteDAL;
        private readonly ITagProcessor TagProcessor;
        public NoteProcessor(INoteDAL _noteDAL, ITagProcessor _tagProcessor)
        {
            NoteDAL = _noteDAL;
            TagProcessor = _tagProcessor;
        }

        public Response<Note> Create(Note _note)
        {
            _note = AddTags(_note);
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.Create(_note.ToDTO());
            string tempMessage = noteDALreponse.Message;
            noteDALreponse = NoteDAL.GetByTitle(_note.Title);
            noteDALreponse.Message = tempMessage;

            Response<Note> response = new(noteDALreponse.Succeeded)
            {
                Message = noteDALreponse.Message,
                Code = noteDALreponse.Code
            };

            foreach (Tag tag in _note.TagList)
            {
                TagProcessor.Create(tag);

                Response<Tag> tagResponse = TagProcessor.GetByTitle(tag.Title);

                if (tagResponse.Data.Count  == 0)
                {
                    response.Succeeded = tagResponse.Succeeded;
                    response.Message = tagResponse.Message;

                    return response;
                }
                else if (noteDALreponse.Data.Count == 0)
                {
                    response.Succeeded = noteDALreponse.Succeeded;
                    response.Message = noteDALreponse.Message;

                    return response;
                }
                NoteDAL.CreateNoteTag(noteDALreponse.Data[0].ID, tagResponse.Data[0].ID);
            }

            return response;
        }

        public Note AddTags(Note _note)
        {
            string[] allWords = _note.Text.Split(" ");
            for (int i = 0; i < allWords.Length; i++)
            {
                string word = allWords[i];
                if (word.StartsWith("#"))
                {
                    Tag tag = new(i, word.Substring(1).ToLower());
                    if (_note.IsTagCompatible(tag))
                    {
                        _note.TryAddTag(tag);
                    }
                }
            }

            return _note;
        }

        public Response<Note> Get(int _noteId)
        {
            throw new NotImplementedException();
        }

        public Response<Note> GetByPerson(int _personId)
        {
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.GetByPerson(_personId);

            Response<Note> response = new(noteDALreponse.Succeeded)
            {
                Message = noteDALreponse.Message,
                Code = noteDALreponse.Code
            };

            foreach (NoteDTO noteDTO in noteDALreponse.Data)
            {
                Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId);

                foreach (TagDTO tagDTO in noteDTO.TagList)
                {
                    Tag tag = new(tagDTO.ID, tagDTO.Title);

                    if (note.IsTagCompatible(tag))
                    {
                        note.TryAddTag(tag);
                    }
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

        public Response<Note> GetByCategory(int _categoryId)
        {
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.GetByCategory(_categoryId);

            Response<Note> response = new(noteDALreponse.Succeeded)
            {
                Message = noteDALreponse.Message,
                Code = noteDALreponse.Code
            };

            foreach (NoteDTO noteDTO in noteDALreponse.Data)
            {
                Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId);

                foreach (TagDTO tagDTO in noteDTO.TagList)
                {
                    Tag tag = new(tagDTO.ID, tagDTO.Title);

                    if (note.IsTagCompatible(tag))
                    {
                        note.TryAddTag(tag);
                    }
                }

                response.AddItem(note);
            }

            return response;
        }
    }
}