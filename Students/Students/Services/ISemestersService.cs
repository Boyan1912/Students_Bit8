using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public interface ISemestersService
    {
        Task<List<Semester>> GetAll();
        Task Create(int studentId, string name);
    }
}
