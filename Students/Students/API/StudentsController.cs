using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Students.Models;
using Students.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.API
{
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IStudentsService _service;

        public StudentsController(IConfiguration config, IStudentsService service)
        {
            _config = config;
            _service = service;
        }

        [HttpGet("Students/GetData")]
        public async Task<IActionResult> GetData()
        {
            var result = new ApiResultModel<Student>();

            try
            {
                var semesters = await _service.GetAll();
                result.Data = semesters;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpGet("Students/GetTopTen")]
        public async Task<IActionResult> GetTopTen()
        {
            var result = new ApiResultModel<SummaryStudentModel>();

            try
            {
                var students = await _service.GetTopTen();
                result.Data = students;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpGet("Students/GetEmptyScores")]
        public async Task<IActionResult> GetEmptyScores()
        {
            var result = new ApiResultModel<Student>();

            try
            {
                var students = await _service.GetStudentsWithEmptyScores();
                result.Data = students;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpPost("Students/Create")]
        public async Task<IActionResult> Create(string firstName, string lastName, string dateBirth)
        {
            var result = new ApiResultModel<object>();

            try
            {
                await _service.CreateStudent(firstName, lastName, dateBirth);
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return Ok(result);
        }
    }
}
