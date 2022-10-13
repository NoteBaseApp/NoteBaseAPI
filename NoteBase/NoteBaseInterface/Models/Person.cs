using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Person(int _ID, string _Name, string _Email)
        {
            ID = _ID;
            Name = _Name;
            Email = _Email;
        }
    }
}
