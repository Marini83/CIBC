using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cibc.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           

            Cibc.UI.View.DisplayMainWindowGrid view = new Cibc.UI.View.DisplayMainWindowGrid();
            view.DataContext = new Cibc.UI.ViewModel.DisplayMainWindowViewModel();
            view.Show();

        }
    }
}
