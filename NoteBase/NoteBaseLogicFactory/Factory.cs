using NoteBaseLogic;

namespace NoteBaseLogicFactory
{
    public class Factory
    {
        public static NoteProcessor CreateNoteProcessor()
        {
            return new NoteProcessor();
        }
    }
}