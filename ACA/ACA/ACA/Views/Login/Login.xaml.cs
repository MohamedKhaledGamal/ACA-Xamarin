using ACA.Common;
using ACA.Connector;
using ACA.Localize;
using ACA.Models;
using ACA.Views.Temp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACA.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        string UserID = "";

        public Login()
        {
            InitializeComponent();
            //base.setupPage();
            NavigationPage.SetHasNavigationBar(this, false);

            LocalizeArabic();
        }

        #region Events
        private void btnLogin_Clicked(object sender, EventArgs e)
        {

            //PPLoading.IsRunning = true;
            //this.Navigation.PushAsync(nextPage);
            if (txtUserName.Text != "" && txtPassword.Text != "")
            {

                LoginModel.RootObject logindata = LoginOffline();
                if (logindata.status == true)
                {
                    UserID = logindata.content.result.userId;

                    logindata.content.result.Password = txtPassword.Text;
                    logindata.content.result.userName = txtUserName.Text;

                    //PPLoading.IsRunning = false;
                    App.Current.MainPage = new NavigationPage(new ItemsPage(UserID))
                    {

                    };
                }
                else
                    DisplayAlert("تنبيه", "إسم المستخدم او كلمة المرور غير صحيحة", "نعم");
            }
            else
                DisplayAlert("تنبيه", "إسم المستخدم او كلمة المرور غير صحيحة", "نعم");

        }
        #endregion

        #region Methods
        public LoginModel.RootObject RefreshDataAsync(string Username, string Password)
        {
            LoginModel.RootObject Result = new LoginModel.RootObject();

            try
            {
                ServiceRequest s = new ServiceRequest();
                Result = s.LoginService(Username, Password, "");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", "Something went wrong with service", "OK");
            }

            return Result;
        }
        public void LocalizeArabic()
        {
            txtUserName.Placeholder = LocalAR.lblUsername;
            txtPassword.Placeholder = LocalAR.lblPassword;
        }

        public LoginModel.RootObject LoginOffline()
        {
            LoginModel.RootObject Result = new LoginModel.RootObject();

            try
            {
                if (DependencyService.Get<ISaveAndLoad>().FileExist("LoginInfo.txt"))
                {
                    string LatestSyncList = DependencyService.Get<ISaveAndLoad>().LoadText("LoginInfo.txt");
                    if (LatestSyncList != "")
                    {
                        Result = JsonConvert.DeserializeObject<LoginModel.RootObject>(LatestSyncList);
                    }
                    else
                    {
                        Result = RefreshDataAsync(txtUserName.Text,txtPassword.Text);
                    }
                }
                else
                {
                    Result = RefreshDataAsync(txtUserName.Text, txtPassword.Text);
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