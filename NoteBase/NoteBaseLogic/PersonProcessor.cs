using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
namespace NoteBaseLogic
{
    public class PersonProcessor: IPersonProcessor
    {
        private readonly IPersonDAL PersonDAL;
        public PersonProcessor(IPersonDAL _personDAL)
        {
            PersonDAL = _personDAL;
        }

        public int Create(string _name, string _email)
        {
            throw new NotImplementedException();
        }

        public Person GetByEmail(string _email)
        {
            PersonDTO personDTO = PersonDAL.GetByEmail(_email);

            return new(personDTO);
        }

        public int Update(int _id, string _name, string _email)
        {
            throw new NotImplementedException();
        }

        public int Delete(int _personId)
        {
            throw new NotImplementedException();
        }
    }
}
