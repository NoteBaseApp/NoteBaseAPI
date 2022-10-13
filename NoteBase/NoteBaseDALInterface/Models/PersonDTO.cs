using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseDALInterface.Models
{
    public class PersonDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public PersonDTO(int _ID, string _Name, string _Email)
        {
            ID = _ID;
            Name = _Name;
            Email = _Email;
        }
    }
}
