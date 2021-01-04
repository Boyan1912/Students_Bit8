using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public interface IStudentsService
    {
        Task<List<Student>> GetAll();
        Task<List<SummaryStudentModel>> GetTopTen();
    }
}
