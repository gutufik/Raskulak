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
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var password = pbPassword.Password;

            App.User = DataAccess.GetUser(login, password);
            if (App.User == null)
                MessageBox.Show("Неверный логин или пароль");
            else
                NavigationService.Navigate(new Pages.CatalogPage());
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.RegistrationPage());
        }
    }
}
