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
            Response<Note> noteReponse = new(false);

            if (_note.CategoryId == 0)
            {
                noteReponse.Message = "No valid category was given";

                return noteReponse;
            }

            _note = AddTags(_note);
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.Create(_note.ToDTO());
            if (noteDALreponse.Succeeded)
            {
                noteReponse = GetByTitle(_note.Title);
            }

            Response<Note> response = new(noteDALreponse.Succeeded)
            {
                Message = noteDALreponse.Message,
                Code = noteDALreponse.Code
            };

            foreach (Tag tag in _note.TagList)
            {
                Response<Tag> tagResponse = TagProcessor.GetByTitle(tag.Title);

                if (tagResponse.Data.Count == 0)
                {
                    TagProcessor.Create(tag);
                    tagResponse = TagProcessor.GetByTitle(tag.Title);
                }

                if (tagResponse.Data.Count  == 0)
                {
                    response.Succeeded = tagResponse.Succeeded;
                    response.Message = tagResponse.Message;

                    return response;
                }
                else if (noteReponse.Data.Count == 0)
                {
                    response.Succeeded = noteReponse.Succeeded;
                    response.Message = noteReponse.Message;

                    return response;
                }
                NoteDAL.CreateNoteTag(noteReponse.Data[0].ID, tagResponse.Data[0].ID);
            }

            noteReponse = GetByTitle(_note.Title);

            response.AddItem(noteReponse.Data[0]);

            return response;
        }

        //what if somebody usses a tag with a hashtag in it like #C#
        //aslo move this to the Note Class
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

        public Response<Note> GetById(int _noteId)
        {
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.GetById(_noteId);

            Response<Note> response = new(noteDALreponse.Succeeded)
            {
                Message = noteDALreponse.Message,
                Code = noteDALreponse.Code
            };

            if (!noteDALreponse.Succeeded || noteDALreponse.Data.Count == 0)
            {
                return response;
            }

            NoteDTO noteDTO = noteDALreponse.Data[0];

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

            return response;
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


        public Response<Note> GetByTitle(string _Title)
        {
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.GetByTitle(_Title);

            Response<Note> response = new(noteDALreponse.Succeeded)
            {
                Message = noteDALreponse.Message,
                Code = noteDALreponse.Code
            };

            NoteDTO noteDTO = noteDALreponse.Data[0];

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

            return response;
        }

        public Response<Note> GetByCategory(int _catId)
        {
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.GetByCategory(_catId);

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

        public Response<Note> Update(Note _note)
        {
            Response<Note> notereponse = new(false);

            if (_note.CategoryId == 0)
            {
                notereponse.Message = "No valid category was given";

                return notereponse;
            }

            _note = AddTags(_note);
            DALResponse<NoteDTO> noteDALreponse = NoteDAL.Update(_note.ToDTO());
            if (noteDALreponse.Succeeded)
            {
                notereponse = GetByTitle(_note.Title);
            }

            Response<Note> response = new(noteDALreponse.Succeeded)
            {
                Message = noteDALreponse.Message,
                Code = noteDALreponse.Code
            };

            NoteDAL.DeleteNoteTag(_note.ID);

            foreach (Tag tag in _note.TagList)
            {
                TagProcessor.Create(tag);

                Response<Tag> tagResponse = TagProcessor.GetByTitle(tag.Title);

                if (tagResponse.Data.Count == 0)
                {
                    response.Succeeded = tagResponse.Succeeded;
                    response.Message = tagResponse.Message;

                    return response;
                }
                else if (notereponse.Data.Count == 0)
                {
                    response.Succeeded = notereponse.Succeeded;
                    response.Message = notereponse.Message;

                    return response;
                }

                //update notetag table
                NoteDAL.CreateNoteTag(_note.ID, tagResponse.Data[0].ID);
            }

            notereponse = GetById(_note.ID);

            response.AddItem(notereponse.Data[0]);

            return response;
        }

        public Response<Note> Delete(Note _note, int _PersonId)
        {
            Response<Note> response = new(false);

            Response<Note> noteReponse = GetById(_note.ID);
            if (noteReponse.Data.Count == 0)
            {
                response.Message = "Note doesn't exist";

                return response;
            }


            DALResponse<NoteDTO> noteDalReponse = NoteDAL.DeleteNoteTag(_note.ID);
            if (!noteDalReponse.Succeeded)
            {
                response = new(noteDalReponse.Succeeded)
                {
                    Message = noteDalReponse.Message,
                    Code = noteDalReponse.Code
                };

                return response;
            }

            noteDalReponse = NoteDAL.Delete(_note.ID);

            if (!noteDalReponse.Succeeded)
            {
                response = new(noteDalReponse.Succeeded)
                {
                    Message = noteDalReponse.Message,
                    Code = noteDalReponse.Code
                };

                return response;
            }

            foreach (Tag tag in _note.TagList)
            {
                TagProcessor.TryDelete(tag.ID, _PersonId);
            }

            return response;
        }

        
    }
}