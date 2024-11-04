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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MunicipalityApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for ServiceStatusView.xaml
    /// </summary>
    public partial class ServiceStatusView : UserControl
    {
        private BinarySearchTree bst = new BinarySearchTree();
        private MinHeap minHeap = new MinHeap();
        private AVLTree avlTree = new AVLTree();
        private List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

        public ServiceStatusView()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            AddServiceRequest(new ServiceRequest(3,"Adding new street lights", "Pending", 3, DateTime.Now.AddDays(-5)));
            AddServiceRequest(new ServiceRequest(2, "Fixing the potholes", "In Progress", 2, DateTime.Now.AddDays(-3)));
            AddServiceRequest(new ServiceRequest(1, "Cleaning storm drains", "Completed", 3, DateTime.Now.AddDays(-1)));
            AddServiceRequest(new ServiceRequest(4, "Cutting grass around community", "In Progress", 3, DateTime.Now.AddDays(-2)));
            AddServiceRequest(new ServiceRequest(5, "Repairing street signs", "Pending", 2, DateTime.Now.AddDays(-8)));
            AddServiceRequest(new ServiceRequest(6, "Fixing broken sidewalks", "In Progress", 1, DateTime.Now.AddDays(-10)));
            AddServiceRequest(new ServiceRequest(7, "Installing speed bumps", "Completed", 3, DateTime.Now.AddDays(-15)));
            AddServiceRequest(new ServiceRequest(8, "Tree trimming", "Pending", 2, DateTime.Now.AddDays(-7)));
            AddServiceRequest(new ServiceRequest(9, "Repairing public benches", "In Progress", 1, DateTime.Now.AddDays(-6)));
            AddServiceRequest(new ServiceRequest(10, "Replacing damaged streetlights", "Completed", 2, DateTime.Now.AddDays(-9)));
            AddServiceRequest(new ServiceRequest(11, "Pest control in parks", "Pending", 3, DateTime.Now.AddDays(-12)));
            AddServiceRequest(new ServiceRequest(12, "Fixing water fountains", "In Progress", 2, DateTime.Now.AddDays(-14)));
            AddServiceRequest(new ServiceRequest(13, "Garbage collection schedule update", "Completed", 1, DateTime.Now.AddDays(-11)));
            AddServiceRequest(new ServiceRequest(14, "Painting pedestrian crossings", "Pending", 3, DateTime.Now.AddDays(-4)));
            AddServiceRequest(new ServiceRequest(15, "Repairing playground equipment", "In Progress", 2, DateTime.Now.AddDays(-13)));
            AddServiceRequest(new ServiceRequest(16, "Clearing overgrown pathways", "Completed", 1, DateTime.Now.AddDays(-3)));
            AddServiceRequest(new ServiceRequest(17, "Enhancing park lighting", "Pending", 2, DateTime.Now.AddDays(-9)));
            AddServiceRequest(new ServiceRequest(18, "Repainting community center", "In Progress", 3, DateTime.Now.AddDays(-16)));
            AddServiceRequest(new ServiceRequest(19, "Repairing community pool", "Completed", 2, DateTime.Now.AddDays(-18)));
            AddServiceRequest(new ServiceRequest(20, "Planting new trees", "Pending", 1, DateTime.Now.AddDays(-20)));

            DisplayRequestsFromBST();
        }

        private void AddServiceRequest(ServiceRequest request)
        {
            bst.Insert(request);
            minHeap.Insert(request);
            avlTree.Insert(request);
        }

        private void DisplayRequestsFromBST()
        {
            serviceRequests.Clear();

            TraverseInOrder(bst.Root);

            dataGridRequests.ItemsSource = null; 
            dataGridRequests.ItemsSource = serviceRequests;
        }

        private void TraverseInOrder(BSTNode node)
        {
            if (node == null) return;

            TraverseInOrder(node.Left);
            serviceRequests.Add(node.Request);  
            TraverseInOrder(node.Right);
        }

        private void DisplayRequestsFromAVLTree()
        {
            serviceRequests.Clear();
            TraverseAVLInOrder(avlTree.Root);
            dataGridRequests.ItemsSource = null;
            dataGridRequests.ItemsSource = serviceRequests;
        }

        private void DisplayRequestsFromMinHeap()
        {
            TraverseMinHeap();
        }

        private void TraverseAVLInOrder(AVLNode node)
        {
            if (node == null) return;
            TraverseAVLInOrder(node.Left);
            serviceRequests.Add(node.Request);
            TraverseAVLInOrder(node.Right);
        }

        private void ViewBST_Click(object sender, RoutedEventArgs e)
        {
            DisplayRequestsFromBST();
        }

        private void ViewAVLTree_Click(object sender, RoutedEventArgs e)
        {
            DisplayRequestsFromAVLTree();
        }

        private void ViewMinHeap_Click(object sender, RoutedEventArgs e)
        {
            DisplayRequestsFromMinHeap();
        }

        private void TraverseMinHeap()
        {
            var tempHeap = new MinHeap(minHeap);  
            serviceRequests.Clear();              

            while (!tempHeap.IsEmpty())
            {
                serviceRequests.Add(tempHeap.ExtractMin()); 
            }

            dataGridRequests.ItemsSource = null;
            dataGridRequests.ItemsSource = serviceRequests;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(searchTextBox.Text, out int searchId))
            {
                ServiceRequest result = bst.Search(searchId); 

                if (result != null)
                {
                    serviceRequests.Clear();
                    serviceRequests.Add(result);
                    dataGridRequests.ItemsSource = null;
                    dataGridRequests.ItemsSource = serviceRequests;
                }
                else
                {
                    MessageBox.Show("Service Request not found.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric ID.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
