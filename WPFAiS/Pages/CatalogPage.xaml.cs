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
    /// Interaction logic for CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        public List<Product> Products { get; set; }
        public CatalogPage()
        {
            InitializeComponent();
            Products = DataAccess.GetProducts();
            DataAccess.NewItemAddedEvent += RefreshProducts;
            if (App.User.Role.Name == "Client")
            { 
                btnAddProduct.Visibility = Visibility.Hidden;
                btnAllOrders.Visibility = Visibility.Hidden;
            }

            DataContext = this;
        }
        private void RefreshProducts()
        {
            Products = DataAccess.GetProducts();
            lvProducts.ItemsSource = Products;
            lvProducts.Items.Refresh();
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProducts.SelectedItem != null)
            {
                spProduct.Visibility = Visibility.Visible;
                lblEmpty.Visibility = Visibility.Hidden;
                tbProduct.Text = (lvProducts.SelectedItem as Product).Name;
            }
            else
            { 
                lblEmpty.Visibility = Visibility.Visible;
                spProduct.Visibility = Visibility.Hidden;
            }
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.BasketPage());
        }

        private void btnAddToBasket_Click(object sender, RoutedEventArgs e)
        {
            var product = lvProducts.SelectedItem as Product;
            int count;
            if (int.TryParse(tbCount.Text, out count) && count > 0 && count <= product.Count)
            {
                DataAccess.UpdateCount(product, count);

                DataAccess.AddToBasket(product, count, App.User);
            }
            else 
            {
                MessageBox.Show("Неверное количество продукта");
            }
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.OrdersPage(DataAccess.GetOrders(App.User)));
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProductPage());
        }

        private void btnAllOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.OrdersPage(DataAccess.GetOrders()));
        }
    }
}
