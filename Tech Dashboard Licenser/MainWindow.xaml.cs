using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NewLicenseMenuItem.Click += NewLicenseMenuItem_Click;
            ViewLicenseMenuItem.Click += ViewLicenseMenuItem_Click;
            ExportLicenseMenuItem.Click += ExportLicenseMenuItem_Click;
            AboutMenuItem.Click += AboutMenuItem_Click;
            ExitMenuItem.Click += ExitMenuItem_Click;
            contentArea.Content = new ViewLicenses();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutBox about = new Tech_Dashboard_Licenser.AboutBox();
            about.ShowDialog();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExportLicenseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LicenserContext db = new LicenserContext();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
            {
                var newFile = new FileInfo(saveFileDialog.FileName);
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets.Add("Licenses");
                    ws.Cells["A1"].LoadFromCollection<License>(db.Licenses.AsEnumerable<License>(), true, OfficeOpenXml.Table.TableStyles.Light8);
                    package.Save();
                }
            }
        }

        private void ImportLicenseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                
            }
        }

        private void ViewLicenseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = new ViewLicenses();
        }

        private void NewLicenseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = new NewLicense();
        }
    }
}
