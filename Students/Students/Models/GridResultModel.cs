using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class GridResultModel<T> where T : class
    {
        public IList<T> Data { get; set; }
    }
}
