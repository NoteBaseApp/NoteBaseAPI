using NoteBaseLogic;

namespace NoteBaseLogicFactory
{
    public class Factory
    {
        public static NoteProcessor CreateNoteProcessor(string _connString)
        {
            return new NoteProcessor(_connString);
        }
    }
}