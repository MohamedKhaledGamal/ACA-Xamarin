using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ACA.Models;
using ACA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACA.Views.Survey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyActionsList : ContentPage
    {
        public List<Object> Procedures { get; set; }
        //ObservableCollection<Procedure> BindList = new ObservableCollection<Procedure>();

        private SurveyAddNewActionViewModel surveyAddNewAction;
        public SurveyActionsList()
        {
            InitializeComponent();
            //BindingContext = surveyAddNewAction = new SurveyAddNewActionViewModel();
        }
        //public SurveyActionsList(List<Procedure> allProcedures)
        //{
        //    InitializeComponent();
        //    BindingContext = this;
        //    try
        //    {
        //        // Procedures.Add(allProcedures);
        //        for (int i = 0; i < allProcedures.Count; i++)
        //        {
        //            BindList.Add(allProcedures[i]);
        //        }
        //        ProceuresListView.ItemsSource = BindList;
        //        //  Label.Text = allProcedures.InspectorName;
        //    }
        //    catch (Exception e)
        //    {
        //        System.Diagnostics.Debug.WriteLine(e);
        //    }
        //}
        public async void AddBtn_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyAddNewAction());
        }
    }
}