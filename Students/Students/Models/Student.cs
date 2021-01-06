using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class Student : BaseStudentModel
    {
        public Student(int id, string firstName, string lastName) : base(id, firstName, lastName)
        {

        }

        public List<Semester> Semesters { get; set; }
    }
}
