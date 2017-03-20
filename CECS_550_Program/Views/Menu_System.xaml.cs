using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CECS_550_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu_System : Page
    {
        public Menu_System()
        {
            this.InitializeComponent();
            PageFrame.Navigate(typeof(Login_Page));
            PageFrame.Navigated += PageFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
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
            PageFrame.Navigate(typeof(User_Settings_Page));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(Event_Page));
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
