using CECS_550_Program.Common;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// Home_Page.xaml: List of events

namespace CECS_550_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home_Page : Page
    {
        DispatcherTimer Timer = new DispatcherTimer();

        public String Item
        {
            get { return _item; }
            set { _item = value;
            }
        }
        private String _item;

        ObservableCollection<String> items = new ObservableCollection<String>();

        public Home_Page()
        {
            // TODO: Confirm that "Task" works or if we need to reference the event by something else
       //     Models.Event_Details task = Application.Current.Resources["Task"] as Models.Event_Details;

            this.InitializeComponent();
            this.Update_Weather();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            EventViewModel model = new EventViewModel();
            Models.User_Account account = Application.Current.Resources["User"] as Models.User_Account;
            model.EventList = client.GetMeetingInfoForUserAsync(account.clientID).GetAwaiter().GetResult();
            this.DataContext = model;
        }

        private async void Update_Weather()
        {
            var position = await LocationManager.GetPosition();
            RootObject myWeather = await OpenWeatherMapProxy.GetWeather(position.Coordinate.Latitude, position.Coordinate.Longitude);
            string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
            Weather_Image.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            Result_Text_Block.Text =
                ((int)myWeather.main.temp).ToString() + " °F" +
                " " + myWeather.weather[0].description.ToUpper();
        }

        private void Timer_Tick(object sender, object e)
        {
            Time.Text = DateTime.Now.ToString("h:mm:ss tt");
        }

        private void Add_Events_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Add_Event_Page));
        }

        private void Remove_Events_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Delete_Event_Page));

          //  var x = DataContext as EventViewModel;
            // access specific event by taskID

        }

        private void Other_Events_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void TodaysEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Database_Service.Tasks x = (Database_Service.Tasks) TodaysEvents.SelectedItem;
            Application.Current.Resources.Remove("Event");
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            
            var eventString = await client.GetSpecificMeetingInfoAsync(x.TaskID);
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
            this.EventNameBlock.Text = eventDetails.eventName;
            this.AddressBlock.Text = eventDetails.address;
            this.EventTimeBlock.Text = Convert.ToString(eventDetails.eventTime);
            if (eventDetails.chatID != null)
            {
                this.EventMeetingIDBlock.Text = "Your meeting ID string is: " + eventDetails.chatID;
            }
            this.DisplayMeetingInfoDialog();
        }

        private async void DisplayMeetingInfoDialog()
        {
            ContentDialogResult meetingInfoDialog = await EventContentDialog.ShowAsync();

            if (meetingInfoDialog == ContentDialogResult.Primary)
            {
                this.Frame.Navigate(typeof(Event_Page));
            }
            else
            {

            }
        }

        private void EventContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            ConfirmJoinCheckBox.IsChecked = false;
        }

        private void ConfirmJoinCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EventContentDialog.IsPrimaryButtonEnabled = true;
        }

        private void ConfirmJoinCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            EventContentDialog.IsPrimaryButtonEnabled = false;
        }
    }
}
