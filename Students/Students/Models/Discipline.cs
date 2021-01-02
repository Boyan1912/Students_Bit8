using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class Discipline
    {
        public Discipline(int id, string name, string professor, float? score = null)
        {
            IdDiscipline = id;
            Name = name;
            ProfessorName = professor;
            Score = score;
        }
        public int IdDiscipline { get; set; }
        public string Name { get; set; }
        public string ProfessorName { get; set; }
        public float? Score { get; set; }
    }
}
