using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Models
{
    public class Surveys
    {
        public class SurveyType
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool isActive { get; set; }
        }

        public class ValidationTestEntity
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool isActive { get; set; }
        }

        public class ValidationTestProcedure
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool isActive { get; set; }
        }

        public class Validations
        {
            public bool SurveyQuestionIsRequired { get; set; }
            public bool ApplyMinValueValidation { get; set; }
            public string MinValue { get; set; }
            public bool ApplyMaxValueValidation { get; set; }
            public string MaxValue { get; set; }
        }

        public class Question
        {
            public string Id { get; set; }
            public string QuestionText { get; set; }
            public string Description { get; set; }
            public int SurveyQuestionTypes { get; set; }
            public List<Answers> Answers { get; set; }
            public Validations SurveyQuestionValidation { get; set; }
        }

        public class Answers
        {
            public string AnswerText { get; set; }
            public bool CanAddNotes { get; set; }
            public bool RequiresAction { get; set; }
            public List<string> RelatedQuestions { get; set; }
        }

        public class SubSection
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public List<Question> Questions { get; set; }
        }

        public class Section
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public List<SubSection> SubSections { get; set; }
        }

        public class Group
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public List<Section> Sections { get; set; }
        }

        public class Survey
        {
            public int id { get; set; }
            public string Title { get; set; }
            public SurveyType surveyType { get; set; }
            public List<ValidationTestEntity> validationTestEntities { get; set; }
            public List<ValidationTestProcedure> validationTestProcedures { get; set; }
            public List<Group> groups { get; set; }
        }

        public class Result
        {
            public List<Survey> surveys { get; set; }
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
