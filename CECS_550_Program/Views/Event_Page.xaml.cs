using CECS_550_Program.Common;
using CECS_550_Program.RTC;
using System;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    public sealed partial class Event_Page : Page
    {
        private MediaPlayer _mediaPlayer;
        private Models.User_Account account = (Models.User_Account)Application.Current.Resources["User"];
        private Models.Event_Details eventDetails = (Models.Event_Details)Application.Current.Resources["Event"];
        private RTCHandler rtc = new RTCHandler(new RTCConnectionObject());
        private Database_Service.SchedServiceClient client;


        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            client = new Database_Service.SchedServiceClient();
            if (eventDetails.topics != null)
            {
                this.MeetingTopics.Text = eventDetails.topics;
            }
            EventViewModel model = new EventViewModel();
            
            this.DataContext = new EventViewModel();
        }

        public Event_Page()
        {
            this.InitializeComponent();
            System.Uri manifestUri = new Uri("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)");
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = MediaSource.CreateFromUri(manifestUri);
            account = Application.Current.Resources["User"] as Models.User_Account;
            //_mediaPlayer.Play();

            
            Task.Run(async () => {
                string connecting;
                try
                {
                    UpdateChat(connecting = await rtc.ConnectAsync(null, new ConnectionInformationObject(eventDetails.chatID, account.email, account.username, false)));
                }
                catch (Exception e)
                { }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Login_Page));
        }

        private void MessageArea_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter && MessageArea.Text != "")
            {
                rtc.SendAsync(MessageArea.Text);
                MessageArea.Text = "";
            }
        }

        private async void UpdateChat(string connecting)
        {
            try
            {
                EventViewModel data;

                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    data = DataContext as EventViewModel;
                    data.Chat += connecting + "\n";
                    DataContext = data;
                }).AsTask().Wait();
                //DataContext.Chat += connecting + "\n";

                while (true)
                {
                    string s = await rtc.ReadAsync();

                    Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        data = DataContext as EventViewModel;
                        data.Chat += s + "\n";
                        DataContext = data;
                    }).AsTask().Wait();
                }
            }
            catch (Exception e)
            { }
        }

        private void RequestAddToQueueButton_Click(object sender, RoutedEventArgs e)
        {
            this.DisplayUserQueueInfoDialog();
        }

        private void AdminDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            ConfirmJoinCheckBox.IsChecked = false;
        }

        private void ConfirmJoinCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AdminDialog.IsPrimaryButtonEnabled = true;
        }

        private void ConfirmJoinCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AdminDialog.IsPrimaryButtonEnabled = false;
        }

        private async void DisplayUserQueueInfoDialog()
        {
            ContentDialogResult meetingInfoDialog = await AdminDialog.ShowAsync();

            if (meetingInfoDialog == ContentDialogResult.Primary)
            {

            }
            else
            {

            }
        }
    }
}
