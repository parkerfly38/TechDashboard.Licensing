using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Xceed.Wpf.Toolkit;

namespace Tech_Dashboard_Licenser
{
    /// <summary>
    /// Interaction logic for NewLicense.xaml
    /// </summary>
    public partial class NewLicense : UserControl
    {
        public NewLicense()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtCompanyName.Text.Length <= 0)
            {
                var result = System.Windows.MessageBox.Show("Please enter a company name.", "Company Name Required", MessageBoxButton.OK);
                return;
            }
            if (txtTotalLicenseCount.Text.Length <= 0 || txtTotalLicenseCount.Value <= 0)
            {
                var result = System.Windows.MessageBox.Show("Please enter a license count greater than zero.", "License Count Required", MessageBoxButton.OK);
                return;
            }
            if (cbTypeOfLicense.SelectedIndex < 0)
            {
                var result = System.Windows.MessageBox.Show("Please select a license type.", "License Type Required", MessageBoxButton.OK);
                return;
            }
            if (dateLicenseIssueDate.SelectedDate == null)
            {
                var result = System.Windows.MessageBox.Show("Please select a valid issue date.", "License Issue Date Required", MessageBoxButton.OK);
                return;
            }
            if (dateLicenseExpirationDate.SelectedDate == null)
            {
                var result = System.Windows.MessageBox.Show("Please selected a valid expiration date.", "License Expiration Date Required", MessageBoxButton.OK);
                return;
            }
            if (dateLicenseIssueDate.SelectedDate >= dateLicenseExpirationDate.SelectedDate)
            {
                var result = System.Windows.MessageBox.Show("Expiration date must occur after issue date.", "Invalid Date Selections", MessageBoxButton.OK);
                return;
            }

            //prep our encrtyped string
            SimpleAES encryptText = new SimpleAES("V&WWJ3d39brdR5yUh5(JQGHbi:FB@$^@", "W4aRWS!D$kgD8Xz@");

            LicenserContext db = new LicenserContext();
            License newLicense = new License
            {
                CompanyName = txtCompanyName.Text,
                TotalLicenseCount = Convert.ToInt32(txtTotalLicenseCount.Text),
                TypeOfLicense = ((ComboBoxItem)cbTypeOfLicense.SelectedItem).Content.ToString(),
                LicenseIssueDate = (DateTime)dateLicenseIssueDate.SelectedDate,
                LicenseExpirationDate = (DateTime)dateLicenseExpirationDate.SelectedDate,
                LicenseEncodedString = encryptText.EncryptToString(txtCompanyName.Text + ";" + txtTotalLicenseCount.Text + ";" +
                    ((ComboBoxItem)cbTypeOfLicense.SelectedItem).Content.ToString() + ";" + dateLicenseIssueDate.SelectedDate.ToString() + ";" +
                    dateLicenseExpirationDate.SelectedDate.ToString())
            };

            db.Licenses.Add(newLicense);
            db.SaveChanges();

            ContentControl contentArea = (ContentControl)this.Parent;
            contentArea.Content = new ViewLicenses();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtCompanyName.Text = "";
            txtTotalLicenseCount.Text = "";
            dateLicenseExpirationDate.ClearValue(DatePicker.SelectedDateProperty);
            dateLicenseIssueDate.ClearValue(DatePicker.SelectedDateProperty);
            cbTypeOfLicense.SelectedIndex = -1;
        }
    }
}
