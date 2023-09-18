using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
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
using System.Net.Mail;
using System.Net;
using System.Windows.Interop;

namespace WpfFinalProject
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
        string idi = "";
        string usernam = "";
        string imgAdd="";
        string ema = "";
        Chat ch;
        public ProductView(string address, string name, string price, string id, string describe, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            ProdAddress.Content = address;
            ProdName.Content = name;
            ProdPrice.Content = price;
            ProdDescripe.Content = describe;
            idi = id;
            imgAdd = address;
             
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Login, ImageAddress, PostAddress FROM Profile WHERE Id='" + idi + "'";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Profile");

                ProfInfo.ItemsSource = null;
                ProfInfo.ItemsSource = ds.Tables["Profile"].DefaultView;

                DataRowView rowview = ProfInfo.Items[0] as DataRowView;
                string log = rowview.Row[0].ToString();
                address = rowview.Row[1].ToString();
                ema = rowview.Row[2].ToString();
                SellerLogin.Content = log;
                ProfImg.Content = address;
                usernam = _mainWindow.nickname.Content.ToString();
                
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
                string fromMail = "naz.finalproject@gmail.com";
                string fromPassword = "niuegcirapdktefa";

                MailMessage message = new MailMessage();

                message.From = new MailAddress(fromMail);
                message.To.Add(new MailAddress(ema));
                message.Subject = $"New Chat";
                message.Body = $"User with nickname {usernam} start chat with you through:";
                message.IsBodyHtml = true;

                message.Attachments.Add(new Attachment(imgAdd));
                var smtpclient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };
                smtpclient.Send(message);

                ch = new Chat(_mainWindow.labe.Content.ToString(), Convert.ToInt32(idi));
                ch.Show();
            }
            else
            {
                MessageBox.Show("You need a account for chatting with another people");
            }
        }
    }
}

