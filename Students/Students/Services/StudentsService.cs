using MySqlConnector;
using Students.Models;
using Students.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly string _connString;
        private readonly IAppRepository repo;

        public StudentsService(string connString)
        {
            _connString = connString;
            repo = new AppRepository(_connString);
        }

        public async Task<List<Student>> GetAll()
        {
            string sql = "SELECT s.id_student, s.first_name, s.last_name, s.date_of_birth, se.id_semester, se.name AS semester_name, se.start_date, se.end_date, d.id_discipline, d.name AS discipline_name, d.professor_name, d.score " +
                        "FROM student s " +
                        "LEFT JOIN students_semesters ss ON ss.id_student = s.id_student " +
                        "LEFT JOIN semester se ON se.id_semester = ss.id_semester " +
                        "LEFT JOIN discipline d ON d.id_semester = se.id_semester;";

            return await repo.GetResults<Student>(sql, (reader, result) => ParseStudentsFromSqlResult(reader, result));
        }

        public async Task<List<SummaryStudentModel>> GetTopTen()
        {
            string sql = "SELECT s.id_student, s.first_name, s.last_name,  s.date_of_birth, avg(d.score) AS avg_score " +
                        "FROM student s " +
                            "INNER JOIN students_semesters ss ON ss.id_student = s.id_student " +
                            "INNER JOIN semester se ON se.id_semester = ss.id_semester " +
                            "INNER JOIN discipline d ON d.id_semester = se.id_semester " +
                        "GROUP BY s.id_student " +
                        "ORDER BY avg_score DESC " +
                    "LIMIT 10;";

            return await repo.GetResults<SummaryStudentModel>(sql, (reader, results) => ParseSummaryStudentModel(reader, results));
        }

        public async Task<List<Student>> GetStudentsWithEmptyScores()
        {
            string sql = "SELECT s.id_student, s.first_name, s.last_name, s.date_of_birth, se.id_semester, se.name AS semester_name, se.start_date, se.end_date, d.id_discipline, d.name AS discipline_name, d.professor_name, d.score " +
                        "FROM student s " +
                        "LEFT JOIN students_semesters ss ON ss.id_student = s.id_student " +
                        "LEFT JOIN semester se ON se.id_semester = ss.id_semester " +
                        "INNER JOIN discipline d ON d.id_semester = se.id_semester AND isnull(d.score) " +
                        "ORDER BY s.first_name ASC, s.last_name ASC, se.name;";

            return await repo.GetResults<Student>(sql, (reader, results) => ParseStudentsFromSqlResult(reader, results));
        }

        public async Task CreateStudent(string firstName, string lastName, string dateBirth)
        {
            string sql = "INSERT INTO student(first_name, last_name, date_of_birth) " +
                    $"VALUES('{firstName}', '{lastName}', STR_TO_DATE('{dateBirth}', '%d/%m/%Y'));";

            await repo.Execute(sql);
        }

        public async Task<List<Student>> GetByName(string fistName, string lastName)
        {
            string sql = $"SELECT * FROM student WHERE first_name='{fistName}' AND last_name='{lastName}';";
            return await repo.GetResults<Student>(sql, (r, res) => ParseStudentsFromSqlResult(r, res));
        }

        private async Task ParseStudentsFromSqlResult(MySqlDataReader reader, List<Student> result)
        {
            while (await reader.ReadAsync())
            {
                int id = (int)reader.GetValue(0);
                var student = result.FirstOrDefault(s => s.IdStudent == id);
                if (student == null)
                {
                    student = new Student(id, reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                    DateTime dateBirth;
                    if (DateTime.TryParse(reader.GetValue(3).ToString(), null, System.Globalization.DateTimeStyles.None, out dateBirth))
                    {
                        student.DateOfBirth = dateBirth.ToString("MM/dd/yyyy");
                    }
                    student.Semesters = new List<Semester>();
                    result.Add(student);
                }
                int idSemester;
                if (int.TryParse(reader.GetValue(4).ToString(), System.Globalization.NumberStyles.Integer, null, out idSemester))
                {
                    var semester = student.Semesters.FirstOrDefault(se => se.IdSemester == idSemester);
                    if (semester == null)
                    {
                        semester = new Semester();
                        semester.IdSemester = idSemester;
                        semester.Name = reader.GetValue(5).ToString();
                        DateTime startDate;
                        if (DateTime.TryParse(reader.GetValue(6).ToString(), null, System.Globalization.DateTimeStyles.None, out startDate))
                        {
                            semester.StartDate = startDate.ToString("MM/dd/yyyy");
                        }
                        DateTime endDate;
                        if (DateTime.TryParse(reader.GetValue(7).ToString(), null, System.Globalization.DateTimeStyles.None, out endDate))
                        {
                            semester.EndDate = endDate.ToString("MM/dd/yyyy");
                        }
                        semester.Disciplines = new List<Discipline>();
                        student.Semesters.Add(semester);
                    }
                    int idDiscipline;
                    if (int.TryParse(reader.GetValue(8).ToString(), System.Globalization.NumberStyles.Integer, null, out idDiscipline))
                    {
                        var discipline = semester.Disciplines.FirstOrDefault(d => d.IdDiscipline == idDiscipline);
                        if (discipline == null)
                        {
                            discipline = new Discipline(idDiscipline, reader.GetValue(9).ToString(), reader.GetValue(10).ToString());
                            float score;
                            if (float.TryParse(reader.GetValue(11).ToString(), System.Globalization.NumberStyles.Float, null, out score))
                            {
                                discipline.Score = score;
                            }
                        }
                        semester.Disciplines.Add(discipline);
                    }
                }
            }
        }

        private async Task ParseSummaryStudentModel(MySqlDataReader reader, List<SummaryStudentModel> result)
        {
            while (await reader.ReadAsync())
            {
                var student = new SummaryStudentModel((int)reader.GetValue(0), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                DateTime dateBirth;
                if (DateTime.TryParse(reader.GetValue(3).ToString(), null, System.Globalization.DateTimeStyles.None, out dateBirth))
                {
                    student.DateOfBirth = dateBirth.ToString("MM/dd/yyyy");
                }
                student.AvgScore = (double)reader.GetValue(4);
                result.Add(student);
            }
        }
    }
}
