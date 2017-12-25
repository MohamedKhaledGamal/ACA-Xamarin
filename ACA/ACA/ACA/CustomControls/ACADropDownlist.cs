using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.CustomControls
{
    public class ACADropDownlist : Picker
    {
        string Titlestr = "";
        List<object> ItemSourceBind;
        string selectedText = "";
        public ACADropDownlist()
        {
            this.Title = Titlestr;
            this.VerticalOptions = LayoutOptions.CenterAndExpand;
            //this.Rotation = 180;
            //this.Margin = Device.RuntimePlatform == Device.iOS ? new Thickness(150, 10, 50, 0) : new Thickness(150, 10, 50, 0);
            this.ItemsSource = ItemSourceBind;

        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedText = (string)picker.ItemsSource[selectedIndex];
            }
        }
    }
}
