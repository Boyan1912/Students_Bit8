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
        private readonly IDisciplinesService _service;

        public DisciplinesController(IConfiguration config, IDisciplinesService service)
        {
            _config = config;
            _service = service;
        }

        [HttpGet("Disciplines/GetData")]
        public async Task<IActionResult> GetData()
        {
            var result = new ApiResultModel<Discipline>();

            try
            {
                var disciplines = await _service.GetAll();
                result.Data = disciplines;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpDelete("Disciplines/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ApiResultModel<object>();
            try
            {
                await _service.Delete(id);
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}
