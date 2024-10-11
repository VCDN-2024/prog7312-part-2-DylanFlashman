using MunicipalityApp.MVVM.Model;
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
using System.Windows.Shapes;

namespace MunicipalityApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        public Event NewEvent { get; private set; }

        public AddEventWindow()
        {
            InitializeComponent();
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbEventName.Text) || cbEventCategory.SelectedItem == null || !dpEventDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Create the new event
            NewEvent = new Event
            {
                Name = tbEventName.Text,
                Category = cbEventCategory.Text,
                Date = dpEventDate.SelectedDate.Value
            };

            this.DialogResult = true; // Close the window with a positive result
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Close without adding the event
            this.Close();
        }
    }
}
