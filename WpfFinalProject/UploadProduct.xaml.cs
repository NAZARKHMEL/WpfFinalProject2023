using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace WpfFinalProject
{
    /// <summary>
    /// Interaction logic for UploadProduct.xaml
    /// </summary>
    public partial class UploadProduct : Window
    {
        private readonly MainWindow _mainWindow;

        DbProviderFactory factory = null;
        DbConnection connection = null;
        string connectionString = null;
        List<string> strings = new List<string>();
        List<string> names = new List<string>();
        List<string> prices = new List<string>();
        List<string> idishniki = new List<string>();
        List<string> descriptions = new List<string>();
        int iR = 0;
        int miR = 0;
        List<int> arr = new List<int>();
        public UploadProduct(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                lbl.Content = files[0];
                kub.Visibility = Visibility.Hidden;
                img.Source = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextRange textRange = new TextRange(
                prodDescrib.Document.ContentStart,
                prodDescrib.Document.ContentEnd
                );
                try
                {
                    if (prodnam.Text == "")
                    {
                        prodnam.BorderBrush = Brushes.Red;
                    }
                    if (textRange.Text == "")
                    {
                        prodnam.BorderBrush = Brushes.Red;
                    }
                    if (prodnam.Text != "" && textRange.Text != "")
                    {

                        prodnam.BorderBrush = Brushes.Gray;

                        factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                        connection = factory.CreateConnection();

                        connectionString = GetConnectionString("System.Data.SqlClient");

                        connection.ConnectionString = connectionString;
                        DbDataAdapter adapter = factory.CreateDataAdapter();
                        adapter.SelectCommand = connection.CreateCommand();
                        adapter.SelectCommand.CommandText = "INSERT INTO Product(Name,Description,ImageAddress,Price,IdProfile)VALUES('" + prodnam.Text + "','" + textRange.Text + "','" + lbl.Content + "','" + prodPrice.Text + "','" + _mainWindow.profId.Content + "')";
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Product");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show(ex.Message + "    " + line);
            }
            ((MainWindow)this.Owner).GetProducts();
        }
        private string GetConnectionString(string providerName)
        {
            string result = null;
            ConnectionStringSettingsCollection settings =
            ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                foreach (ConnectionStringSettings item in settings)
                {
                    if (item.ProviderName == providerName)
                    {
                        result = item.ConnectionString;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
