using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using System.Data.SqlClient;

namespace WpfProject
{

    public partial class Registration : Window
    {
        private readonly MainWindow _mainWindow;
        string file = "";

        DbProviderFactory factory = null;
        DbConnection connection = null;
        string connectionString = null;
        public Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            string fileName = System.IO.Path.GetFullPath(file[0]);
            if(fileName != null)
            {
                var fileNames = fileName as string; 
                if(fileNames.Length > 0)
                {

                }
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                lbl.Content = files[0];
                img2.Source = null;
                file = files[0];

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (nam.Text == "")
                {
                    nam.BorderBrush = Brushes.Red;
                }
                if (Pass.Password == "")
                {
                    Pass.BorderBrush = Brushes.Red;

                }
                if (CPass.Password == "" || CPass.Password != Pass.Password)
                {
                    CPass.BorderBrush = Brushes.Red;
                }
                if (nam.Text != "" && Pass.Password != "" && CPass.Password != "" && CPass.Password == Pass.Password)
                {

                    nam.BorderBrush = Brushes.Gray;
                    CPass.BorderBrush = Brushes.Gray;
                    Pass.BorderBrush = Brushes.Gray;

                    factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    connection = factory.CreateConnection();

                    connectionString = GetConnectionString("System.Data.SqlClient");

                    connection.ConnectionString = connectionString;
                    if (profilePicture.ImageSource == null)
                    {
                        lbl.Content = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";
                        _mainWindow.labe.Content = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";
                    }

                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = connection.CreateCommand();
                    adapter.SelectCommand.CommandText = "INSERT INTO Profile(Login,Password,ImageAddress)VALUES('" + nam.Text + "','" + CPass.Password + "','" + lbl.Content + "')";
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Profile");
                    _mainWindow.nickname.Content = nam.Text;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) 
                {
                    MessageBox.Show("This login is already exist");
                }
            }
            _mainWindow.letters.Visibility = Visibility.Visible;
            _mainWindow.labe.Content = file;
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
