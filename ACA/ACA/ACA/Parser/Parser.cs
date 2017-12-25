using ACA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Net.Http;
using JavaScriptCore;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Java.IO;
using Android.Webkit;
using System.Threading;

namespace ACA
{
    public class Parser
    {

        public LoginModel.RootObject ParsePost(string RequestURL, string Username, string Password)
        {
            var client = new RestClient(RequestURL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"username\":\"m\",\"password\":\"mm\"}", ParameterType.RequestBody);
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<LoginModel.RootObject>(response.Result.Content);
        }

        public void ParseSyncPost(string RequestURL, string JsonObject, Action<bool, SyncResultModle.RootObject> Callback)
        {

            try
            {
                //var item = "{\"userId\":\"1\",\"deviceId\":\"1\",\"results\":[{\"taskId\":\"3\",\"Result\":[{\"questionId\":\"3913c36e-3c2e-5269-21a0-15da29e18b82\",\"answerId\":null,\"answerText\":\"egabi :D\"}]}]}";

                var client = new RestClient("http://10.3.1.222:9291/api/survey/syncresults");

                //var request = new RestRequest(Method.POST);
                //client.Timeout = TimeSpan.MaxValue;
                //request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("content-type", "application/json");
                //request.AddParameter("application/json", JsonObject, ParameterType.RequestBody);
                //var response = client.Execute(request);
                //return JsonConvert.DeserializeObject<SyncResultModle.RootObject>(response.Result.Content);


                HttpClient clientx = new HttpClient();
                HttpResponseMessage response = new HttpResponseMessage();
                clientx.MaxResponseContentBufferSize = 1000;
                clientx.Timeout = TimeSpan.FromSeconds(2);
                var uri = new Uri(RequestURL);
                //var item = "{\"userId\":\"1\",\"deviceId\":\"1\",\"results\":[{\"taskId\":\"3\",\"Result\":[{\"questionId\":\"3913c36e-3c2e-5269-21a0-15da29e18b82\",\"answerId\":null,\"answerText\":\"egabi :D\"}]}]}";
                //var json = item;
                HttpContent content = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                //response = await clientx.PostAsync(uri, content);
                clientx.PostAsync(uri, content).ContinueWith((result) =>
                {
                    string ErrorMsg;
                    if (result.IsFaulted)
                    {
                        ErrorMsg = result.Exception.StackTrace;
                        Callback.Invoke(false, null);
                    }
                    else if (result.IsCanceled)
                    {
                        ErrorMsg = "Task is cancelled!";
                        Callback.Invoke(false, null);
                    }
                    else if (result.IsCompleted)
                    {

                        result.Result.Content.ReadAsStringAsync().ContinueWith((ContentFetchTask) =>
                        {
                            if (ContentFetchTask.IsFaulted)
                                Callback.Invoke(false, null);
                            else if (ContentFetchTask.IsCanceled)
                                Callback.Invoke(false, null);
                            else if (ContentFetchTask.IsCompleted)
                                Callback.Invoke(true, JsonConvert.DeserializeObject<SyncResultModle.RootObject>(ContentFetchTask.Result));

                        });

                    }
                });


            }
            catch
            {
                Callback.Invoke(false, null);
            }
        }

        /**
         * public async Task<SyncResultModle.RootObject> ParseSyncPost(string RequestURL, string JsonObject)
        {
          
            try
            {
                //var item = "{\"userId\":\"1\",\"deviceId\":\"1\",\"results\":[{\"taskId\":\"3\",\"Result\":[{\"questionId\":\"3913c36e-3c2e-5269-21a0-15da29e18b82\",\"answerId\":null,\"answerText\":\"egabi :D\"}]}]}";

                var client = new RestClient("http://10.3.1.222:9291/api/survey/syncresults");

                //var request = new RestRequest(Method.POST);
                //client.Timeout = TimeSpan.MaxValue;
                //request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("content-type", "application/json");
                //request.AddParameter("application/json", JsonObject, ParameterType.RequestBody);
                //var response = client.Execute(request);
                //return JsonConvert.DeserializeObject<SyncResultModle.RootObject>(response.Result.Content);


                HttpClient clientx = new HttpClient();
                HttpResponseMessage response = new HttpResponseMessage();
                clientx.MaxResponseContentBufferSize = 1000;
                clientx.Timeout = TimeSpan.FromSeconds(2);
                var uri = new Uri(RequestURL);
                //var item = "{\"userId\":\"1\",\"deviceId\":\"1\",\"results\":[{\"taskId\":\"3\",\"Result\":[{\"questionId\":\"3913c36e-3c2e-5269-21a0-15da29e18b82\",\"answerId\":null,\"answerText\":\"egabi :D\"}]}]}";
                //var json = item;
                HttpContent content = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                //response = await clientx.PostAsync(uri, content);
                await clientx.PostAsync(uri, content).ContinueWith((result) => {
                    if(result.IsFaulted)
                        return JsonConvert.DeserializeObject<SyncResultModle.RootObject>(result.Result.Content.ToString());
                    if (result.IsCompleted)
                    JsonConvert.DeserializeObject<SyncResultModle.RootObject>(result.Result.Content.ToString()); 
                });
                return null;


            }
            catch
            {
                return null;
            }

        }
         * */

        public static Lookups.RootObject ParseGetLookups<T>(string RequestURL)
        {

            var client = new RestClient(RequestURL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "84ba3819-415e-67ba-07e4-90fce7eb43bd");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\"username\":\"m\",\"password\":\"mm\"}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<Lookups.RootObject>(response.Content);

        }

        public static string ParseGetLookupsJson(string RequestURL)
        {

            var client = new RestClient(RequestURL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\"username\":\"m\",\"password\":\"mm\"}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request).Result;

            return response.Content;

        }

        public static TasksDetails.RootObject ParseGetTasks<T>(string RequestURL, string UserID, string DeviceID)
        {
            string URL = RequestURL + "/" + UserID + "/" + DeviceID;
            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<TasksDetails.RootObject>(response.Content);

        }

        public static Surveys.RootObject ParseGetQuestions<T>(string RequestURL, string UserID, string DeviceID)
        {
            string URL = RequestURL + "/" + UserID + "/" + DeviceID;
            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<Surveys.RootObject>(response.Content);

        }

        public static async Task<LoginModel.RootObject> ParseLoginPost<T>(string RequestURL, string Username, string Password)
        {
            //var client = new RestClient(RequestURL);

            //client.Timeout = TimeSpan.MaxValue;
            //var request = new RestRequest(Method.POST);

            ////request.AddHeader("cache-control", "no-cache");
            ////request.AddHeader("content-type", "application/json");
            //request.AddHeader("Accept", "application/json");
            //request.AddParameter("application/json", "{ \"username\":\""+Username+"\", \"password\":\""+Password+"\" }", ParameterType.RequestBody);

            //var response = client.Execute<LoginModel.RootObject>(request);

            //return response.Result.Data;

            var client = new System.Net.Http.HttpClient();
            StringContent str = new StringContent("username=" + Username + "&password=" + Password + "", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(new Uri(RequestURL), str);
            var placesJson = response.Content.ReadAsStringAsync().Result;
            LoginModel.RootObject placeobject = new LoginModel.RootObject();
            if (placesJson != "")
            {
                placeobject = JsonConvert.DeserializeObject<LoginModel.RootObject>(placesJson);
            }
            return placeobject;


        }

        public static string ParseGetQuestionsJson(string RequestURL, string UserID, string DeviceID)
        {
            string URL = RequestURL + "/" + UserID + "/" + DeviceID;
            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request).Result;

            return response.Content;

        }

        public static string ParseGetTasksJson(string RequestURL, string UserID, string DeviceID)
        {
            string URL = RequestURL + "/" + UserID + "/" + DeviceID;
            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request).Result;

            return response.Content;

        }




        #region Commented Code
        //private async Task<JSValue> FetchWeatherAsync(string url)
        //{
        //    // Create an HTTP web request using the URL:
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
        //    request.ContentType = "application/json";
        //    request.Method = "GET";

        //    // Send the request to the server and wait for the response:
        //    using (WebResponse response = await request.GetResponseAsync())
        //    {
        //        // Get a stream representation of the HTTP web response:
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            // Use this stream to build a JSON document object:
        //            JSValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
        //            Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

        //            // Return the JSON document:
        //            return jsonDoc;
        //        }
        //    }
        //}
        #endregion

    }
}
