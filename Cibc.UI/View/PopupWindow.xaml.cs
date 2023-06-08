using Cibc.UI.Model;
using System.Collections.ObjectModel;
using System.Windows;
namespace Cibc.UI.View
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        public ObservableCollection<InstrumentData> InstrumentDataCollection { get; set; } = new ObservableCollection<InstrumentData>();

        public PopupWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
