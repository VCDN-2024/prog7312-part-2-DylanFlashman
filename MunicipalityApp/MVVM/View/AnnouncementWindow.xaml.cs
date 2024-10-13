using MunicipalityApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace MunicipalityApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for AnnouncementWindow.xaml
    /// </summary>
    public partial class AnnouncementWindow : Window
    {
        public AnnouncementWindow(ObservableCollection<Announcement> announcements)
        {
            InitializeComponent();
            lvAnnouncements.ItemsSource = announcements;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
