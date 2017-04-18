using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using CECS_550_Program.Common;

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
            this.InitializeComponent();
            this.Update_Weather();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            //   client.GetMeetingInfoForUserAsync();
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
            /*ObservableCollection<String> items = new ObservableCollection<string>();
            for (int i = 0; i < 5; i++)
            {
                char[] chars = {'a','b','c'};
                items.Add(new String(chars));
            }
            DataContext = items;*/
            this.Frame.Navigate(typeof(Add_Event_Page));
        }

        private void Remove_Events_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Other_Events_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void View_Meeting_Changed(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
