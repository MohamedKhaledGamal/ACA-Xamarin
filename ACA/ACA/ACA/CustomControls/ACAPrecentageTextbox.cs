using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.CustomControls
{
    public class ACAPrecentageTextbox : Entry
    {
        public ACAPrecentageTextbox()
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
        public void ACAPrecentageTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ACAPrecentageTextbox x = new ACAPrecentageTextbox();
            x.Text = ((Convert.ToInt32(e.OldTextValue) * 100) / 100).ToString();
        }
    }
}
