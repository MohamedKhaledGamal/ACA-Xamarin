using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Models
{
    public class Lookups
    {
        public class Category
        {
            public int id { get; set; }
            public string title { get; set; }
            public List<object> lookups { get; set; }
        }

        public class Result
        {
            public List<Category> categories { get; set; }
        }

        public class Content
        {
            public Result result { get; set; }
            public List<object> msgCodes { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public Content content { get; set; }
        }
    }
}
