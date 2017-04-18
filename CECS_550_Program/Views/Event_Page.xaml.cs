using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CECS_550_Program.Common;
using CECS_550_Program.RTC;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    public sealed partial class Event_Page : Page
    {
        private MediaPlayer _mediaPlayer;
        private Models.User_Account account = new Models.User_Account();
        RTCHandler rtc = new RTCHandler(new RTCConnectionObject());

        public Event_Page()
        {
            this.InitializeComponent();
            System.Uri manifestUri = new Uri("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)");
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = MediaSource.CreateFromUri(manifestUri);
            account = Application.Current.Resources["User"] as Models.User_Account;
            //_mediaPlayer.Play();

            this.DataContext = new EventViewModel();
            Task.Run(async () => {
                string connecting;
                try
                {
                    UpdateChat(connecting = await rtc.ConnectAsync(null, new ConnectionInformationObject("General", account.username, false)));
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
    }
}
