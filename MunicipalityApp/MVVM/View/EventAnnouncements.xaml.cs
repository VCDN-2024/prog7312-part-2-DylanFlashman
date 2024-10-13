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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MunicipalityApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for EventAnnouncements.xaml
    /// </summary>
    public partial class EventAnnouncements : UserControl
    {
        private Dictionary<string, List<Event>> eventDictionary;
        private Dictionary<string, List<Announcement>> announcementDictionary;
        private PriorityQueue<Event, DateTime> eventPriorityQueue;
        private ObservableCollection<Event> displayedEvents;
        private ObservableCollection<Announcement> displayedAnnouncements;
        private HashSet<string> uniqueCategories;
        private HashSet<DateTime> uniqueDates;
        private UserSearchHistory userSearchHistory = new UserSearchHistory();

        public EventAnnouncements()
        {
            InitializeComponent();
            //PopulateCategoryCombobox();
            //SetSearchDate();
            InitializeEventDictionary();
            DisplayAllEvents();
            InitializePriorityQueue();
            InitializeAnnouncementDictionary();
        }

        private void InitializePriorityQueue()
        {
            eventPriorityQueue = new PriorityQueue<Event, DateTime>();

            foreach (var categoryEvents in eventDictionary.Values)
            {
                foreach (var ev in categoryEvents)
                {
                    eventPriorityQueue.Enqueue(ev, ev.Date);
                }
            }

        }

        private void InitializeEventDictionary()
        {

            eventDictionary = new Dictionary<string, List<Event>>();
            uniqueCategories = new HashSet<string>(); 
            uniqueDates = new HashSet<DateTime>();

            var sampleEvents = new List<Event>
            {
                new Event { Name = "Rugby Tournament", Category = "Sports", Date = DateTime.Now.AddDays(26) },
                new Event { Name = "Mueseum Opening", Category = "Art", Date = DateTime.Now.AddDays(22) },
                new Event { Name = "Extra Lessons", Category = "Education", Date = DateTime.Now.AddDays(35) },
                new Event { Name = "Football Match", Category = "Sports", Date = DateTime.Now.AddDays(7) },
                new Event { Name = "Chris Brown Concert", Category = "Music", Date = DateTime.Now.AddDays(25) },
                new Event { Name = "Sip & Paint", Category = "Art", Date = DateTime.Now.AddDays(12) },
                new Event { Name = "Hackathon", Category = "Technology", Date = DateTime.Now.AddDays(15) },
                new Event { Name = "Music Festival", Category = "Music", Date = DateTime.Now.AddDays(5) },
                new Event { Name = "Tech Conference", Category = "Education", Date = DateTime.Now.AddDays(10) },
                new Event { Name = "Sports Day", Category = "Sports", Date = DateTime.Now.AddDays(3) }
            };

            foreach (var ev in sampleEvents)
            {
                uniqueCategories.Add(ev.Category);
                uniqueDates.Add(ev.Date);

                string key = $"{ev.Category}_{ev.Date.ToShortDateString()}"; 

                if (!eventDictionary.ContainsKey(key))
                {
                    eventDictionary[key] = new List<Event>(); 
                }

                eventDictionary[key].Add(ev); 
            }

            PopulateCategoryCombobox();

            displayedEvents = new ObservableCollection<Event>(sampleEvents);
            lvDisplay.ItemsSource = displayedEvents;
        }

        private void DisplayAllEvents()
        {
            //var allEvents = eventDictionary.SelectMany(kvp => kvp.Value).ToList();
            //lvDisplay.ItemsSource = allEvents;
            displayedEvents.Clear();
            foreach (var events in eventDictionary.Values.SelectMany(e => e))
            {
                displayedEvents.Add(events); 
            }
            lvDisplay.ItemsSource = displayedEvents;
        }

        private void PopulateCategoryCombobox()
        {
            cbCategories.Items.Add("Select Category");

            foreach (var category in uniqueCategories)
            {
                cbCategories.Items.Add(category);
            }

            cbCategories.SelectedIndex = 0;
        }

        private void SetSearchDate()
        {
            dpEventDate.SelectedDate = DateTime.Today;
        }

        private void btnSearchEvents_Click(object sender, RoutedEventArgs e)
        {

            string selectedCategory = cbCategories.SelectedItem.ToString();
            if (selectedCategory == "Select Category")
            {
                MessageBox.Show("Please select a valid category");
                return;
            }

            if (!dpEventDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a valid date");
                return;
            }
            DateTime selectedDate = dpEventDate.SelectedDate.Value;

            userSearchHistory.RecordSearch(selectedCategory, selectedDate);

            string searchKey = $"{selectedCategory}_{selectedDate.ToShortDateString()}";

            if (eventDictionary.TryGetValue(searchKey, out var filteredEvents))
            {
                displayedEvents.Clear(); 
                foreach (var ev in filteredEvents)
                {
                    displayedEvents.Add(ev); 
                }
                //lvDisplay.ItemsSource = filteredEvents; 
            }
            else
            {
                MessageBox.Show("No events found for the selected category and date.");
                DisplayAllEvents(); 
            }

            //UpdateRecommendations();
            ClearSearch();
        }

        private void ClearSearch()
        {
            cbCategories.SelectedIndex = 0;
            dpEventDate.SelectedDate = DateTime.Today;
        }

        private void btnOrganiseByDate_Click(object sender, RoutedEventArgs e)
        {
            displayedEvents.Clear();

            var sortedEvents = new List<Event>();
            while (eventPriorityQueue.Count > 0)
            {
                sortedEvents.Add(eventPriorityQueue.Dequeue()); 
            }

            foreach (var ev in sortedEvents)
            {
                displayedEvents.Add(ev); 
            }

            InitializePriorityQueue();
        }

        private void btnEventsList_Click(object sender, RoutedEventArgs e)
        {
            DisplayAllEvents();
        }

        private void btnOpenAddEvent_Click(object sender, RoutedEventArgs e)
        {
            AddEventWindow addEventWindow = new AddEventWindow();

            if (addEventWindow.ShowDialog() == true) 
            {
                Event newEvent = addEventWindow.NewEvent;

                AddEventToDictionary(newEvent); 
                DisplayAllEvents(); 
            }
        }

        private void AddEventToDictionary(Event newEvent)
        {
            string key = $"{newEvent.Category}_{newEvent.Date.ToShortDateString()}";

            if (!eventDictionary.ContainsKey(key))
            {
                eventDictionary[key] = new List<Event>();
            }

            eventDictionary[key].Add(newEvent);

            InitializePriorityQueue() ;

            uniqueCategories.Add(newEvent.Category);
            uniqueDates.Add(newEvent.Date);
        }

        private List<Event> GenerateRecommendations()
        {
            List<Event> recommendedEvents = new List<Event>();

            var topCategories = userSearchHistory.GetTopCategories();
            var topDates = userSearchHistory.GetTopDates();

            foreach (var category in topCategories)
            {
                foreach (var categoryEvents in eventDictionary)
                {
                    if (categoryEvents.Key.StartsWith(category))
                    {
                        recommendedEvents.AddRange(categoryEvents.Value);
                    }
                }
            }

            foreach (var date in topDates)
            {
                foreach (var categoryEvents in eventDictionary.Values)
                {
                    recommendedEvents.AddRange(categoryEvents.Where(ev => ev.Date == date));
                }
            }

            return recommendedEvents.Distinct().ToList(); 
        }

        private void UpdateRecommendations()
        {
            List<Event> recommendations = GenerateRecommendations();

            lvDisplay.ItemsSource = recommendations;
        }

        private void btnRecommendations_Click(object sender, RoutedEventArgs e)
        {            
            UpdateRecommendations();
        }

        private void InitializeAnnouncementDictionary()
        {
            announcementDictionary = new Dictionary<string, List<Announcement>>();

            var sampleAnnouncement = new List<Announcement>
            {
                new Announcement { Name = "Electricity", Description = "Loadshedding to resume", Date = DateTime.Now.AddDays(26) },
                new Announcement { Name = "Water", Description = "Water to be off due to maintenance", Date = DateTime.Now.AddDays(22) },
                new Announcement { Name = "Roads", Description = "Fixing potholes", Date = DateTime.Now.AddDays(35) },
                new Announcement { Name = "Roads", Description = "Roadwors to continue", Date = DateTime.Now.AddDays(7) }
            };

            foreach(var announcement in sampleAnnouncement)
            {
                string key = $"{announcement.Name}_{announcement.Date.ToShortDateString()}";

                if (!announcementDictionary.ContainsKey(key))
                {
                    announcementDictionary[key] = new List<Announcement>();
                }

                announcementDictionary[key].Add(announcement);
            }

            displayedAnnouncements = new ObservableCollection<Announcement>(sampleAnnouncement);
            //lvDisplay.ItemsSource = displayedAnnouncements;
        }

        private void btnAnnouncementsList_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementWindow announcementWindow = new AnnouncementWindow(displayedAnnouncements);

            announcementWindow.ShowDialog();
                //InitializeAnnouncementDictionary();
            
        }
    }

    public enum Categories
    {
        Music,
        Sports,
        Education,
        Technology,
        Art
    }
}
