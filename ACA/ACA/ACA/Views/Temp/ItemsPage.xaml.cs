using ACA.Common;
using ACA.Connector;
using ACA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using static ACA.Common.Enums;

namespace ACA.Views.Temp
{
    public partial class ItemsPage : ContentPage
    {
        TasksDetails.RootObject viewModel;
        string SessionUserID = "";

        ObservableCollection<TasksDetails.Task> TasksList = new ObservableCollection<TasksDetails.Task>();
        public ItemsPage(string UserID)
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar");
            SessionUserID = UserID;
            BindingContext = viewModel = new TasksDetails.RootObject();
            BindListView();
            this.Title = Localize.LocalAR.AppTitle;

            //DependencyService.Get<ISaveAndLoad>().SaveText("temp.txt", "hello My brother");

            //string x = DependencyService.Get<ISaveAndLoad>().LoadText("temp.txt");
        }

        #region Events
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as TasksDetails.Task;
            if (item == null)
                return;

            await Task.Delay(100);
            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            await Navigation.PushAsync(new ItemDetailPage(item.entityDetails.name, item.taskId.ToString(), item.surveyTemplateId.ToString()));
            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    //if (viewModel.Items.Count == 0)
        //    //    viewModel.LoadItemsCommand.Execute(null);
        //}
        private void BindListView()
        {
            TasksList.Clear();
            viewModel = GetAllItems();
            if (viewModel != null || viewModel.status != false)
            {
                for (int i = 0; i < viewModel.content.result.tasks.Count; i++)
                {

                    TasksList.Add(viewModel.content.result.tasks[i]);
                }

                ItemsListView.ItemsSource = TasksList;
            }
        }
        private void SyncForms_Clicked(object sender, EventArgs e)
        {
            try
            {
                GetListofTasks();
                GetListofQuestions();
                BindListView();
                DisplayAlert("تنبيه", "تمت مزامنة المهام بنجاح", "نعم");
            }
            catch (Exception ex)
            {
                DisplayAlert("تنبيه", "حدث خطأ اثناء المزامنة ارجو المحاولة مرة آخري", "نعم");
            }
        }
        private void SyncResults_Clicked(object sender, EventArgs e)
        {
            SyncResultModle.RootObject logindata = new SyncResultModle.RootObject();

            // Do some work on a background thread, allowing the UI to remain responsive
            SentResultData((Success,Result) =>
            {
                if (Success)
                {
                    //await DisplayAlert("تنبيه", "تمت مزامنة النتائج بنجاح", "نعم");
                    if (Result.status == true)
                    {
                        DependencyService.Get<ISaveAndLoad>().SaveText("AnswersList.txt", "");
                        GetListofLookups();
                    }
                    else
                    {
                        DisplayAlert("تنبيه", "يوجد خطا في الخدمة ارجو المحاولة مرة آخري", "نعم");
                    }
                }
                else
                    DisplayAlert("تنبيه", "يوجد خطا في الخدمة ارجو المحاولة مرة آخري", "نعم");
            });
            // When the background work is done, continue with this code block

            //if (logindata.content.result == "true")
            //{
                
            //}
            //else
                

        }
        private void SyncLookups_Clicked(object sender, EventArgs e)
        {
            try
            {
                GetListofLookups();
                DisplayAlert("تنبيه", "تمت مزامنة القوائم بنجاح", "نعم");
            }
            catch (Exception ex)
            {
                DisplayAlert("تنبيه", "حدث خطأ اثناء المزامنة ارجو المحاولة مرة آخري", "نعم");
            }
            //Lookups.RootObject LookupsObj = GetAllLookups();
            //if (LookupsObj.status == true)
            //{
            //    List<Lookups.Category> listCategory = LookupsObj.content.result.categories;
            //    for (int i = 0; i < listCategory.Count; i++)
            //    {
            //        if (listCategory[i].id == Convert.ToInt32(LookupType.SurveyStatus))
            //        {
            //            List<object> listofLookup = listCategory[i].lookups;
            //        }
            //    }
            //}
            //else
            //    DisplayAlert("Alert", "Something error in service", "OK");

        }
        private void ChangePassword_Clicked(object sender, EventArgs e)
        {

        }
        private void Logout_Clicked(object sender, EventArgs e)
        {

        }
        async void SearchButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new NewItemPage());
        }
        #endregion

        #region Methods 
        public Lookups.RootObject GetAllLookups()
        {
            Lookups.RootObject Result = new Lookups.RootObject();

            try
            {
                if (DependencyService.Get<ISaveAndLoad>().FileExist("Lookups.txt"))
                {
                    string LatestSyncList = DependencyService.Get<ISaveAndLoad>().LoadText("Lookups.txt");
                    Result = JsonConvert.DeserializeObject<Lookups.RootObject>(LatestSyncList);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            return Result;
        }
        //public TasksDetails.RootObject GetAllItems()
        //{
        //    TasksDetails.RootObject Result = new TasksDetails.RootObject();

        //    try
        //    {
        //        Result = ServiceRequest.GetItemDetails("1","3");
        //    }
        //    catch (Exception ex)
        //    {
        //         DisplayAlert("Alert", "Something went wrong with service", "OK");
        //    }

        //    return Result;
        //}
        public TasksDetails.RootObject GetAllItems()
        {
            TasksDetails.RootObject Result = new TasksDetails.RootObject();

            try
            {
                if (DependencyService.Get<ISaveAndLoad>().FileExist("TasksList.txt"))
                {
                    string LatestSyncList = DependencyService.Get<ISaveAndLoad>().LoadText("TasksList.txt");
                    Result = JsonConvert.DeserializeObject<TasksDetails.RootObject>(LatestSyncList);
                }
                else
                {
                    GetListofTasks();
                    GetListofQuestions();
                    Result = GetAllItems();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            return Result;
        }

        public SyncResultModle.RootObject SentResultData(Action<bool, SyncResultModle.RootObject> Callback )
        {
            SyncResultModle.RootObject Result = new SyncResultModle.RootObject();
            try
            {
                string ResultJson = DependencyService.Get<ISaveAndLoad>().LoadText("AnswersList.txt");
                ServiceRequest s = new ServiceRequest();
                s.SyncResultService(ResultJson,(Success,_Result) => {
                    Callback.Invoke(Success, _Result);
                });
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
                Callback.Invoke(false, null);
            }

            return Result;
        }

        public void GetListofLookups()
        {
            string Result = "";
            try
            {
                Result = Result = ServiceRequest.GetLookupsJsonString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            DependencyService.Get<ISaveAndLoad>().SaveText("Lookups.txt", "");
            DependencyService.Get<ISaveAndLoad>().SaveText("Lookups.txt", Result);
        }
        public void GetListofTasks()
        {
            string Result = "";

            try
            {
                Result = ServiceRequest.GetItemDetailsJsonString("1", "1");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            DependencyService.Get<ISaveAndLoad>().SaveText("TasksList.txt", "");
            DependencyService.Get<ISaveAndLoad>().SaveText("TasksList.txt", Result);
        }
        public void GetListofQuestions()
        {
            string Result = "";

            try
            {
                Result = ServiceRequest.GetQuestionsJsonString("1", "1");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            DependencyService.Get<ISaveAndLoad>().SaveText("QuestionsList.txt", "");
            DependencyService.Get<ISaveAndLoad>().SaveText("QuestionsList.txt", Result);
        }

        #endregion

    }

}