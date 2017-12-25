using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Models
{
    public class TasksDetails
    {
        //public class EntityType
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //    public bool isActive { get; set; }
        //}

        //public class MapLocation
        //{
        //    public string address { get; set; }
        //    public object longitude { get; set; }
        //    public object latitude { get; set; }
        //}

        //public class EntityDetail
        //{
        //    public string key { get; set; }
        //    public string value { get; set; }
        //}

        //public class Task
        //{
        //    public string entityTitle { get; set; }
        //    public EntityType entityType { get; set; }
        //    public MapLocation mapLocation { get; set; }
        //    public List<EntityDetail> entityDetails { get; set; }
        //    public int surveyId { get; set; }
        //}

        //public class Result
        //{
        //    public List<Task> tasks { get; set; }
        //}

        //public class Content
        //{
        //    public Result result { get; set; }
        //    public List<object> msgCodes { get; set; }
        //}

        //public class RootObject
        //{
        //    public bool status { get; set; }
        //    public Content content { get; set; }
        //}

        public class EntityDetails
        {
            public int id { get; set; }
            public int typeId { get; set; }
            public string typeName { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public int districtId { get; set; }
            public string districtName { get; set; }
            public int governorateID { get; set; }
            public string governorateName { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }
            public List<EntityInfo> entityInfo { get; set; }
        }

        public class EntityInfo
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        public class Task
        {
            public int taskId { get; set; }
            public EntityDetails entityDetails { get; set; }
            public int surveyTemplateId { get; set; }
        }

        public class Result
        {
            public List<Task> tasks { get; set; }
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
