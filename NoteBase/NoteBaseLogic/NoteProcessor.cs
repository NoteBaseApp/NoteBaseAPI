using NoteBaseDALFactory;
using NoteBaseDALInterface;
using NoteBaseInterface;

namespace NoteBaseLogic
{
    public class NoteProcessor : INoteProcessor
    {
        private static INoteDAL NoteDAL = Factory.CreateNoteDAL();
        public NoteProcessor()
        {
            NoteDAL = Factory.CreateNoteDAL();
        }
    }
}