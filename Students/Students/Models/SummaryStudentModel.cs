using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class SummaryStudentModel : BaseStudentModel
    {
        public SummaryStudentModel(int id, string firstName, string lastName) : base(id, firstName, lastName)
        {

        }

        public double AvgScore { get; set; }
    }
}
