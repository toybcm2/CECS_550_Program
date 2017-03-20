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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Event_Page : Page
    {
        private MediaPlayer _mediaPlayer;

        public Event_Page()
        {
            this.InitializeComponent();
            System.Uri manifestUri = new Uri("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)");
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Source = MediaSource.CreateFromUri(manifestUri);
            _mediaPlayer.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Login_Page));
        }
    }
}
