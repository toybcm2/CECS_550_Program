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
            if (radioSelection.Equals("Meeting"))
            {
                meetingId = GenerateKey();
                var message = await client.InsertTaskAsync(account.clientID, radioSelection, this.DateSelection.Date.Date, this.AddressTextBox.Text.Trim(), this.EventNameTextBox.Text.Trim(), meetingId, this.TopicsTextBox.Text.Trim());
                this.DisplaySuccessDialog();
            }
            else if (radioSelection.Equals("Appointment") || radioSelection.Equals("Other"))
            {
                var message = await client.InsertTaskAsync(account.clientID, radioSelection, this.DateSelection.Date.Date, this.AddressTextBox.Text.Trim(), this.EventNameTextBox.Text.Trim(), null, this.TopicsTextBox.Text.Trim());
                this.DisplaySuccessDialog();
            }
            else if (radioSelection.Equals("Existing Meeting"))
            {
                var eventString = await client.GetSpecificMeetingInfoAsync(Convert.ToInt32(this.MeetingIDTextBox.Text.Trim()));
                Models.Event_Details eventDetails = new Models.Event_Details()
                {
                    eventName = eventString.TaskName,
                    organizerFirstName = eventString.OrganizerFirstName,
                    organizerLastName = eventString.OrganizerLastName,
                    taskID = eventString.TaskID,
                    topics = eventString.Topic,
                    isCancelled = eventString.Cancelled,
                    eventTime = eventString.TaskTime,
                    chatID = eventString.ChatID,
                    address = eventString.TaskAddress
                };
                Application.Current.Resources.Add("Event", eventDetails);
                if (!eventDetails.isCancelled)
                {
                    var message = await client.InsertTaskAsync(account.clientID, "Meeting", eventDetails.eventTime, eventDetails.address, eventDetails.eventName, eventDetails.chatID, eventDetails.topics);
                    this.DisplaySuccessDialog();
                }
                else
                {
                    this.DisplayCancelledDialog();
                }                
            }
            else
            {
                this.DisplayErrorDialog();
            }
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

        private async void DisplayCancelledDialog()
        {
            ContentDialog cancelledDialog = new ContentDialog
            {
                Title = "Error",
                Content = "This event has been cancelled.",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await cancelledDialog.ShowAsync();
        }

        private async void DisplayErrorDialog()
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Error",
                Content = "Please make a selection for the meeting type.",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await errorDialog.ShowAsync();
        }

        private void Choice_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            radioSelection = rb.Content.ToString();
            if (radioSelection == "Existing Meeting")
            {
                this.Disable_TextBoxes();
            }
            else
            {
                this.Enable_TextBoxes();
            }
        }

        private void Disable_TextBoxes()
        {
            this.MeetingIDTextBox.IsEnabled = true;
            this.EventNameTextBox.IsEnabled = false;
            this.AddressTextBox.IsEnabled = false;
            this.DateSelection.IsEnabled = false;
            this.TimeSelection.IsEnabled = false;
            this.TopicsTextBox.IsEnabled = false;
        }

        private void Enable_TextBoxes()
        {
            this.MeetingIDTextBox.IsEnabled = false;
            this.EventNameTextBox.IsEnabled = true;
            this.AddressTextBox.IsEnabled = true;
            this.DateSelection.IsEnabled = true;
            this.TimeSelection.IsEnabled = true;
            this.TopicsTextBox.IsEnabled = true;
        }

        private string GenerateKey()
        {
            int Size = 20;
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = input[new Random(DateTime.UtcNow.Millisecond).Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
