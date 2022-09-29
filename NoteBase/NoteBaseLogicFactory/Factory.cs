using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseLogic;

namespace NoteBaseLogicFactory
{
    public class Factory
    {
        public static NoteProcessor CreateNoteProcessor(string _connString)
        {
            return new NoteProcessor(_connString);
        }

        public static NoteProcessor CreateNoteProcessor(IDAL<NoteDTO> _noteDAL)
        {
            return new NoteProcessor(_noteDAL);
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