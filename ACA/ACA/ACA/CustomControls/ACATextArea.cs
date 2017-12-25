using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.CustomControls
{
    public class ACATextArea : Editor
    {
        public string mytext = "";

        public ACATextArea()
        {
            this.FontSize = 14;
            this.Margin = new Thickness(26, 0, 26, 0);
            this.HeightRequest = 100;
            this.Text = mytext;
            this.TextColor = Color.Silver;
            this.BackgroundColor = Color.Transparent;
        }
    }
}
