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

        public Response<Person> Create(Person _person)
        {
            throw new NotImplementedException();
        }

        public Response<Person> GetByEmail(string _email)
        {
            DALResponse<PersonDTO> DALreponse = PersonDAL.GetByEmail(_email);

            Response<Person> response = new(DALreponse.Succeeded)
            {
                Message = DALreponse.Message
            };

            Person person = new(DALreponse.Data[0].ID, DALreponse.Data[0].Name, DALreponse.Data[0].Email);
            response.AddItem(person);

            return response;
        }

        public Response<Person> Update(int _personId, Person _person)
        {
            throw new NotImplementedException();
        }

        public Response<Person> Delete(int _personId)
        {
            throw new NotImplementedException();
        }
    }
}
