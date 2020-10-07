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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        LoadPage loadPage = new LoadPage();
        SectionPage sectionPage = new SectionPage();

        private void loads_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(loadPage);
        }

        private void sections_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(sectionPage);
        }
    }
}
