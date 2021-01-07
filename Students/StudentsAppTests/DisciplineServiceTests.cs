using NUnit.Framework;
using Students.Models;
using Students.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAppTests
{
    public class DisciplinesServiceTests
    {
        private IDisciplinesService service;
        
        [SetUp]
        public async Task Setup()
        {
            service = new DisciplinesService("server=localhost;user=root;password=password;database=mydb");
        }

        [Test]
        public async Task GetsAllSuccessfully()
        {
            var result = await service.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreatesDisciplineWithoutSemesterAndScoreSuccessfully()
        {
            Assert.DoesNotThrowAsync(async() => await service.Create("Test Name", "Test Professor Name", null));
            var testDisciplines = await service.GetByName("Test Name");
            Assert.IsNotNull(testDisciplines);
            Assert.IsTrue(testDisciplines.Count > 0);
        }
        [Test]
        public async Task CreatesDisciplineWithoutSemesterAndWithScoreSuccessfully()
        {
            Assert.DoesNotThrowAsync(async() => await service.Create("Test Name", "Test Professor Name", null, 3.6f));
            var testDisciplines = await service.GetByName("Test Name");
            Assert.IsNotNull(testDisciplines);
            Assert.IsTrue(testDisciplines.Count > 0);
            Assert.IsTrue(testDisciplines.Count(d => d.Score == 3.6f) > 0);
        }

        [Test]
        public async Task DoesNotDeleteDisciplineWithScore()
        {
            var testDisciplines = await service.GetByName("Test Name");
            int disciplineWithoutScoreId = testDisciplines.FirstOrDefault(d => d.Score.HasValue).IdDiscipline;
            Assert.ThrowsAsync<ArgumentException>(async () => await service.Delete(disciplineWithoutScoreId));
        }

        [Test]
        public async Task DeletesDisciplineWithoutScore()
        {
            var testDisciplines = await service.GetByName("Test Name");
            int disciplineWithoutScoreId = testDisciplines.FirstOrDefault(d => !d.Score.HasValue).IdDiscipline;
            Assert.DoesNotThrowAsync(async () => await service.Delete(disciplineWithoutScoreId));
            var all = await service.GetAll();
            Assert.IsNull(all.FirstOrDefault(d => d.IdDiscipline == disciplineWithoutScoreId));
        }
    }
}