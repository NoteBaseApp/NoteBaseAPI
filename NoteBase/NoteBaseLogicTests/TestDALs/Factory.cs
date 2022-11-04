using NoteBaseLogic;

namespace NoteBaseLogicTests.TestDALs
{
    internal class Factory
    {
        public static NoteProcessor CreateNoteProcessor()
        {
            return new NoteProcessor(new NoteTestDAL(), CreateTagProcessor());
        }

        public static TagProcessor CreateTagProcessor()
        {
            return new TagProcessor(new TagTestDAL());
        }

        public static PersonProcessor CreatePersonProcessor()
        {
            //return new PersonProcessor(new PersonTestDAL());
            throw new NotImplementedException();
        }

        public static CategoryProcessor CreateCategoryProcessor()
        {
            return new CategoryProcessor(new CategoryTestDAL(), CreateNoteProcessor());
        }
    }
}
