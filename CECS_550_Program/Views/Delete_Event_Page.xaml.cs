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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Delete_Event_Page : Page
    {
        private Models.User_Account account = new Models.User_Account();

        public Delete_Event_Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Models.User_Account)Application.Current.Resources["User"];
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            client.CancelMeetingAsync(1);
        }
        public void InitializeComponent()
        {

        }
    }
}
