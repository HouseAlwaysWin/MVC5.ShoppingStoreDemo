using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStoreDomain
{
    public class PageModel<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

    public class Test
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
    }
}
