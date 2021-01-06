using Microsoft.VisualStudio.TestTools.UnitTesting;
using Students.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Students_Tests
{
    [TestClass]
    public class DisciplinesServiceTests
    {
        private readonly IDisciplinesService service = new DisciplinesService("server=localhost;user=root;password=password;database=mydb");
        
        [TestMethod]
        public async Task ReturnsResultsSuccessfully()
        {
            var results = await service.GetAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);

            var disciplineWithScores = results.FirstOrDefault(d => d.Score.HasValue);
            Assert.IsNotNull(disciplineWithScores);
        }

        /*[TestMethod]
        public void DoesNotDeleteDisciplineWithScores()
        {
            Assert.ThrowsException<ArgumentException>(service.Delete(disciplineIdWithScores).Wait);
        }*/

    }
}
