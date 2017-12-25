using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.CustomControls
{
    public class ACADatePicker : DatePicker
    {
        DateTime dt = new DateTime();
        public ACADatePicker()
        {
            this.Margin = new Thickness(20, 0, 40, 0);
            this.BackgroundColor = Color.Transparent;
            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
            this.HeightRequest = 20;
            this.Format = "dd/MM/yyyy";
            this.MinimumDate = DateTime.Parse("01/01/1990");
            this.MaximumDate = DateTime.Parse("30/12/2050");
            this.VerticalOptions = LayoutOptions.CenterAndExpand;
        }
    }
}
