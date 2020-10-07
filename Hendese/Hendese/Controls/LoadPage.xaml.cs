using Hendese.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hendese.Controls
{
    /// <summary>
    /// Interaction logic for LoadPage.xaml
    /// </summary>
    public partial class LoadPage : Page
    {
        public LoadPage()
        {
            InitializeComponent();
        }

        private void Model_Click(object sender, RoutedEventArgs e)
        {
            Button obj = (Button)sender;

            BaseModel Model = null;

            if (obj.Name == "SimpleSingleLoad")
                Model = new SimpleSingleLoad();
            else if (obj.Name == "SimpleDistributedLoad")
                Model = new SimpleDistributedLoad();
            else if (obj.Name == "ConsolSingleLoad")
                Model = new ConsolSingleLoad();
            else if (obj.Name == "ConsolDistributedLoad")
                Model = new ConsolDistributedLoad();
            else if (obj.Name == "FixedSingleLoad")
                Model = new FixedSingleLoad();
            else if (obj.Name == "FixedDistributedLoad")
                Model = new FixedDistributedLoad();
            else if (obj.Name == "SimpleFixedDistributedLoad")
                Model = new SimpleFixedDistributedLoad();

            if (Model != null)
                this.NavigationService.Navigate(new LoadDetailsPage(Model));
            else
                if (MainWindow.extendedNotifyIcon.targetNotifyIcon != null)
                MainWindow.extendedNotifyIcon.targetNotifyIcon.ShowBalloonTip(1000, "Error",
                    "Oops! Something went wrong! Model may not be included yet...",
                     System.Windows.Forms.ToolTipIcon.Error);
            else
                MessageBox.Show("Oops!Something went wrong!Model may not be included yet...");
        }
    }
}
