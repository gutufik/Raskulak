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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var password = pbPassword.Password;
            DataAccess.AddUser(new User {Login = login, Role = DataAccess.GetRole("Client"), Password = password });
            App.User = DataAccess.GetUser(login, password);
            NavigationService.Navigate(new Pages.CatalogPage());
        }
    }
}
