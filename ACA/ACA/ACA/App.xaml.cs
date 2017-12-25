using ACA.Views.Login;
using ACA.Views.Temp;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ACA
{
    public partial class App : Application
    {
        public App()
        {
            //InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android
                || Device.RuntimePlatform == Device.UWP)
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar-EG");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ar-EG");

                Localize.LocalAR.Culture = CultureInfo.CurrentCulture;

                // determine the correct, supported .NET culture
                MainPage = new NavigationPage(new Login())
                {
                    BarBackgroundColor = Color.FromHex("#d3271a")
                };

                //MainPage = new NavigationPage(new ItemDetailPage("","",""))
                //{
                //    BarBackgroundColor = Color.FromHex("#d3271a")
                //};

                MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
            }
           

           

        }
    }
}