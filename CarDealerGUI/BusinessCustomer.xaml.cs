using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Libraries;

namespace CarDealerGUI
{
    /// <summary>
    /// Interaction logic for BusinessCustomer.xaml
    /// </summary>
    public partial class BusinessCustomer : Window
    {
        public BusinessCustomer()
        {
            InitializeComponent();
          
            
            
        }

        private void BCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BResetButton_Click(object sender, RoutedEventArgs e)
        {
            BSAddressBox.Clear();
            BSContactBox.Clear();
            BSFaxBox.Clear();
            BSphoneBox.Clear();
            BSSEnr.Clear();
            CompanyNameBox.Clear();
        }
    }
}
