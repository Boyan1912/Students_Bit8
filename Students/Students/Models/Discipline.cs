using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class Discipline
    {
        public int IdDiscipline { get; set; }
        public string Name { get; set; }
        public string ProfessorName { get; set; }
        public float Score { get; set; }
    }
}
