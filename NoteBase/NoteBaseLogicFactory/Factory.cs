using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseLogic;
using NoteBaseDAL;

namespace NoteBaseLogicFactory
{
    public class ProcessorFactory
    {
        public static NoteProcessor CreateNoteProcessor(INoteDAL _noteDAL, ITagDAL _tagDAL)
        {
            return new NoteProcessor(_noteDAL, _tagDAL);
        }

        public static TagProcessor CreateTagProcessor(ITagDAL _tagDAL)
        {
            return new TagProcessor(_tagDAL);
        }
    }
}