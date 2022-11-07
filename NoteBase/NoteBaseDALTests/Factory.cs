using NoteBaseDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseDALTests
{
    internal static class Factory
    {
        private static string connstring = "Data Source=LAPTOP-AK9JEN2V;Initial Catalog=NoteBaseTesting;Integrated Security=True;Connect Timeout=300;";

        public static string getConnectionString()
        {
            return connstring;
        }

        public static NoteDAL CreateNoteDAL()
        {
            return new NoteDAL(connstring);
        }

        public static TagDAL CreateTagDAL()
        {
            return new TagDAL(connstring);
        }

        public static PersonDAL CreatePersonDAL()
        {
            //return new PersonProcessor();
            throw new NotImplementedException();
        }

        public static CategoryDAL CreateCategoryDAL()
        {
            return new CategoryDAL(connstring);
        }
    }
}
