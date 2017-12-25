using ACA.Common;
using ACA.Connector;
using ACA.CustomControls;
using ACA.Models;
using ACA.Views.Temp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACA.Views.Survey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyQuestions : ContentPage
    {
        StackLayout Mainlayout;
        Grid QuestionViewGrid;
        Surveys.RootObject viewModel;
        ObservableCollection<Surveys.Group> GroupList = new ObservableCollection<Surveys.Group>();
        string SessionSurveyID = "", SessionTaskID = "";
        int IncrementalGrid = 0;
        //List<string> lstQuestionsIDs = new List<string>();
        //List<string> lstQuestionsIDsRelatedAnwer = new List<string>();
        List<AnswresModle.RootObject> lstOfAnswerModle = new List<AnswresModle.RootObject>();
        List<AnswresModle.Answer> lstofAnswer = new List<AnswresModle.Answer>();
        List<RelatedQues> lstRelatedQuest = new List<RelatedQues>();

        public class RelatedQues
        {
            public string QuestionID { get; set; }
            public string RelatedAnswer { get; set; }
            public string RelatedQuestionID { get; set; }
        }

        public SurveyQuestions(string SurveyID, string TaskID)
        {
            InitializeComponent();
            SessionSurveyID = SurveyID;
            SessionTaskID = TaskID;
            CreateUI(SessionSurveyID);

        }

        #region Methods
        public List<RelatedQues> GetListofRelatedQuestions(string TempSurveyID)
        {
            List<string> TempList = new List<string>();
            var Module = viewModel.content.result;
            for (int i = 0; i < Module.surveys.Count; i++)
            {
                if (Module.surveys[i].id.ToString() == TempSurveyID)
                {
                    for (int j = 0; j < Module.surveys[i].groups.Count; j++)
                    {
                        for (int k = 0; k < Module.surveys[i].groups[j].Sections.Count; k++)
                        {
                            for (int x = 0; x < Module.surveys[i].groups[j].Sections[k].SubSections.Count; x++)
                            {
                                for (int y = 0; y < Module.surveys[i].groups[j].Sections[k].SubSections[x].Questions.Count; y++)
                                {
                                    for (int z = 0; z < Module.surveys[i].groups[j].Sections[k].SubSections[x].Questions[y].Answers.Count; z++)
                                    {
                                        for (int h = 0; h < Module.surveys[i].groups[j].Sections[k].SubSections[x].Questions[y].Answers[z].RelatedQuestions.Count; h++)
                                        {
                                            string RelatedQuestionID = Module.surveys[i].groups[j].Sections[k].SubSections[x].Questions[y].Answers[z].RelatedQuestions[h];
                                            string RelatedAnswre = Module.surveys[i].groups[j].Sections[k].SubSections[x].Questions[y].Answers[z].AnswerText;
                                            string QuestionID = Module.surveys[i].groups[j].Sections[k].SubSections[x].Questions[y].Id;
                                            if (QuestionID != "")
                                            {
                                                RelatedQues reQues = new RelatedQues();
                                                reQues.QuestionID = QuestionID;
                                                reQues.RelatedQuestionID = RelatedQuestionID;
                                                reQues.RelatedAnswer = RelatedAnswre;

                                                lstRelatedQuest.Add(reQues);
                                                //TempList.Add(RelatedQuestionID);
                                                //lstQuestionsIDsRelatedAnwer.Add(RelatedAnswre);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //return TempList;
            return lstRelatedQuest;
        }
        public void CreateUI(string SurveyID)
        {

            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            layout.HorizontalOptions = LayoutOptions.FillAndExpand;

            ScrollView scrollView = new ScrollView();
            scrollView.HorizontalOptions = LayoutOptions.FillAndExpand;
            scrollView.VerticalOptions = LayoutOptions.FillAndExpand;
            scrollView.Orientation = ScrollOrientation.Vertical;

            QuestionViewGrid = new Grid { RowSpacing = 1, ColumnSpacing = 1 };
            QuestionViewGrid.HorizontalOptions = LayoutOptions.EndAndExpand;
            QuestionViewGrid.VerticalOptions = LayoutOptions.StartAndExpand;
            QuestionViewGrid.Margin = new Thickness(0, 0, 0, 0);
            QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

            viewModel = GetAllGroupsQuestions();

            for (int i = 0; i < viewModel.content.result.surveys.Count; i++)
            {
                if (SurveyID == viewModel.content.result.surveys[i].id.ToString())
                {
                    //lstQuestionsIDs = GetListofRelatedQuestions(SurveyID);
                    GetListofRelatedQuestions(SurveyID);
                    if (viewModel.content.result.surveys[i].Title != "")
                    {
                        QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                        #region Add Survey Header
                        var SurveyHeader = new ACASurveyHeader();
                        string X = viewModel.content.result.surveys[i].Title;
                        var header = new FormattedString();
                        header.Spans.Add(new Span { Text = X, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), FontAttributes = FontAttributes.Bold });
                        SurveyHeader.FormattedText = header;
                        #endregion

                        QuestionViewGrid.Children.Add(SurveyHeader, 0, IncrementalGrid);
                        IncrementalGrid++;
                    }
                    for (int j = 0; j < viewModel.content.result.surveys[i].groups.Count; j++)
                    {
                        if (viewModel.content.result.surveys[i].groups[j].Title != "")
                        {
                            QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                            #region Add GroupHeader
                            var GroupHeader = new ACAGroupHeader();
                            string grouptext = viewModel.content.result.surveys[i].groups[j].Title + " :";
                            var grheader = new FormattedString();
                            grheader.Spans.Add(new Span { Text = grouptext, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), FontAttributes = FontAttributes.Bold });
                            GroupHeader.FormattedText = grheader;
                            #endregion

                            QuestionViewGrid.Children.Add(GroupHeader, 0, IncrementalGrid);
                            IncrementalGrid++;
                        }
                        for (int z = 0; z < viewModel.content.result.surveys[i].groups[j].Sections.Count; z++)
                        {
                            if (viewModel.content.result.surveys[i].groups[j].Sections[z].Title != "")
                            {
                                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                                #region Add SectionHeader
                                var SectionHeader = new ACASectionLabel();
                                string Sectiontext = viewModel.content.result.surveys[i].groups[j].Sections[z].Title + " :";
                                var Secheader = new FormattedString();
                                Secheader.Spans.Add(new Span { Text = Sectiontext, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), FontAttributes = FontAttributes.Bold });
                                SectionHeader.FormattedText = Secheader;
                                #endregion

                                QuestionViewGrid.Children.Add(SectionHeader, 0, IncrementalGrid);
                                IncrementalGrid++;
                            }
                            for (int w = 0; w < viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections.Count; w++)
                            {
                                if (viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Title != "")
                                {
                                    QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                                    #region Add SubSectionHeader
                                    var SubSectionHeader = new ACASubSectionHeader();
                                    string SubSectiontext = viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Title + " :";
                                    var SubSecheader = new FormattedString();
                                    SubSecheader.Spans.Add(new Span { Text = SubSectiontext, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), FontAttributes = FontAttributes.Bold });
                                    SubSectionHeader.FormattedText = SubSecheader;
                                    #endregion

                                    QuestionViewGrid.Children.Add(SubSectionHeader, 0, IncrementalGrid);
                                    IncrementalGrid++;
                                }
                                for (int q = 0; q < viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Questions.Count; q++)
                                {
                                    QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                                    string QuestionType = viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Questions[q].SurveyQuestionTypes.ToString();
                                    string QuestionDescription = viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Questions[q].Description;
                                    string QuestionID = viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Questions[q].Id;
                                    //if (QuestionType == "1" || QuestionType == "2" || QuestionType == "3" || QuestionType == "8" || QuestionType == "0")
                                    //{
                                    #region Add QuestionText
                                    var Question = new ACALabel();
                                    string Questiontext = "  *  " + viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Questions[q].QuestionText;
                                    var Queheader = new FormattedString();
                                    Queheader.Spans.Add(new Span { Text = Questiontext, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), FontAttributes = FontAttributes.Bold });
                                    Question.FormattedText = Queheader;
                                    #endregion

                                    QuestionViewGrid.Children.Add(Question, 0, IncrementalGrid);
                                    IncrementalGrid++;
                                    List<string> answers = new List<string>();
                                    bool EnableFlag = true;
                                    answers.Add("إختر");
                                    for (int t = 0; t < viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Questions[q].Answers.Count; t++)
                                    {
                                        answers.Add(viewModel.content.result.surveys[i].groups[j].Sections[z].SubSections[w].Questions[q].Answers[t].AnswerText);
                                    }

                                    //for (int U = 0; U < lstQuestionsIDs.Count; U++)
                                    //{
                                    //    if (lstQuestionsIDs[U] == QuestionID)
                                    //    {
                                    //        //EnableFlag = false;
                                    //        EnableFlag = true;
                                    //    }
                                    //}
                                    DrawControlByQuestionID(QuestionType, QuestionID, QuestionDescription, answers, EnableFlag);

                                    //}
                                }
                            }
                        }
                    }
                }
            }

            CheckRelatedQuestion(lstRelatedQuest);
            scrollView.Content = QuestionViewGrid;
            //layout.Children.Add(scrollView);
            ContentView MainContentView = new ContentView();
            MainContentView.HorizontalOptions = LayoutOptions.FillAndExpand;
            MainContentView.Content = scrollView;
            this.Content = MainContentView;

            #region Commented Code
            //for (int i = 0; i < 3; i++)
            //{
            //    QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });
            //}
            //QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });
            //QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

            //QuestionViewGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            //#region Add Header
            //var SurveyHeader = new ACALabel { FontSize = 35 };
            //string X = "العنوان الرئيسي للتقرير :";
            //var header = new FormattedString();
            //header.Spans.Add(new Span { Text = X, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), FontAttributes = FontAttributes.Bold });
            //SurveyHeader.FormattedText = header;
            //#endregion

            //#region Add Question Text
            //var QuestionText = new ACALabel { FontSize = 10 };
            //QuestionText.TextColor = Color.Black;
            //string text = "السؤال الاول .. ؟";
            //var questiontxt = new FormattedString();
            //questiontxt.Spans.Add(new Span { Text = text, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), FontAttributes = FontAttributes.Bold });
            //QuestionText.FormattedText = questiontxt;
            //#endregion


            //QuestionViewGrid.Children.Add(QuestionText, 0, 1);


            //Grid.SetRow(SurveyHeader, 0);
            //Grid.SetRow(QuestionText, 1);
            //Grid.SetRow(textboxAnswer, 2);

            //var s = new FormattedString();
            //s.Spans.Add(new Span { Text = "Red Bold", FontAttributes = FontAttributes.Bold });
            //s.Spans.Add(new Span { Text = "Default" });
            //s.Spans.Add(new Span { Text = "italic small", FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), FontAttributes = FontAttributes.Italic });
            //label.FormattedText = s;
            #endregion
        }
        #endregion

        #region Creation Result
        public void DrawControlByQuestionID(string QuestionType, string QuestionID, string Description, List<string> lstOfAnswers, bool EnableFlag)
        {
            if (QuestionType == "1")
            {
                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                #region Add Textbox
                var textboxAnswer = new ACATextbox { FontSize = 20 };
                string txtboxhint = Description;
                textboxAnswer.Placeholder = txtboxhint;
                textboxAnswer.ClassId = QuestionID;
                textboxAnswer.IsEnabled = EnableFlag;
                //var textboxAnswer = new ACANumericTextBox { FontSize = 20 };
                //string txtboxhint = Description;
                //textboxAnswer.Placeholder = txtboxhint;
                #endregion

                QuestionViewGrid.Children.Add(textboxAnswer, 0, IncrementalGrid);
                IncrementalGrid++;
            }

            else if (QuestionType == "2")
            {
                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                #region Add TextArea
                var textAreaAnswer = new ACATextArea { FontSize = 20 };
                string txtboxhint = Description;
                textAreaAnswer.ClassId = QuestionID;
                #endregion

                QuestionViewGrid.Children.Add(textAreaAnswer, 0, IncrementalGrid);
                IncrementalGrid++;
            }

            else if (QuestionType == "3")
            {
                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                #region Add Numeric Textbox
                var textboxnumricAnswer = new ACANumericTextBox { FontSize = 20 };
                string txtboxhint = Description;
                textboxnumricAnswer.ClassId = QuestionID;
                textboxnumricAnswer.Placeholder = txtboxhint;
                #endregion

                QuestionViewGrid.Children.Add(textboxnumricAnswer, 0, IncrementalGrid);
                IncrementalGrid++;
            }

            else if (QuestionType == "4")
            {
                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                #region Add DecimalTextbox
                var textDecimalAnswer = new ACADecimalTextbox { FontSize = 20 };
                string txtboxhint = Description;
                textDecimalAnswer.ClassId = QuestionID;
                textDecimalAnswer.Placeholder = txtboxhint;
                #endregion

                QuestionViewGrid.Children.Add(textDecimalAnswer, 0, IncrementalGrid);
                IncrementalGrid++;
            }

            else if (QuestionType == "5")
            {
                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                #region Add PrecenategTextbox
                var textPrecentageAnswer = new ACAPrecentageTextbox { FontSize = 20 };
                string txtboxhint = Description;
                textPrecentageAnswer.ClassId = QuestionID;
                textPrecentageAnswer.Placeholder = txtboxhint;
                #endregion

                QuestionViewGrid.Children.Add(textPrecentageAnswer, 0, IncrementalGrid);
                IncrementalGrid++;
            }

            else if (QuestionType == "6")
            {
                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                #region Add DatePicker
                var DatePickerBind = new ACADatePicker {};
                string txtboxhint = Description;
                DatePickerBind.ClassId = QuestionID;
                #endregion

                QuestionViewGrid.Children.Add(DatePickerBind, 0, IncrementalGrid);
                IncrementalGrid++;
            }

            else if (QuestionType == "8")
            {
                QuestionViewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

                #region Add DropDownlist
                var DDLQuestion = new ACADropDownlist();
                DDLQuestion.ItemsSource = lstOfAnswers;
                DDLQuestion.SelectedIndex = 0;
                DDLQuestion.ClassId = QuestionID;
                DDLQuestion.Margin = new Thickness(150, 10, 50, 0);
                DDLQuestion.IsEnabled = EnableFlag;
                DDLQuestion.SelectedIndexChanged += this.pickQuestion_SelectedIndexChanged;
                #endregion

                QuestionViewGrid.Children.Add(DDLQuestion, 0, IncrementalGrid);
                IncrementalGrid++;
            }

        }

        public Surveys.RootObject GetAllGroupsQuestions()
        {
            Surveys.RootObject Result = new Surveys.RootObject();

            try
            {
                if (DependencyService.Get<ISaveAndLoad>().FileExist("QuestionsList.txt"))
                {
                    string LatestSyncList = DependencyService.Get<ISaveAndLoad>().LoadText("QuestionsList.txt");
                    Result = JsonConvert.DeserializeObject<Surveys.RootObject>(LatestSyncList);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            return Result;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var Display = await DisplayAlert("تنبيه", "هل تريد حفظ الاجابات", "نعم", "لا");

            if (Display == true)
            {
                SaveAnswers();
                //await DisplayAlert("تنبيه", "option selected is yes", "نعم");
            }
            else if (Display == false)
            {
                //await DisplayAlert("تنبيه", "option selected is No", "نعم");
            }
        }

        public async void SaveAnswers()
        {
            for (int i = 0; i < QuestionViewGrid.Children.Count; i++)
            {
                AnswresModle.Answer answer = new AnswresModle.Answer();
                if (QuestionViewGrid.Children[i] is ACATextbox)
                {
                    answer.answerText = ((ACATextbox)QuestionViewGrid.Children[i]).Text;
                    answer.questionId = ((ACATextbox)QuestionViewGrid.Children[i]).ClassId;
                    answer.answerId = "";
                    lstofAnswer.Add(answer);
                }
                else if (QuestionViewGrid.Children[i] is ACANumericTextBox)
                {
                    answer.answerText = ((ACANumericTextBox)QuestionViewGrid.Children[i]).Text;
                    answer.questionId = ((ACANumericTextBox)QuestionViewGrid.Children[i]).ClassId;
                    answer.answerId = "";
                    lstofAnswer.Add(answer);
                }
                else if (QuestionViewGrid.Children[i] is ACAPrecentageTextbox)
                {
                    answer.answerText = ((ACAPrecentageTextbox)QuestionViewGrid.Children[i]).Text;
                    answer.questionId = ((ACAPrecentageTextbox)QuestionViewGrid.Children[i]).ClassId;
                    answer.answerId = "";
                    lstofAnswer.Add(answer);
                }
                else if (QuestionViewGrid.Children[i] is ACADropDownlist)
                {
                    answer.answerText = ((ACADropDownlist)QuestionViewGrid.Children[i]).SelectedItem.ToString();
                    answer.questionId = ((ACADropDownlist)QuestionViewGrid.Children[i]).ClassId;
                    answer.answerId = ((ACADropDownlist)QuestionViewGrid.Children[i]).SelectedIndex.ToString();
                    lstofAnswer.Add(answer);
                }
                else if (QuestionViewGrid.Children[i] is ACATextArea)
                {

                    answer.answerText = ((ACATextArea)QuestionViewGrid.Children[i]).Text;
                    answer.questionId = ((ACATextArea)QuestionViewGrid.Children[i]).ClassId;
                    answer.answerId = "";
                    lstofAnswer.Add(answer);
                }

                else if (QuestionViewGrid.Children[i] is ACADecimalTextbox)
                {

                    answer.answerText = ((ACADecimalTextbox)QuestionViewGrid.Children[i]).Text;
                    answer.questionId = ((ACADecimalTextbox)QuestionViewGrid.Children[i]).ClassId;
                    answer.answerId = "";
                    lstofAnswer.Add(answer);
                }

                else if (QuestionViewGrid.Children[i] is ACADatePicker)
                {

                    answer.answerText = ((ACADatePicker)QuestionViewGrid.Children[i]).Date.ToString("dd/MM/yyyy");
                    answer.questionId = ((ACADatePicker)QuestionViewGrid.Children[i]).ClassId;
                    answer.answerId = "";
                    lstofAnswer.Add(answer);
                }


            }

            AnswresModle.Results results = new AnswresModle.Results();
            results.taskId = SessionTaskID;
            results.Result = lstofAnswer;

            List<AnswresModle.Results> listofResults = new List<AnswresModle.Results>();

            listofResults.Add(results);

            AnswresModle.RootObject root = new AnswresModle.RootObject();
            root.userId = "1";
            root.deviceId = "1";
            root.results = listofResults;
            string JsonStringResult = JsonConvert.SerializeObject(root);
            DependencyService.Get<ISaveAndLoad>().SaveText("AnswersList.txt", "");
            DependencyService.Get<ISaveAndLoad>().SaveText("AnswersList.txt", JsonStringResult);

            await Navigation.PushAsync(new ItemsPage("1"));
        }

        public void CheckRelatedQuestion(List<RelatedQues> list)
        {
            for (int i = 0; i < QuestionViewGrid.Children.Count; i++)
            {
                AnswresModle.Answer answer = new AnswresModle.Answer();
                if (QuestionViewGrid.Children[i] is ACATextbox)
                {
                    for (int U = 0; U < list.Count; U++)
                    {
                        if (list[U].RelatedQuestionID == ((ACATextbox)QuestionViewGrid.Children[i]).ClassId)
                        {
                            //EnableFlag = false;
                            QuestionViewGrid.Children[i].IsVisible = false;
                            QuestionViewGrid.Children[i - 1].IsVisible = false;
                        }
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACANumericTextBox)
                {
                    for (int U = 0; U < list.Count; U++)
                    {
                        if (list[U].RelatedQuestionID == ((ACANumericTextBox)QuestionViewGrid.Children[i]).ClassId)
                        {
                            //EnableFlag = false;
                            QuestionViewGrid.Children[i].IsVisible = false;
                            QuestionViewGrid.Children[i - 1].IsVisible = false;
                        }
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACAPrecentageTextbox)
                {
                    for (int U = 0; U < list.Count; U++)
                    {
                        if (list[U].RelatedQuestionID == ((ACAPrecentageTextbox)QuestionViewGrid.Children[i]).ClassId)
                        {
                            //EnableFlag = false;
                            QuestionViewGrid.Children[i].IsVisible = false;
                            QuestionViewGrid.Children[i - 1].IsVisible = false;
                        }
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACADropDownlist)
                {
                    for (int U = 0; U < list.Count; U++)
                    {
                        if (list[U].RelatedQuestionID == ((ACADropDownlist)QuestionViewGrid.Children[i]).ClassId)
                        {
                            //EnableFlag = false;
                            QuestionViewGrid.Children[i].IsVisible = false;
                            QuestionViewGrid.Children[i - 1].IsVisible = false;
                        }
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACATextArea)
                {
                    for (int U = 0; U < list.Count; U++)
                    {
                        if (list[U].RelatedQuestionID == ((ACATextArea)QuestionViewGrid.Children[i]).ClassId)
                        {
                            //EnableFlag = false;
                            QuestionViewGrid.Children[i].IsVisible = false;
                            QuestionViewGrid.Children[i - 1].IsVisible = false;
                        }
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACADecimalTextbox)
                {
                    for (int U = 0; U < list.Count; U++)
                    {
                        if (list[U].RelatedQuestionID == ((ACADecimalTextbox)QuestionViewGrid.Children[i]).ClassId)
                        {
                            //EnableFlag = false;
                            QuestionViewGrid.Children[i].IsVisible = false;
                            QuestionViewGrid.Children[i - 1].IsVisible = false;
                        }
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACADatePicker)
                {
                    for (int U = 0; U < list.Count; U++)
                    {
                        if (list[U].RelatedQuestionID == ((ACADatePicker)QuestionViewGrid.Children[i]).ClassId)
                        {
                            //EnableFlag = false;
                            QuestionViewGrid.Children[i].IsVisible = false;
                            QuestionViewGrid.Children[i - 1].IsVisible = false;
                        }
                    }
                }
            }
        }

        public void RetrieveRelatedQuestions(string RelatedQuestionID)
        {
            for (int i = 0; i < QuestionViewGrid.Children.Count; i++)
            {
                if (QuestionViewGrid.Children[i] is ACATextbox)
                {
                    if (RelatedQuestionID == ((ACATextbox)QuestionViewGrid.Children[i]).ClassId)
                    {
                        //EnableFlag = false;
                        QuestionViewGrid.Children[i].IsVisible = true;
                        QuestionViewGrid.Children[i - 1].IsVisible = true;
                    }

                }
                else if (QuestionViewGrid.Children[i] is ACANumericTextBox)
                {
                    if (RelatedQuestionID == ((ACANumericTextBox)QuestionViewGrid.Children[i]).ClassId)
                    {
                        //EnableFlag = false;
                        QuestionViewGrid.Children[i].IsVisible = true;
                        QuestionViewGrid.Children[i - 1].IsVisible = true;
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACAPrecentageTextbox)
                {
                    if (RelatedQuestionID == ((ACAPrecentageTextbox)QuestionViewGrid.Children[i]).ClassId)
                    {
                        //EnableFlag = false;
                        QuestionViewGrid.Children[i].IsVisible = true;
                        QuestionViewGrid.Children[i - 1].IsVisible = true;
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACADropDownlist)
                {
                    if (RelatedQuestionID == ((ACADropDownlist)QuestionViewGrid.Children[i]).ClassId)
                    {
                        //EnableFlag = false;
                        QuestionViewGrid.Children[i].IsVisible = true;
                        QuestionViewGrid.Children[i - 1].IsVisible = true;
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACATextArea)
                {
                    if (RelatedQuestionID == ((ACATextArea)QuestionViewGrid.Children[i]).ClassId)
                    {
                        //EnableFlag = false;
                        QuestionViewGrid.Children[i].IsVisible = true;
                        QuestionViewGrid.Children[i - 1].IsVisible = true;
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACADecimalTextbox)
                {
                    if (RelatedQuestionID == ((ACADecimalTextbox)QuestionViewGrid.Children[i]).ClassId)
                    {
                        //EnableFlag = false;
                        QuestionViewGrid.Children[i].IsVisible = true;
                        QuestionViewGrid.Children[i - 1].IsVisible = true;
                    }
                }
                else if (QuestionViewGrid.Children[i] is ACADatePicker)
                {
                    if (RelatedQuestionID == ((ACADatePicker)QuestionViewGrid.Children[i]).ClassId)
                    {
                        //EnableFlag = false;
                        QuestionViewGrid.Children[i].IsVisible = true;
                        QuestionViewGrid.Children[i - 1].IsVisible = true;
                    }
                }
            }
        }

        private void pickQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            //if (picker.SelectedIndex == 1)
            //{

            //}
            for (int i = 0; i < lstRelatedQuest.Count; i++)
            {
                if (lstRelatedQuest[i].QuestionID == picker.ClassId)
                {
                    if (lstRelatedQuest[i].RelatedAnswer == picker.SelectedItem.ToString())
                    {
                        CheckRelatedQuestion(lstRelatedQuest);
                        RetrieveRelatedQuestions(lstRelatedQuest[i].RelatedQuestionID);
                    }
                }
            }
        }

        #endregion

    }
}