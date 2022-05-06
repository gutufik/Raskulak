using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public Product Product { get; set; }
        public ProductPage()
        {
            InitializeComponent();
        }
        public ProductPage(Product product)
        {
            InitializeComponent();
            Product = product;
            tbCount.Text = product.Count.ToString();
            tbPrice.Text = product.Price.ToString();
            tbName.Text = product.Name;
            tbImage.Text = product.ImageLink;
            tbCreator.Text = product.Creator.Login;
            tbDate.Text = product.CreationDate.ToString();
            spCreator.Visibility = Visibility.Visible;
            if (App.User.Role.Name == "Client")
            {
                spProduct.IsEnabled = false;
                btnAddProduct.Visibility = Visibility.Hidden;
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            int count;
            int price;

            if (!(int.TryParse(tbCount.Text, out count)
                && int.TryParse(tbPrice.Text, out price)
                && count > 0 && price > 0))
            {
                MessageBox.Show("Данные запонение не верно");
                return;
            }
            BitmapImage bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(tbImage.Text, UriKind.Absolute);
                bitmap.EndInit();
            }
            catch { }


            var product = new Product
            {
                Name = tbName.Text,
                Count = int.Parse(tbCount.Text),
                CreationDate = DateTime.Now,
                Creator = App.User,
                Price = int.Parse(tbPrice.Text),
                ImageLink = tbImage.Text
            };


            if (Product == null)
                DataAccess.AddProduct(product);
            else
            {
                Product.Name = tbName.Text;
                Product.Count = int.Parse(tbCount.Text);
                Product.CreationDate = DateTime.Now;
                Product.Creator = App.User;
                Product.Price = int.Parse(tbPrice.Text);
                Product.ImageLink = tbImage.Text;
                DataAccess.UpdateProduct(Product);
            }

            NavigationService.GoBack();
        }

        private void tbImage_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(tbImage.Text, UriKind.Absolute);
                bitmap.EndInit();

                imgProduct.Source = bitmap;
            }
            catch { }
        }
    }
}
