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
using Tech_Dashboard_Licenser.Models;

namespace Tech_Dashboard_Licenser
{
    /// <summary>
    /// Interaction logic for ViewLicenses.xaml
    /// </summary>
    public partial class ViewLicenses : UserControl
    {
        LicenserContext db = new LicenserContext();

        public ViewLicenses()
        {
            InitializeComponent();
            LicenserContext db = new LicenserContext();
            dataGrid.ItemsSource = db.Licenses.ToList();
        }

        private void ShowCode_Click(object sender, RoutedEventArgs e)
        {
            int LicenseID = (int)((Button)sender).CommandParameter;
            var item = db.Licenses.Where(x => x.LicenseID == LicenseID).FirstOrDefault<License>();
            var result = System.Windows.MessageBox.Show("Click OK to copy this code to clipboard:" + Environment.NewLine + item.LicenseEncodedString, "License Code", MessageBoxButton.OK);
            Clipboard.SetText(item.LicenseEncodedString);
            return;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Are you sure you want to delete this license?  Unless you have backed it up via an Excel export, it will be gone forever.", "Confirm Deletion", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            int LicenseID = (int)((Button)sender).CommandParameter;
            var item = db.Licenses.Where(x => x.LicenseID == LicenseID).FirstOrDefault<License>();
            db.Licenses.Remove(item);
            db.SaveChanges();
            dataGrid.ItemsSource = db.Licenses.ToList();
        }
    }
}
