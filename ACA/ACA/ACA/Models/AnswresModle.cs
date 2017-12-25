using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Models
{
    public class AnswresModle
    {
        public class Answer
        {
            public string questionId { get; set; }
            public string answerId { get; set; }
            public string answerText { get; set; }
        }

        public class Results
        {
            public string taskId { get; set; }
            public List<Answer> Result { get; set; }
        }

        public class RootObject
        {
            public string userId { get; set; }
            public string deviceId { get; set; }
            public List<Results> results { get; set; }
        }
    }
}
