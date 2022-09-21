using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface.Models
{
    public class User
    {
        public int ID { get; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User(int _id, string _name, string _email)
        {
            ID = _id;
            Name = _name;
            Email = _email;
        }
    }
}
