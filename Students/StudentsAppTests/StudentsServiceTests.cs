using NUnit.Framework;
using Students.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAppTests
{
    public class StudentsServiceTests
    {
        private IStudentsService service;
        
        [SetUp]
        public void Setup()
        {
            service = new StudentsService("server=localhost;user=root;password=password;database=mydb");
        }

        [Test]
        public async Task GetsAllSuccessfully()
        {
            var result = await service.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreatesStudentSuccessfully()
        {
            Assert.DoesNotThrowAsync(async() => await service.CreateStudent("Test Student", "Test Student", "11/11/1911"));
            var testStudents = await service.GetByName("Test Student", "Test Student");
            Assert.IsNotNull(testStudents);
            Assert.IsTrue(testStudents.Count > 0);
        }
        
        [Test]
        public async Task GetsAggregatedTop10StudentsWithScoresSuccessfully()
        {
            var result = await service.GetTopTen();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 10);
        }

        [Test]
        public async Task GetsAggregatedStudentsWithoutScoresSuccessfully()
        {
            var result = await service.GetStudentsWithEmptyScores();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TrueForAll(s => s.Semesters.TrueForAll(se => se.Disciplines.TrueForAll(d => !d.Score.HasValue))));
        }
    }
}
