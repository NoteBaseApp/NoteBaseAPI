using NoteBaseLogic;

namespace NoteBaseLogicFactory
{
    public class Factory
    {
        public static NoteProcessor CreateNoteProcessor(string _connString)
        {
            return new NoteProcessor(_connString);
        }

        public static TagProcessor CreateTagProcessor(string _connString)
        {
            return new TagProcessor(_connString);
        }
    }
}