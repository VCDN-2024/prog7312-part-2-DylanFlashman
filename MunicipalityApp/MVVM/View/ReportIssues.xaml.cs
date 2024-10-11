using Microsoft.Win32;
using MunicipalityApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MunicipalityApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for ReportIssues.xaml
    /// </summary>
    public partial class ReportIssues : UserControl
    {
        private List<string> _suburbs;
        private List<string> _categories;
        private List<IssueForm> _forms;

        public ReportIssues()
        {
            InitializeComponent();

            _forms = new List<IssueForm>();

            _suburbs = new List<string>()
            {
                "Durban North","Westville","Newlands East","Newlands West","Greenwood Park","Parlock","Umhlanga"
            };

            _categories = new List<string>()
            {
                "Water Supply","Sewage Collection & Disposal","Refuse Removal","Electricity Supply","Municipal Health Services","Municipal Roads",
                "Storm Water Drainage","Street Lighting","Municipal Parks & Recreation","Public Safety"
            };

            cbSuburbs.ItemsSource = _suburbs;
            cbCategories.ItemsSource = _categories;
        }

        /*
         * Klaus78. 2012
         * Open file dialog and select a file using WPF controls and C#
         * StackOverflow
         * https://stackoverflow.com/questions/10315188/open-file-dialog-and-select-a-file-using-wpf-controls-and-c-sharp
         * [Accessed 1 september 2024]
         */
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            string fileName;
            string filePath = "";

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                filePath = openFileDialog.FileName;
                fileName = System.IO.Path.GetFileName(filePath);

                tbFileUpload.Text = fileName;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string address = tbLocation.Text;
            string suburb = (string)cbSuburbs.SelectedItem;
            string postalCode = tbPostalCode.Text;
            string selectedCategory = (string)cbCategories.SelectedItem;
            string description = "";
            string directory = System.IO.Path.GetFullPath(tbFileUpload.Text);
            string? attachment = directory;

            

            /* Lines 90 - 101
             * Miller,G. 2010
             * How to get a WPF rich textbox into a string
             * StackOverflow
             * https://stackoverflow.com/questions/4125310/how-to-get-a-wpf-rich-textbox-into-a-string
             * [Accessed 1 september 2024]
             */

            TextRange textRange = new TextRange(
                tbDescription.Document.ContentStart,
                tbDescription.Document.ContentEnd
                );

            using (MemoryStream ms = new MemoryStream())
            {
                textRange.Save(ms, DataFormats.Text);
                string sDescription = System.Text.Encoding.UTF8.GetString(ms.ToArray());

                description = sDescription;
            }

            if(!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(suburb) && !string.IsNullOrEmpty(postalCode) &&
                !string.IsNullOrEmpty(selectedCategory) && !string.IsNullOrEmpty(description))
            {
                try
                {
                    IssueForm form = new()
                    {
                        Address = address,
                        Suburb = suburb,
                        PostalCode = postalCode,
                        Category = selectedCategory,
                        Description = description,
                        Attachment = attachment
                    };

                    _forms.Add(form);

                    if(_forms.Count > 0)
                    {
                        MessageBox.Show("Your Query Was Submitted Successfully");
                    }

                    //MessageBox.Show($"{attachment}");

                    ClearFields();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please fill in all details before submitting.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearFields()
        {
            tbLocation.Text = string.Empty;
            cbSuburbs.SelectedIndex = -1;
            tbPostalCode.Text = string.Empty;
            cbCategories.SelectedIndex = -1;
            tbDescription.Document.Blocks.Clear();
            tbFileUpload.Text = "Select a file to upload";
        }
    }
}
