using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CECS_550_Program
{
    public sealed partial class Menu_System : Page
    {
        private Models.User_Account account = new Models.User_Account();

        public Menu_System()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(450, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            PageFrame.Navigate(typeof(Login_Page));
            PageFrame.Navigated += PageFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        public async void Create_New_Contact_Chat_Page()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(PrivateChatMenu), null);
                Window.Current.Content = frame;
                Window.Current.Activate();
                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }

        public void Login_User(Database_Service.Users userString)
        {
            account.clientID = userString.ClientID;
            account.username = userString.UserName;
            account.avatarImage = userString.Avatar;
            account.firstName = userString.FirstName;
            account.lastName = userString.LastName;
            account.phoneNumber = userString.Phone;
            account.address = userString.Address;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(Home_Page));
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(User_Settings_Page), account);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(Event_Page));
        }

        private void AboutButton2_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(Contacts_Page));
        }

        private void ChatWindowCallBack(IAsyncResult arg)
        {
            
        }

        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (PageFrame.CanGoBack)
            {
                e.Handled = true;
                PageFrame.GoBack();
            }
        }
    }
}
