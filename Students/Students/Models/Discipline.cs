using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class Discipline
    {
        //[JsonProperty("idDiscipline")]
        public int IdDiscipline { get; set; }
        
        //[JsonProperty("name")]
        public string Name { get; set; }
        
        //[JsonProperty("professorName")]
        public string ProfessorName { get; set; }
        
        //[JsonProperty("score")]
        public float Score { get; set; }
    }
}
