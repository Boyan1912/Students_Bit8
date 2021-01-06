using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public abstract class BaseStudentModel
    {
        public BaseStudentModel(int id, string firstName, string lastName)
        {
            IdStudent = id;
            FirstName = firstName;
            LastName = lastName;
        }
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
    }
}
