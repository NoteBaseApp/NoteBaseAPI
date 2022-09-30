using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseLogic;
using NoteBaseDAL;

namespace NoteBaseLogicFactory
{
    public class Factory
    {
        public static NoteProcessor CreateNoteProcessor(string _connString)
        {
            return new NoteProcessor(_connString);
        }

        public static NoteProcessor CreateNoteProcessor(NoteDAL _noteDAL, TagDAL _tagDAL)
        {
            return new NoteProcessor(_noteDAL, _tagDAL);
        }

        public static TagProcessor CreateTagProcessor(string _connString)
        {
            return new TagProcessor(_connString);
        }

        public static TagProcessor CreateTagProcessor(IDAL<TagDTO> _tagDAL)
        {
            return new TagProcessor(_tagDAL);
        }
    }
}