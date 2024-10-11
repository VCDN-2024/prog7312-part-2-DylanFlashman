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
        private PriorityQueue<Event, DateTime> eventPriorityQueue;
        private ObservableCollection<Event> displayedEvents;
        private HashSet<string> uniqueCategories;
        private HashSet<DateTime> uniqueDates;

        public EventAnnouncements()
        {
            InitializeComponent();
            //PopulateCategoryCombobox();
            SetSearchDate();
            InitializeEventDictionary();
            DisplayAllEvents();
            InitializePriorityQueue();
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
                displayedEvents.Add(events); // Add all events to the ObservableCollection
            }
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

            string searchKey = $"{selectedCategory}_{selectedDate.ToShortDateString()}";

            if (eventDictionary.TryGetValue(searchKey, out var filteredEvents))
            {
                displayedEvents.Clear(); // Clear current events
                foreach (var ev in filteredEvents)
                {
                    displayedEvents.Add(ev); // Add filtered events to the ListView
                }
                //lvDisplay.ItemsSource = filteredEvents; 
            }
            else
            {
                MessageBox.Show("No events found for the selected category and date.");
                DisplayAllEvents(); 
            }

            ClearSearch();
        }

        private void ClearSearch()
        {
            cbCategories.SelectedIndex = 0;
            dpEventDate.SelectedDate = DateTime.Today;
        }

        private void btnOrganiseByDate_Click(object sender, RoutedEventArgs e)
        {
            displayedEvents.Clear(); // Clear the ListView before adding sorted events

            var sortedEvents = new List<Event>();
            while (eventPriorityQueue.Count > 0)
            {
                sortedEvents.Add(eventPriorityQueue.Dequeue()); // Add events from the priority queue
            }

            foreach (var ev in sortedEvents)
            {
                displayedEvents.Add(ev); // Add them to the ObservableCollection
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
