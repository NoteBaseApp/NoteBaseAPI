using NoteBaseDAL;

namespace NoteBaseDALFactory
{
    public class Factory
    {
        public static NoteDAL CreateNoteDAL()
        {
            return new NoteDAL();
        }
    }
}