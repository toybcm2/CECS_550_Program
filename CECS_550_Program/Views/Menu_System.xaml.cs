using System;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
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

        public void Login_User()
        {
            account = (Models.User_Account)Application.Current.Resources["User"];
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Resources.ContainsKey("User"))
            {
                PageFrame.Navigate(typeof(Home_Page));
            }
            else
            {
                PageFrame.Navigate(typeof(Login_Page));
            }
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Resources.ContainsKey("User"))
            {
                PageFrame.Navigate(typeof(User_Settings_Page));
            }
            else
            {
                PageFrame.Navigate(typeof(Login_Page));
            }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Resources.ContainsKey("User"))
            {
                PageFrame.Navigate(typeof(Event_Page));
            }
            else
            {
                PageFrame.Navigate(typeof(Login_Page));
            }
        }

        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Resources.ContainsKey("User"))
            {
                PageFrame.Navigate(typeof(Contacts_Page));
            }
            else
            {
                PageFrame.Navigate(typeof(Login_Page));
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Resources.ContainsKey("User"))
            {
                Application.Current.Resources.Remove("User");
                this.DisplaySuccessDialog();
            }
            else
            {
                PageFrame.Navigate(typeof(Login_Page));
            }
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
            if (PageFrame.CanGoBack && Application.Current.Resources.ContainsKey("User"))
            {
                e.Handled = true;
                PageFrame.GoBack();
            }
        }

        private async void DisplaySuccessDialog()
        {
            ContentDialog successDialog = new ContentDialog
            {
                Title = "Success",
                Content = "You have been successfully logged out.",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await successDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                PageFrame.Navigate(typeof(Login_Page));
            }
        }
    }
}
