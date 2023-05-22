using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        DbProviderFactory factory = null;
        DbConnection connection = null;
        string connectionString = null;
        private readonly MainWindow _mainWindow;
        public ProductView(string address, string name, string price, string id, string describe, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            ProdAddress.Content = address;
            ProdName.Content = name;
            ProdPrice.Content = price+" $";
            ProdDescripe.Content = describe;
            string idi = id;

            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Login, ImageAddress FROM Profile WHERE Id='" + idi + "'";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Profile");

                ProfInfo.ItemsSource = null;
                ProfInfo.ItemsSource = ds.Tables["Profile"].DefaultView;

                DataRowView rowview = ProfInfo.Items[0] as DataRowView;
                string log = rowview.Row[0].ToString();
                address = rowview.Row[1].ToString();
                SellerLogin.Content = log;
                ProfImg.Content = address;
                connection.Close();
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show(ex.Message + "    " + line);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow.letters.Visibility == Visibility.Visible)
            {
                Chat ch = new Chat();
                ch.Show();
            }
            else
            {
                MessageBox.Show("You need a account for chatting with another people");
            }
        }
    }
}
