using ACA.Common;
using ACA.Connector;
using ACA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACA.Views.Survey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyInformation : ContentPage
    {
        TasksDetails.RootObject viewModel;
        ObservableCollection<TasksDetails.EntityInfo> EntitiesList = new ObservableCollection<TasksDetails.EntityInfo>();


        public SurveyInformation(string TaskID)
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar");
            this.Title = Localize.LocalAR.AppTitle;
            BindControls(TaskID); 
        }

        #region Methods

        private void BindControls(string TaskID)
        {
            viewModel = GetAllItems();
            for (int i = 0; i < viewModel.content.result.tasks.Count; i++)
            {
                if (viewModel.content.result.tasks[i].taskId.ToString() == TaskID)
                {
                    lblItemHeader.Text = viewModel.content.result.tasks[i].entityDetails.name;
                    lblItemtext.Text = viewModel.content.result.tasks[i].entityDetails.typeName;
                    lblItemGovernrate.Text = viewModel.content.result.tasks[i].entityDetails.governorateName;
                    lblItemRegion.Text = viewModel.content.result.tasks[i].entityDetails.districtName;
                    lblItemAddress.Text = viewModel.content.result.tasks[i].entityDetails.address;

                    for (int j = 0; j < viewModel.content.result.tasks[i].entityDetails.entityInfo.Count; j++)
                    {
                        EntitiesList.Add(viewModel.content.result.tasks[i].entityDetails.entityInfo[j]);
                    }

                    //for (int j = 0; j < 1; j++)
                    //{
                    //    EntitiesList.Add(viewModel.content.result.tasks[i].entityDetails);
                    //}
                }
            }
            ItemsListViewDetails.ItemsSource = EntitiesList;
        }

        //public TasksDetails.RootObject GetAllItems()
        //{
        //    TasksDetails.RootObject Result = new TasksDetails.RootObject();

        //    try
        //    {
        //        Result = ServiceRequest.GetItemDetails("1", "3");
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayAlert("Alert", "Something went wrong with service", "OK");
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
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            return Result;
        }
        #endregion
    }
}