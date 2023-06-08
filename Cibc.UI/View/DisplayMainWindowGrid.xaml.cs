using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cibc.UI.Model;
namespace Cibc.UI.View
{
    /// <summary>
    /// Interaction logic for DisplayGrid.xaml
    /// </summary>
    public partial class DisplayMainWindowGrid : Window
    {
        public DisplayMainWindowGrid()
        {
            this.DataContext = new Cibc.UI.ViewModel.DisplayMainWindowViewModel();
            InitializeComponent();
            this.dataGrid.BeginningEdit += (s, ss) => ss.Cancel = true;

            Thread thread = new Thread(new ThreadStart(StartComponent));
            thread.Start();
        }

        private void StartComponent()
        {
            var ColorRed = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 232, 38, 19));
            var ColorGreen = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 92, 236, 11));
        }
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridRow = sender as DataGridRow;
            if (dataGridRow != null)
            {
                var selectedItem = (Instrument)dataGridRow.Item;
                var viewModel = (Cibc.UI.ViewModel.DisplayMainWindowViewModel)this.DataContext;
                viewModel.Instruments[selectedItem.InstrumentId] = selectedItem;
                var popup = new PopupWindow();
                popup.InstrumentDataCollection.Add(new InstrumentData { Timestamp = DateTime.Now, Price = 123.45 });
                popup.Show();
            }
        }


    }
}
