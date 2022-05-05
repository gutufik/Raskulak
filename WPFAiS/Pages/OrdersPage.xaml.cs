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
using Core;

namespace Raskulak.Pages
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public List<Order> Orders { get; set; }
        public OrdersPage(List<Order> orders)
        {
            InitializeComponent();
            Orders = orders;
            if (Orders.Count == 0)
                lblEmpty.Visibility = Visibility.Visible;
            else if (App.User.Role.Name != "Client")
                btnDelete.Visibility = Visibility.Visible;
            DataContext = this;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var order = (lvOrders.SelectedItem as Order);
            if (order != null)
            { 
                DataAccess.DeleteOrder(order);
                Orders.Remove(order);
                lvOrders.Items.Refresh();
            }
        }
    }
}
