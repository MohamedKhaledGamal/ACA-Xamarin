using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.CustomControls
{
    public class ACASurveyHeader : Label
    {
        public ACASurveyHeader()
        {
            this.VerticalOptions = LayoutOptions.StartAndExpand;
            this.VerticalTextAlignment = TextAlignment.Center;
            this.HorizontalTextAlignment = TextAlignment.Center;
            this.TextColor = Color.FromHex("#d3271a");
            this.FontSize = 35;
            //this.RotationY = 180;
            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
            this.Margin = Device.RuntimePlatform == Device.iOS ? new Thickness(20, 10, 20, 0) : new Thickness(25, 10, 20, 0);
        }
    }
}
