using System;
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
        public Home_Page()
        {
            this.InitializeComponent();
            this.Update_Weather();
        }

        private async void Update_Weather()
        {
            try
            {
                var position = await LocationManager.GetPosition();
                RootObject myWeather = await OpenWeatherMapProxy.GetWeather(position.Coordinate.Latitude, position.Coordinate.Longitude);
                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                Weather_Image.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                Result_Text_Block.Text =
                    "Location: " + myWeather.name + "\n" +
                    "Temperature: " + ((int)myWeather.main.temp).ToString() + " °F\n" +
                    "Condition: " + myWeather.weather[0].description;
            }
            catch (Exception e)
            {
                
            }
            finally
            {
            }
        }
    }
}
