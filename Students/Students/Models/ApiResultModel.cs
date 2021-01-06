using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class ApiResultModel<T> where T : class
    {
        public IList<T> Data { get; set; }
        public string Message{ get;set; }
        public string ErrorMessage { get; set; }
    }
}
