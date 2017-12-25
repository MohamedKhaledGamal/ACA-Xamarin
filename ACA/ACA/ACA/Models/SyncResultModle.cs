using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Models
{
    public class SyncResultModle
    {
        public class Content
        {
            public string result { get; set; }
            public List<object> msgCodes { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public Content content { get; set; }
        }
    }
}
