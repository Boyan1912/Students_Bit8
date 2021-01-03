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
    public class StudentsController
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
    }
}
