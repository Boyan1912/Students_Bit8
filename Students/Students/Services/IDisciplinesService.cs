using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public interface IDisciplinesService
    {
        Task<List<Discipline>> GetAll();
        Task Delete(int id);
        Task UpdateProfessorName(int id, string professorName);
        Task Create(string name, string professorName, int? semesterId, float? score = null);
        Task<List<Discipline>> GetByName(string name);
    }
}
