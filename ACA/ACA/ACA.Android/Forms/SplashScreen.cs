using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Globalization;
using System.Threading;

namespace ACA.Droid.Forms
{
    [Activity(Theme= "@style/SplashTheme",
        MainLauncher = true,
        NoHistory=true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            System.Threading.Thread.Sleep(3000);
            StartActivity(typeof(MainActivity));
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar-AR");
            Thread.CurrentThread.CurrentCulture= new CultureInfo("ar-EG");
        }
    }
}