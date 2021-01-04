using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class Semester
    {
        public int IdSemester { get; set; }
        public string Name { get; set; }
        public List<Discipline> Disciplines { get; set; }
        public bool HasStudents { get; set; }
    }
}
