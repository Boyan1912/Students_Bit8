﻿using Microsoft.AspNetCore.Mvc;
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
    public class SemestersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ISemestersService _service;

        public SemestersController(IConfiguration config, ISemestersService service)
        {
            _config = config;
            _service = service;
        }

        [HttpGet("Semesters/GetData")]
        public async Task<IActionResult> GetData()
        {
            var result = new ApiResultModel<Semester>();

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

        [HttpPost("Semesters/Create")]
        public async Task<IActionResult> Create([FromQuery] int studentId, [FromQuery] string name)
        {
            var result = new ApiResultModel<object>();
            try
            {
                await _service.Create(studentId, name);
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
