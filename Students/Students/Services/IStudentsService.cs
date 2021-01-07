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
        Task<List<Student>> GetStudentsWithEmptyScores();
        Task CreateStudent(string firstName, string lastName, string dateBirth);
        Task<List<Student>> GetByName(string fistName, string lastName);
    }
}
