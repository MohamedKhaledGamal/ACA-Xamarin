using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACA.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACA.Views.Survey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyAddNewAction : ContentPage
    {

       
        //Procedure _procedureModel = new Procedure();
        public bool IsOptionOne { get; set; } = true;
        public SurveyAddNewAction()
        {
            //InitializeComponent();
        }

        public void ProcedureType_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            //HandleViewBasedOnProcedure(ProcedureType.SelectedIndex);
        }

        void HandleViewBasedOnProcedure(int choice)
        {
            switch (choice)
            {
                //case 0: //the selection is محضر
                //    OptionOne.IsVisible = true;
                //    OptionTwo.IsVisible = false;
                //    break;
                //case 1: //the selection is اجراء حالي
                //    OptionOne.IsVisible = false;
                //    OptionTwo.IsVisible = true;
                //    IsOptionOne = false; // اجراء حالي
                //    break;
                //default:
                //    OptionOne.IsVisible = false;
                //    OptionTwo.IsVisible = false;
                //    break;

            }
        }

        //On add attachment button clicked 
        public async void OptionOneAttachment_OnClicked(object sender, EventArgs e)
        {

        }

        //Save Procedure button clicked
        public async void SaveBtn_OnClicked(object sender, EventArgs e)
        {
            //if (ProcedureType.SelectedIndex < 0) 
            //{
            //    await DisplayAlert("", "اختر نوع الإجراء", "موافق");
            //}
            //else
            //{
            //    _procedureModel.InspectorName = EmployeeName.Text;
            //    _procedureModel.ProcedureType = ProcedureType.Items[ProcedureType.SelectedIndex];

            //    if (IsOptionOne)
            //    {
            //        _procedureModel.ReportType = "محضر تفتيش";
            //        _procedureModel.ReportNumber = EntryReportNumber.Text;
            //        _procedureModel.Organization = "ايجابي";
            //        _procedureModel.ReportImg = null;
            //        _procedureModel.IsReport = true;

            //    }
            //    else
            //    {
            //        _procedureModel.CurrentProcedureType = "تحذير";
            //        _procedureModel.CurrentProcedureDescription = EntryDescription.Text;
            //        _procedureModel.IsReport = false;
            //    }

            //    MessagingCenter.Send(this, "AddItem", _procedureModel);
            //    await Navigation.PopToRootAsync();


            //}
            //List<Procedure> TestList = new List<Procedure>();
            //TestList.Add(_procedureModel);

            //  Navigation.PushModalAsync(new SurveyActionsList(TestList));
        }
    }
}