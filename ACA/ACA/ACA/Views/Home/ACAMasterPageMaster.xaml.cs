using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACA.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ACAMasterPageMaster : ContentPage
    {
        public ListView ListView;

        public ACAMasterPageMaster()
        {
            InitializeComponent();

            BindingContext = new ACAMasterPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class ACAMasterPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ACAMasterPageMenuItem> MenuItems { get; set; }

            public ACAMasterPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<ACAMasterPageMenuItem>(new[]
                {
                    new ACAMasterPageMenuItem { Id = 0, Title = "Page 1" },
                    new ACAMasterPageMenuItem { Id = 1, Title = "Page 2" },
                    new ACAMasterPageMenuItem { Id = 2, Title = "Page 3" },
                    new ACAMasterPageMenuItem { Id = 3, Title = "Page 4" },
                    new ACAMasterPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}