using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for FeedBackView.xaml
    /// </summary>
    public partial class FeedBackView : UserControl
    {
        public FeedBackView()
        {
            SetBrowserFeatureControl();

            InitializeComponent();

            CheckInternetAndLoadForm();
        }

        private void SetBrowserFeatureControl()
        {
            
            string appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            //string appName = System.IO.Path.GetFileName(Environment.ProcessPath);

            
            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"))
            {
                key?.SetValue(appName, 11001, RegistryValueKind.DWord); // IE 11 Mode
            }
        }

        private async void CheckInternetAndLoadForm()
        {
            if (await IsInternetAvailable())
            {
                string formUrl = "https://forms.office.com/r/jgSaiQTFue";
                //FormBrowser.Navigate(formUrl);
            }
            else
            {
                MessageBox.Show("No internet connection detected. Please check your connection and try again.", "Connectivity Issue", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async System.Threading.Tasks.Task<bool> IsInternetAvailable()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://www.google.com/");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
