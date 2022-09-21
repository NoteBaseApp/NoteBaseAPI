using NoteBaseDAL;

namespace NoteBaseDALFactory
{
    public class Factory
    {
        public static NoteDAL CreateNoteDAL(string _connString)
        {
            return new NoteDAL(_connString);
        }
    }
}