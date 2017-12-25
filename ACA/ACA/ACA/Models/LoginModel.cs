using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Models
{
    public class LoginModel
    {
        public class Result
        {
            public string fullName { get; set; }
            public string userName { get; set; }
            public string Password { get; set; }
            public string userId { get; set; }
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
