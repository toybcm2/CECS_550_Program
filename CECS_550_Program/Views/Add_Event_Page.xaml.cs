using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
        private string meetingId = null;

        public Add_Event_Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Models.User_Account)Application.Current.Resources["User"];
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            string test = this.DateSelection.Date.Date.ToString();

            if (String.IsNullOrEmpty(radioSelection))
            {
                ContentDialog successDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "You must select what type of event this is.",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await successDialog.ShowAsync();

                return;
            }

            if (radioSelection.Equals("Meeting"))
                meetingId = GenerateKey();
            client.InsertTaskAsync(account.clientID, radioSelection, this.DateSelection.Date.Date, this.AddressTextBox.Text.Trim(), this.EventNameTextBox.Text.Trim(), meetingId, this.TopicsTextBox.Text.Trim());
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

        private string GenerateKey()
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 20; i++)
            {
                ch = input[new Random(DateTime.UtcNow.Millisecond).Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
