using NoteBaseDAL;
using NoteBaseLogic;

namespace NoteBaseLogicFactory
{
    public class ProcessorFactory
    {
        public static NoteProcessor CreateNoteProcessor(string _connstring)
        {
            return new NoteProcessor(new NoteDAL(_connstring), CreateTagProcessor(_connstring), CreateCategoryProcessor(_connstring));
        }

        public static TagProcessor CreateTagProcessor(string _connstring)
        {
            return new TagProcessor(new TagDAL(_connstring));
        }

        public static PersonProcessor CreatePersonProcessor(string _connstring)
        {
            return new PersonProcessor(new PersonDAL(_connstring));
        }

        public static CategoryProcessor CreateCategoryProcessor(string _connstring)
        {
            return new CategoryProcessor(new CategoryDAL(_connstring), CreateNoteProcessor(_connstring));
        }
    }
}