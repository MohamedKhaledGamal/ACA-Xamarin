﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACA.CustomControls
{
    public class ACAGroupHeader : Label
    {
        public ACAGroupHeader()
        {
            this.VerticalOptions = LayoutOptions.StartAndExpand;
            this.VerticalTextAlignment = TextAlignment.End;
            this.HorizontalTextAlignment = TextAlignment.End;
            this.TextColor = Color.Silver;
            this.FontSize = 20;
            //this.RotationY = 180;
            this.HorizontalOptions = LayoutOptions.EndAndExpand;
            this.Margin = Device.RuntimePlatform == Device.iOS ? new Thickness(20, 10, 20, 0) : new Thickness(25, 10, 20, 0);
        }
    }
}
