using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace WpfFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> strings = new List<string>();
        List<string> names = new List<string>();
        List<string> prices = new List<string>();
        List<string> idishniki = new List<string>();
        List<string> descriptions = new List<string>();
        DbProviderFactory factory = null;
        DbConnection connection = null;
        string connectionString = null;
        int iR = 0;
        int miR = 0;
        List<int> arr;
        public static List<Product> products { get; set; }

        int t = 0;
        public MainWindow()
        {
            InitializeComponent();
            GetProducts();
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Registration taskWindow = new Registration(this);
            taskWindow.Owner = this;
            taskWindow.Show();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Registration taskWindow = new Registration(this);
            taskWindow.Owner = this;
            taskWindow.Show();
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Entry taskWindow = new Entry(this);
            taskWindow.Owner = this;
            taskWindow.Show();
        }

        private void tripoloski_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (t == 0)
            {
                var WidthAnimation = new DoubleAnimation() { To = 130, Duration = TimeSpan.FromSeconds(0.6) };

                menu.BeginAnimation(StackPanel.WidthProperty, WidthAnimation);
                t = 1;
            }
            else if (t == 1)
            {
                var WidthAnimation = new DoubleAnimation() { To = 0, Duration = TimeSpan.FromSeconds(0.6) };

                menu.BeginAnimation(StackPanel.WidthProperty, WidthAnimation);
                t = 0;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (letters.Visibility == Visibility.Visible)
            {
                UploadProduct taskWindow = new UploadProduct(this);
                taskWindow.Owner = this;
                taskWindow.Show();
            }
            else
            {
                MessageBox.Show("You need to use exist profile or registrate new");
            }
        }

        private void Upload_MouseEnter(object sender, MouseEventArgs e)
        {
            var text = sender as TextBlock;
            text.Foreground = Brushes.White;
        }

        private void Upload_MouseLeave(object sender, MouseEventArgs e)
        {
            var text = sender as TextBlock;
            text.Foreground = Brushes.Black;
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
        public void GetProducts()
        {
            arr = new List<int>();
            strings = new List<string>();
            names = new List<string>();
            prices = new List<string>();
            idishniki = new List<string>();
            descriptions = new List<string>();
            miR = 0;
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT ImageAddress, Name, Price, IdProfile, Description FROM Product";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Product");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Product"].DefaultView;

                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    strings.Add(rowview.Row[0].ToString());
                }
                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    names.Add(rowview.Row[1].ToString());
                }
                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    prices.Add(rowview.Row[2].ToString() +"$");
                }
                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    idishniki.Add(rowview.Row[3].ToString());
                }
                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    descriptions.Add(rowview.Row[4].ToString());
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int count = strings.Count;

            while (count > 0)
            {
                if (count > 11)
                {
                    arr.Add(12);
                    count -= 12;
                    miR++;
                }
                else if (count <= 11)
                {
                    arr.Add(count);
                    count -= count;
                    miR++;
                }
            }
            List<Label> labels = new List<Label>();
            List<Label> labels2 = new List<Label>();
            List<Label> labels3 = new List<Label>();
            List<Label> labels4 = new List<Label>();
            List<Label> labels5 = new List<Label>();
            foreach (Label lbl in PLabes.Children)
            {
                labels.Add(lbl);
            }
            foreach (Label lbl in PLabes2.Children)
            {
                labels2.Add(lbl);
            }
            foreach (Label lbl in PLabes3.Children)
            {
                labels3.Add(lbl);
            }
            foreach (Label lbl in PLabes4.Children)
            {
                labels4.Add(lbl);
            }
            foreach (Label lbl in PLabes5.Children)
            {
                labels5.Add(lbl);
            }
            if (arr.Count > 0)
            {

                for (int i = 0; i < 12; i++)
                {
                    labels[i].Content = "";
                    labels2[i].Content = "";
                    labels3[i].Content = "";
                    labels4[i].Content = "Hidden";
                    labels5[i].Content = "";
                }
                for (int i = 0; i < arr[iR]; i++)
                {
                    labels[i].Content = strings[i + (iR * 12)];
                    labels2[i].Content = names[i + (iR * 12)];
                    labels3[i].Content = prices[i + (iR * 12)];
                    labels4[i].Content = "Vissible";
                    labels5[i].Content = idishniki[i + (iR * 12)];
                }
            }
            else
            {
                foreach (Label lbl in PLabes4.Children)
                {
                    lbl.Content = "Hidden";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (iR > 0 && (src.Text == "Search" || src.Text.Length == 0))
            {
                iR--;
                NumbOfPage.Content = iR + 1;
                GetProducts();
            }
            else
            {
                iR--;
                NumbOfPage.Content = iR + 1;
                GetSrcProduct(src.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iR < miR - 1 && (src.Text == "Search" || src.Text.Length == 0))
                {
                    iR++;
                    NumbOfPage.Content = iR + 1;
                    GetProducts();
                }
                else
                {
                    iR++;
                    NumbOfPage.Content = iR + 1;
                    GetSrcProduct(src.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {

            var bord = sender as Border;
            var WidthAnimation = new DoubleAnimation() { To = 160, Duration = TimeSpan.FromSeconds(0.3) };
            bord.BeginAnimation(StackPanel.WidthProperty, WidthAnimation);
            var HeightAnimation = new DoubleAnimation() { To = 160, Duration = TimeSpan.FromSeconds(0.3) };
            bord.BeginAnimation(StackPanel.HeightProperty, HeightAnimation);

        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            var bord = sender as Border;
            var WidthAnimation = new DoubleAnimation() { To = 150, Duration = TimeSpan.FromSeconds(0.3) };
            bord.BeginAnimation(StackPanel.WidthProperty, WidthAnimation);
            var HeightAnimation = new DoubleAnimation() { To = 150, Duration = TimeSpan.FromSeconds(0.3) };
            bord.BeginAnimation(StackPanel.HeightProperty, HeightAnimation);
        }

        private void ProdBord1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string ind1 = "";
                string ind2 = "";
                string address = ((Border)sender).Name;
                if (address.Length == 9)
                {
                    ind1 = Char.GetNumericValue(address[address.Length - 1]).ToString();
                }
                else if (address.Length == 10)
                {
                    ind2 = Char.GetNumericValue(address[address.Length - 1]).ToString();
                    ind1 = Char.GetNumericValue(address[address.Length - 2]).ToString();
                    ind1 = ind1 + ind2;
                }
                int ind = Convert.ToInt32(ind1);
                address = strings[(ind - 1) + (iR * 12)];
                ProductView taskWindow = new ProductView(address, names[(ind - 1) + (iR * 12)], prices[(ind - 1) + (iR * 12)], idishniki[(ind - 1) + (iR * 12)], descriptions[(ind - 1) + (iR * 12)], this);
                taskWindow.Owner = this;
                taskWindow.Show();
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                MessageBox.Show(ex.Message + "    " + line);
            }
        }

        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            labe.Content = null;
            profId.Content = null;
            letters.Visibility = Visibility.Hidden;
            nickname.Content = null;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (src.Text.Length < 1 || src.Text == "Search")
            {
                src.Text = "";
            }
        }

        private void src_LostFocus(object sender, RoutedEventArgs e)
        {
            if (src.Text.Length < 1)
            {
                src.Text = "Search";
            }
        }

        private void src_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (src.Text != "Search" && src.Text.Length > 0)
                {
                    iR = 0;
                    miR = 0;
                    NumbOfPage.Content = iR + 1;
                    GetSrcProduct(src.Text);
                }
                else
                {
                    GetProducts();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        public void GetSrcProduct(string src2)
        {
            arr = new List<int>();
            strings = new List<string>();
            names = new List<string>();
            prices = new List<string>();
            idishniki = new List<string>();
            descriptions = new List<string>();
            miR = 0;
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            connection = factory.CreateConnection();

            connectionString = GetConnectionString("System.Data.SqlClient");

            connection.ConnectionString = connectionString;
            try
            {
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = "SELECT ImageAddress, Name, Price, IdProfile, Description FROM Product";
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Product");

                ImgAdress.ItemsSource = null;
                ImgAdress.ItemsSource = ds.Tables["Product"].DefaultView;

                for (int i = 0; i < ImgAdress.Items.Count - 1; i++)
                {
                    DataRowView rowview = ImgAdress.Items[i] as DataRowView;
                    if (rowview.Row[1].ToString().Contains(src2))
                    {
                        strings.Add(rowview.Row[0].ToString());
                        names.Add(rowview.Row[1].ToString());
                        prices.Add(rowview.Row[2].ToString());
                        idishniki.Add(rowview.Row[3].ToString());
                        descriptions.Add(rowview.Row[4].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int count = strings.Count;

            while (count > 0)
            {
                if (count > 11)
                {
                    arr.Add(12);
                    count -= 12;
                    miR++;
                }
                else if (count <= 11)
                {
                    arr.Add(count);
                    count -= count;
                    miR++;
                }
            }
            List<Label> labels = new List<Label>();
            List<Label> labels2 = new List<Label>();
            List<Label> labels3 = new List<Label>();
            List<Label> labels4 = new List<Label>();
            List<Label> labels5 = new List<Label>();
            foreach (Label lbl in PLabes.Children)
            {
                labels.Add(lbl);
            }
            foreach (Label lbl in PLabes2.Children)
            {
                labels2.Add(lbl);
            }
            foreach (Label lbl in PLabes3.Children)
            {
                labels3.Add(lbl);
            }
            foreach (Label lbl in PLabes4.Children)
            {
                labels4.Add(lbl);
            }
            foreach (Label lbl in PLabes5.Children)
            {
                labels5.Add(lbl);
            }
            if (arr.Count > 0)
            {

                for (int i = 0; i < 12; i++)
                {
                    labels[i].Content = "";
                    labels2[i].Content = "";
                    labels3[i].Content = "";
                    labels4[i].Content = "Hidden";
                    labels5[i].Content = "";
                }
                for (int i = 0; i < arr[iR]; i++)
                {
                    labels[i].Content = strings[i + (iR * 12)];
                    labels2[i].Content = names[i + (iR * 12)];
                    labels3[i].Content = prices[i + (iR * 12)] + "$";
                    labels4[i].Content = "Vissible";
                    labels5[i].Content = idishniki[i + (iR * 12)];
                }
            }
            else
            {
                foreach (Label lbl in PLabes4.Children)
                {
                    lbl.Content = "Hidden";
                }
            }
        }

        private void СhatList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (letters.Visibility == Visibility.Visible)
            {
                Chat ch = new Chat(labe.Content.ToString(), 0);
                ch.Show();
            }
            else
            {
                MessageBox.Show("You need a account for chatting with another people");
            }
        }
    }
}

