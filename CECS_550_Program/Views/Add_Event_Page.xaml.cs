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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Add_Event_Page : Page
    {
        private Models.User_Account account = new Models.User_Account();
        private string radioSelection;

        public Add_Event_Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Models.User_Account)Application.Current.Resources["User"];
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            string test = this.DateSelection.Date.Date.ToString();
            client.InsertTaskAsync(account.clientID, radioSelection, this.DateSelection.Date.Date, this.AddressTextBox.Text.Trim(), this.EventNameTextBox.Text.Trim());
            this.DisplaySuccessDialog();
        }

        private async void DisplaySuccessDialog()
        {
            ContentDialog successDialog = new ContentDialog
            {
                Title = "Success",
                Content = "You have successfully created an event.",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await successDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                this.Frame.Navigate(typeof(Home_Page));
            }
        }

        private void Choice_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            radioSelection = rb.Content.ToString();
        }
    }
}
