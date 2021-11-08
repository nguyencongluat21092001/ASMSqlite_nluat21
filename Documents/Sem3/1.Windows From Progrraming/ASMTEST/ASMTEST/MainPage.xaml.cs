using SQLite.Net;
using ASMTEST.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ASMTEST
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SQLite.Net.SQLiteConnection conn;
        string path;

        public MainPage()
        {
            this.InitializeComponent();

            //Connect to the SQlite DB
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "connect_asm.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
        }

        private void BTNCreateTable_Click(object sender, RoutedEventArgs e)
        {
            conn.CreateTable<Customer>();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string age = txtAge.Text;

            conn.Insert(new Customer() { Name = name, Age = age });
        }

        private void Retrieve_Click(object sender, RoutedEventArgs e)
        {
            var query = conn.Table<Customer>();
            string id = "";
            string name = "";
            string age = "";

            foreach (Customer item in query)
            {
                id += item.Id + ",";
                name += item.Name + ",";
                age += item.Age + ",";
            }

            txtResult.Text = id + "\n" + name + "\n" + age;

        }
    }
}
