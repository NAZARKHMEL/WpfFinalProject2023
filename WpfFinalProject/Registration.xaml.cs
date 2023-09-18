using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
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
using System.Configuration;

namespace WpfFinalProject
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
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
            if (fileName != null)
            {
                var fileNames = fileName as string;
                if (fileNames.Length > 0)
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
                        lbl.Content = "C:\\Users\\Nazar\\Pictures\\Screenshots\\NonProfImage.png";
                        _mainWindow.labe.Content = "C:\\Users\\Nazar\\Pictures\\Screenshots\\NonProfImage.png";
                    }
                    _mainWindow.letters.Visibility = Visibility.Visible;
                    if (file != "")
                    {
                        _mainWindow.labe.Content = file;
                    }
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = connection.CreateCommand();
                    adapter.SelectCommand.CommandText = "INSERT INTO Profile(Login,Password,ImageAddress,PostAddress)VALUES('" + nam.Text + "','" + CPass.Password + "','" + lbl.Content + "','" + epost.Text +"')";
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Profile");
                    GetId();
                }
            }
            catch (SqlException ex)
            {
                    MessageBox.Show(ex.Message);
            }
            this.Close();
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

        private void GetId()
        {
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");
            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Id, ImageAddress FROM Profile WHERE Login='" + nam.Text + "' AND Password='" + CPass.Password + "'";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Profile");

                ImgAddress.ItemsSource = null;
                ImgAddress.ItemsSource = ds.Tables["Profile"].DefaultView;

                DataRowView rowview = ImgAddress.Items[0] as DataRowView;
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
    }
}