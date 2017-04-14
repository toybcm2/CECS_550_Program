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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    public sealed partial class User_Settings_Page : Page
    {
        private Models.User_Account account;
        public User_Settings_Page()
        {
            this.InitializeComponent();
        }

        public void Login_User()
        {
            /*account = new Models.User_Account()
            {
                clientID = userString.ClientID,
                username = userString.UserName,
                avatarImage = userString.Avatar,
                firstName = userString.FirstName,
                lastName = userString.LastName,
                phoneNumber = userString.Phone,
                address = userString.Address
            };*/
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Models.User_Account)e.Parameter;
            string testString = account.username;
            string waitString = "test";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home_Page));
        }

        private void EditPhotoButtonTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Register_Page));
        }
    }
}
