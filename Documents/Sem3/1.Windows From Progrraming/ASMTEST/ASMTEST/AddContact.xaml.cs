using ASMTEST.Helpers;
using ASMTEST.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASMTEST
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddContact : Page
    {
        DatabaseHelperClass helper;
        public AddContact()
        {
            this.InitializeComponent();
            helper = new DatabaseHelperClass();
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            string name = NametxtBx.Text;
            string phone = PhonetxtBx.Text;

            var newContact = new Contacts() { Name = name, PhoneNumber = phone, CreationDate = DateTime.Now.ToString("yyyy-MM-dd") };
            helper.Insert(newContact);

            Frame.Navigate(typeof(ContactList));
        }
    }
}
