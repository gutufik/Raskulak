﻿using System;
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
    /// Interaction logic for BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        public List<BasketItem> BasketItems { get; set; }
        public BasketPage()
        {
            InitializeComponent();

            BasketItems = DataAccess.GetBasketItems(App.User);
            if (BasketItems.Count == 0)
            {
                lblEmpty.Visibility = Visibility.Visible;
                btnOrder.Visibility = Visibility.Hidden;
            }
            DataContext = this;
        }
    }
}
