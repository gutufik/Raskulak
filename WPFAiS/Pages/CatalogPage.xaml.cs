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
            spProduct.Visibility = Visibility.Visible;
            tbProduct.Text = (lvProducts.SelectedItem as Product).Name;
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.BasketPage());
        }

        private void btnAddToBasket_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.AddToBasket(lvProducts.SelectedItem as Product, int.Parse(tbCount.Text), App.User);
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProductPage());
        }
    }
}
