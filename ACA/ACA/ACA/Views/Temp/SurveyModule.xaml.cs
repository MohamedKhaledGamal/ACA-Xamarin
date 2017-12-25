using ACA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACA.Views.Temp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurvayModule : TabbedPage
    {
        string SessionTaskID = "", SessionSurveyID="";

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        public SurvayModule(string TaskID , string SurveyID)
        {

            //InitializeComponent();
            Page SurveyQuestions, Actions, Informations = null;

            this.Title = Localize.LocalAR.AppTitle;
            SessionTaskID = TaskID;
            SessionSurveyID = SurveyID;
            BarBackgroundColor = Color.FromHex("#d3271a");

            SurveyQuestions = new Survey.SurveyQuestions(SessionSurveyID, SessionTaskID)
            {
                Title = Localize.LocalAR.QuestionsScreen
                //Icon = "questionsicon.png"
            };
            Actions = new Survey.SurveyActionsList()
            {
                Title = Localize.LocalAR.ActionsScreen
                //Icon = "actionsicon.png"
            };
            Informations = new Survey.SurveyInformation(SessionTaskID)
            {
                Title = Localize.LocalAR.InforScreen
                //Icon = "infoicon.png"
            };

            Children.Add(Actions);
            Children.Add(SurveyQuestions);
            Children.Add(Informations);

            this.CurrentPage = SurveyQuestions;
            this.BarTextColor = Color.FromHex("#d3271a");
        }
    }
}