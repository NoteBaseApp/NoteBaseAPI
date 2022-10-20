using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseLogic;
using NoteBaseDAL;

namespace NoteBaseLogicFactory
{
    public class ProcessorFactory
    {
        public static NoteProcessor CreateNoteProcessor(INoteDAL _noteDAL, ITagDAL _tagDAL, ICategoryDAL _categoryDAL)
        {
            return new NoteProcessor(_noteDAL, _tagDAL, _categoryDAL);
        }

        public static TagProcessor CreateTagProcessor(ITagDAL _tagDAL)
        {
            return new TagProcessor(_tagDAL);
        }

        public static PersonProcessor CreatePersonProcessor(IPersonDAL _personDAL)
        {
            return new PersonProcessor(_personDAL);
        }

        public static CategoryProcessor CreateCategoryProcessor(ICategoryDAL _categoryDAL)
        {
            return new CategoryProcessor(_categoryDAL);
        }
    }
}