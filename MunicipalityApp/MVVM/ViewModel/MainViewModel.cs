using MunicipalityApp.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MunicipalityApp.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
       

        public HomeViewModel HomeVM { get; set; }
        public ReportIssuesViewModel ReportIssuesVM { get; set; }
        public FeedBackViewModel FeedBackVM { get; set; }
        public EventAnnouncementsViewModel EventsVM { get; set; }
        public ServiceStatusRequest ServiceVM { get; set; }

        public RelayCommand HomeViewCMD { get; set; }
        public RelayCommand ReportViewCMD { get; set; }
        public RelayCommand CloseCMD { get; set; }
        public RelayCommand FeedbackCMD { get; set; }
        public RelayCommand EventAnnouncementsCMD { get; set; }
        public RelayCommand ServiceStatusCMD { get; set; }
        

        private object _currentView;

        public Object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {

            HomeVM = new HomeViewModel();
            ReportIssuesVM = new ReportIssuesViewModel();
            FeedBackVM = new FeedBackViewModel();
            EventsVM = new EventAnnouncementsViewModel();
            ServiceVM = new ServiceStatusRequest();
            CurrentView = HomeVM;

            HomeViewCMD = new RelayCommand(x =>
            {
                CurrentView = HomeVM;
            });

            ReportViewCMD = new RelayCommand(x =>
            {
                CurrentView = ReportIssuesVM;
            });

            CloseCMD = new RelayCommand(x =>
            {
                CloseApp();
            });

            FeedbackCMD = new RelayCommand(x =>
            {
                CurrentView = FeedBackVM;
            });

            EventAnnouncementsCMD = new RelayCommand(x =>
            {
                CurrentView = EventsVM;
            });

            ServiceStatusCMD = new RelayCommand(x =>
            {
                CurrentView = ServiceVM;
            });
            
        }

        private void CloseApp()
        {
            App.Current.Shutdown();
        }        
    }
}
