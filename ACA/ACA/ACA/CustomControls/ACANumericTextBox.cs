using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.CustomControls
{
    public class ACANumericTextBox :Entry
    {
        public ACANumericTextBox()
        {
            this.Margin = new Thickness(20, 0, 40, 0);
            this.PlaceholderColor = Color.Silver;
            this.BackgroundColor = Color.Transparent;
            this.HorizontalTextAlignment = TextAlignment.End;
            this.FontSize = 14;
            this.Keyboard = Keyboard.Numeric;
            this.HeightRequest = 20;
            //this.VerticalOptions = LayoutOptions.EndAndExpand;
            this.HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        
    }
}
