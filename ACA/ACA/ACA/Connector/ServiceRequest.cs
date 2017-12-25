using ACA.Common;
using ACA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACA.Common.Enums;

namespace ACA.Connector
{
    public class ServiceRequest
    {
        /// <summary>
        /// Login Web service Calling
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public  LoginModel.RootObject LoginService(string UserName, string Password, string DeviceID)
        {
            Parser p = new Parser();
            var obj= p.ParsePost(string.Concat(SystemConfig.ServiceAPIURL, "User/", ServiceNames.Login),
                UserName,Password);

            return obj;

        }
        public void SyncResultService(string JsonObject, Action<bool,SyncResultModle.RootObject> Callback)
        {
            Parser p = new Parser();
            p.ParseSyncPost(string.Concat(SystemConfig.ServiceURL, ServiceNames.syncresults),
                JsonObject, (Success,Result) => {
                    Callback.Invoke(Success, Result);
                });
        }

        /// <summary>
        /// Get lookups
        /// </summary>
        /// <returns></returns>
        public static Lookups.RootObject GetLookups()
        {
            return Parser.ParseGetLookups<List<Lookups.RootObject>>(string.Concat(SystemConfig.ServiceURL, ServiceNames.Lookup, "/all"));

        }
        public static string GetLookupsJsonString()
        {
            return Parser.ParseGetLookupsJson(string.Concat(SystemConfig.ServiceAPIURL, ServiceNames.Lookup, "/all"));

        }
        /// <summary>
        /// Get Items and their details
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public static TasksDetails.RootObject GetItemDetails(string UserID , string DeviceID)
        {
            return Parser.ParseGetTasks<List<TasksDetails.RootObject>>(string.Concat(SystemConfig.ServiceURL, ServiceNames.sync),UserID,DeviceID);

        }
        public static string GetItemDetailsJsonString(string UserID, string DeviceID)
        {
            return Parser.ParseGetTasksJson(string.Concat(SystemConfig.ServiceURL, ServiceNames.sync), UserID, DeviceID);

        }
        /// <summary>
        /// Get Questions and Surveys
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public static Surveys.RootObject GetQuestions(string UserID, string DeviceID)
        {
            return Parser.ParseGetQuestions<List<Surveys.RootObject>>(string.Concat(SystemConfig.ServiceURL, ServiceNames.sync),UserID,DeviceID);

         }
        public static string GetQuestionsJsonString(string UserID, string DeviceID)
        {
            return Parser.ParseGetQuestionsJson(string.Concat(SystemConfig.ServiceURL, ServiceNames.sync), UserID, DeviceID);

        }

    }
}
