using ASMTEST.Helpers;
using ASMTEST.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class DetailContact : Page
    {
        Contacts currentContact = new Contacts();
        DatabaseHelperClass helper;
        public DetailContact()
        {
            this.InitializeComponent();
            helper = new DatabaseHelperClass();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentContact = e.Parameter as Contacts;

            NametxtBx.Text = currentContact.Name;
            PhonetxtBx.Text = currentContact.PhoneNumber;
        }

        private void UpdateContact_Click(object sender, RoutedEventArgs e)
        {
            //Get new value of Current Contact
            currentContact.Name = NametxtBx.Text;
            currentContact.PhoneNumber = PhonetxtBx.Text;

            // Update
            helper.Update(currentContact);

            Frame.Navigate(typeof(ContactList));
        }

        private async void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog showDialog = new MessageDialog("Bạn có muốn xóa?");
            showDialog.Commands.Add(new UICommand("Có")
            {
                Id = 0
            });
            showDialog.Commands.Add(new UICommand("KHông")
            {
                Id = 1
            });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                helper.Delete(currentContact.Id);
                Frame.Navigate(typeof(ContactList));
            }

        }
    }
}
