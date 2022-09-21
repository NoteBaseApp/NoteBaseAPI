using NoteBaseDAL;
using NoteBaseDALInterface.Models;

namespace NoteBaseDALFactory
{
    public class Factory
    {
        public static NoteDAL CreateNoteDAL(string _connString)
        {
            return new NoteDAL(_connString);
        }

        public static TagDAL CreateTagDAL(string _connString)
        {
            return new TagDAL(_connString);
        }
    }
}