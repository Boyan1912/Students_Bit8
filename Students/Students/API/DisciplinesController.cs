using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Students.Models;
using Students.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Students.API
{
    [ApiController]
    public class DisciplinesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDisciplinesService service;

        public DisciplinesController(IConfiguration config)
        {
            _config = config;
            service = new DisciplinesService(_config.GetValue<string>("ConnectionStrings:Default"));
        }

        [HttpGet("Disciplines/GetData")]
        public async Task<IActionResult> GetData()
        {
            var disciplines = await service.GetAll();
            var result = new GridResultModel<Discipline>()
            {
                 Data = disciplines
            };
            var json = new JsonResult(result);

            return json;
        }
    }
}
