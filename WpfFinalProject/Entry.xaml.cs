using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data;
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
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class Entry : Window
    {
        private readonly MainWindow _mainWindow;
        DbProviderFactory factory = null;
        DbConnection connection = null;
        string connectionString = null;
        public Entry(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Id, ImageAddress FROM Profile WHERE Login='" + nam.Text + "' AND Password='" + Password.Text + "'";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Profile");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Profile"].DefaultView;

                DataRowView rowview = ImgAdress.Items[0] as DataRowView;
                string adres = rowview.Row[1].ToString();
                int id = Convert.ToInt32(rowview.Row[0]);
                _mainWindow.labe.Content = adres;
                _mainWindow.profId.Content = id;
                _mainWindow.letters.Visibility = Visibility.Visible;
                _mainWindow.nickname.Content = nam.Text;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
