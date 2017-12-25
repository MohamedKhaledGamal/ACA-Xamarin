using ACA.Views.Temp;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ACA
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar");
            Page itemsPage, aboutPage = null;
            string UserID = "";
            //this.BarBackgroundColor = Color.FromHex("#d3271a");
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:

                    itemsPage = new NavigationPage(new ItemsPage(UserID))
                    {
                        Title = "Browse",

                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };

                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    //BarBackgroundColor = Color.Red;
                    break;

                default:
                    itemsPage = new ItemsPage(UserID)
                    {
                        Title = "Home"
                       
                    };

                    //BarBackgroundColor = Color.Red;
                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };
                    break;
            }

            //Children.Add(itemsPage);
            //Children.Add(aboutPage);

            //Title = Children[0].Title;
            //BarBackgroundColor = Color.White;
            //var refresh = new ToolbarItem
            //{
            //    Name = "refresh",
            //    Priority = 1,
            //    Order = ToolbarItemOrder.Secondary
            //};
            //var add = new ToolbarItem
            //{
            //    Name = "add",
            //    Priority = 2,
            //    Order = ToolbarItemOrder.Secondary
            //};
            

            //ToolbarItems.Add(refresh);
            //ToolbarItems.Add(add);
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
