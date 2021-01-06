using NUnit.Framework;
using Students.Services;
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
        public void Test1()
        {
            Assert.Pass();
        }
    }
}