using System;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System.Globalization;
using ACA.Models;
using ACA.Connector;
using System.Collections.ObjectModel;
using ACA.Common;
using Newtonsoft.Json;

namespace ACA.Views.Temp
{
    public partial class ItemDetailPage : ContentPage
    {
        //ItemDetailViewModel viewModel;
        //ItemsViewModel viewItemModel;
        TasksDetails.RootObject viewModel;
        ObservableCollection<TasksDetails.EntityInfo> EntitiesList = new ObservableCollection<TasksDetails.EntityInfo>();
        string SessionSurveyID = "", SessionTaskID;
        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        string BindedItemText, BindedItemDescription;
        public ItemDetailPage(string ItemText, string TaskID, string SurveyID)
        {
            InitializeComponent();

            #region Commented Code
            //Content = new Map(
            //MapSpan.FromCenterAndRadius(
            //       new Position(37, -122), Distance.FromMiles(0.3)))
            //{
            //    IsShowingUser = true,
            //    HeightRequest = 100,
            //    WidthRequest = 960,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            //Content = new Map(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(10)));

            //var stack = new StackLayout { Spacing = 0 };
            //stack.Children.Add(map);
            //Content = stack;
            //map.MapType = MapType.Street;
            #endregion

            //MyMap=new Map(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(10)));

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar");
            SessionSurveyID = SurveyID;
            SessionTaskID = TaskID;
            this.Title = Localize.LocalAR.AppTitle;
            btnQuestionNavigate.Text = Localize.LocalAR.btnStartText;

            BindControls(SessionTaskID);

            //this.BindedItemText = ItemText;
            //this.BindedItemDescription = ItemDescription;

            //viewModel = new ItemDetailViewModel(item);

            //BindingContext = viewModel;

            //BindingContext = viewItemModel = new ItemsViewModel();


            //MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(0.3)));

        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            //BindingContext = this.viewModel = viewModel;
        }

        private async void btnQuestionNavigate_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new SurvayModule(SessionTaskID,SessionSurveyID));
        }

        private void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            LoadingLabel.IsVisible = true;
        }

        private void Browser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            LoadingLabel.IsVisible = false;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (viewItemModel.Items.Count == 0)
        //        viewItemModel.LoadItemsCommand.Execute(null);

        //    var item = new Item
        //    {
        //        Text = BindedItemText,
        //        Description = BindedItemDescription
        //    };

        //    lblItemtext.Text = item.Text;
        //    lblItemDesc.Text = item.Description;
        //}

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

                    string URL = "https://www.google.com.eg/maps/@" + viewModel.content.result.tasks[i].entityDetails.latitude
                        + "," + viewModel.content.result.tasks[i].entityDetails.longitude +
                        ",15z?hl=ar";

                    Browser.Source = URL;

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
