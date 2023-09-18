using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace WpfFinalProject
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public struct Items
    {
        public string ImageAddress { get; set; }
        public string message { get; set; }
    }
    public struct ChatItems
    {
        public string UsersImageAddress { get; set; }
        public string UserNames { get; set; }
    }
    public class IdItems
    {
        public string UsersImageAddress { get; set; }
        public string UserNames { get; set; }
        public string Id { get; set; }
    }
    public partial class Chat : Window
    {
        private readonly MainWindow _mainWindow;
        string address = "";
        int id = 0;
        int idS = 0;
        int chatId = 0;
        int selectedId = 0;
        List<int> lstChatId;
        DbProviderFactory factory = null;
        DbConnection connection = null;
        string connectionString = null;

        private static byte[] _buffer = new byte[1024];
        private static List<Socket> _clientsSockets = new List<Socket>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Chat(string adress, int idish)
        {
            InitializeComponent();
            id = GetId(adress, id);
            address = adress;
            if (idish != 0)
            {
                idS = idish;
                if (!Check(id, idish))
                {
                    Insert(idish);
                }
                else
                {
                    SetId(id, idS);
                }
                Load();

            }
            LoadChats();
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
            lst.Items.Add(new Items
            {
                ImageAddress = address,
                message = txtMessage.Text
            }
            );

            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;

            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = connection.CreateCommand();
            adapter.SelectCommand.CommandText = "INSERT INTO Message(Address,MSG,IdChat)VALUES('" + address + "','" + txtMessage.Text + "','" + chatId + "')";
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Message");


            txtMessage.Text = "";
        }
        public int GetId(string addr, int idish)
        {
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Id FROM Profile WHERE ImageAddress='" + addr + "'";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Profile");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Profile"].DefaultView;

                DataRowView rowview = ImgAdress.Items[0] as DataRowView;
                idish = Convert.ToInt32(rowview.Row[0]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return idish;
        }
        public bool Check(int id, int id2)
        {
            List<int> listId = new List<int>();
            List<int> listSId = new List<int>();
            List<int> listRId = new List<int>();
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Id, SenderId, ReceiverId FROM Chat";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Chat");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Chat"].DefaultView;

                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    listId.Add(Convert.ToInt32(rowview.Row[0]));
                    listSId.Add(Convert.ToInt32(rowview.Row[1]));
                    listRId.Add(Convert.ToInt32(rowview.Row[2]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            for (int i = 0; i < listId.Count; i++)
            {
                if ((listRId[i] == id || listRId[i] == id2) && (listSId[i] == id || listSId[i] == id2))
                {
                    chatId = listId[i];
                    return true;
                }
            }
            return false;
        }
        public void Insert(int idish)
        {
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;

            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = connection.CreateCommand();
            adapter.SelectCommand.CommandText = "INSERT INTO Chat(SenderId, ReceiverId)VALUES('" + id + "','" + idish + "')";
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Chat");
        }
        public void SetId(int ID , int IDS)
        {
            List<int> lstCh = new List<int>();
            factory = null;
            connection = null;
            connectionString = null;

            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Id FROM Chat WHERE (SenderId='"+ ID + "' OR SenderId='"+ IDS + "') AND (ReceiverId='" + ID + "' OR ReceiverId='" + IDS + "')";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Chat");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Chat"].DefaultView;

                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    lstCh.Add(Convert.ToInt32(rowview.Row[0]));
                }
                chatId = lstCh[0];
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show(ex.Message + "    " + line);
            }
        }
        public void Load()
        {
            List<string> lstAdress = new List<string>();
            List<string> lstMsg = new List<string>();
            factory = null;
            connection = null;
            connectionString = null;

            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Address, MSG FROM Message WHERE IdChat='" + chatId + "'";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Message");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Message"].DefaultView;

                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    lstAdress.Add(rowview.Row[0].ToString());
                    lstMsg.Add(rowview.Row[1].ToString());
                }
                for (int i = 0; i < lstAdress.Count; i++)
                {
                    lst.Items.Add(new Items
                    {
                        ImageAddress = lstAdress[i].ToString(),
                        message = lstMsg[i].ToString()
                    }
                    );
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show(ex.Message + "    " + line);
            }
        }
        public void LoadChats()
        {
            List<int> lstId = new List<int>();
            lstChatId = new List<int>();
            factory = null;
            connection = null;
            connectionString = null;

            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Id, SenderId, ReceiverId FROM Chat WHERE SenderId='" + id + "' OR ReceiverId='" + id + "'";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Chat");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Chat"].DefaultView;

                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    if (Convert.ToInt32(rowview[1]) == id)
                    {
                        lstId.Add(Convert.ToInt32(rowview.Row[2]));
                    }
                    else
                    {
                        lstId.Add(Convert.ToInt32(rowview.Row[1]));
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show(ex.Message + "    " + line);
            }
            List<string> lstAdress = new List<string>();
            List<string> lstNames = new List<string>();
            List<string> lstIdish = new List<string>();
            factory = null;
            connection = null;
            connectionString = null;

            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT Id, Login, ImageAddress FROM Profile";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Profile");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Profile"].DefaultView;

                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    lstAdress.Add(rowview.Row[2].ToString());
                    lstNames.Add(rowview.Row[1].ToString());
                    lstIdish.Add(rowview.Row[0].ToString());
                }
                for (int i = 0; i < lstAdress.Count ; i++)
                {
                    foreach (int item in lstId)
                    {
                        if (item == Convert.ToInt32(lstIdish[i]))
                        {
                            ChatView.Items.Add(new IdItems
                            {
                                UsersImageAddress = lstAdress[i].ToString(),
                                UserNames = lstNames[i].ToString(),
                            }
                            );
                            ChatView2.Items.Add(new IdItems
                            {
                                Id = lstIdish[i]
                            }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show(ex.Message + "    " + line);
            }
        }

        private void ChatView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdItems iditem = ChatView2.Items[ChatView.SelectedIndex] as IdItems;
            lst.Items.Clear();
            SetId(id, Convert.ToInt32(iditem.Id));
            Load();
        }
    }
}
