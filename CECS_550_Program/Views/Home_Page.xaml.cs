﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home_Page : Page
    {
        DispatcherTimer Timer = new DispatcherTimer();

        public Home_Page()
        {
            this.InitializeComponent();
            this.Update_Weather();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
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

        }

        private void Remove_Events_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Other_Events_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
