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
        public ProductPage()
        {
            InitializeComponent();
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

            var product = new Product
            {
                Name = tbName.Text,
                Count = int.Parse(tbCount.Text),
                Price = int.Parse(tbPrice.Text),
            };

            DataAccess.AddProduct(product);
            NavigationService.GoBack();
        }
    }
}
