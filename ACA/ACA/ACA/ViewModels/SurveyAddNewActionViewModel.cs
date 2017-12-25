using ACA.Views.Survey;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.ViewModels
{
    public  class SurveyAddNewActionViewModel : BaseViewModel
    {
        //public ObservableCollection<Procedure> Procedures { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SurveyAddNewActionViewModel()
        {
            //Procedures = new ObservableCollection<Procedure>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<SurveyAddNewAction, Procedure>(this, "AddItem", async (obj, item) =>
            //{
            //    var _procedure = item as Procedure;
            //    Procedures.Add(_procedure);

            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Procedures.Clear();
                //var proceduresList = await DataStore.GetItemsAsync(true);
                //foreach (var item in proceduresList)
                //{
                //    Procedures.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
