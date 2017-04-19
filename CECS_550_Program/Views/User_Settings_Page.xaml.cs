using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    public sealed partial class User_Settings_Page : Page
    {
        private Models.User_Account account = new Models.User_Account();
        public User_Settings_Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            account = (Models.User_Account)Application.Current.Resources["User"];
            //method needed to create/read/store an image as an avatar byte array on the client side
            //this.AvatarImageBox.Source = account.avatarImage;
            this.UsernameTextBox.Text = account.username;
            this.FirstNameTextBox.Text = account.firstName;
            this.LastNameTextBox.Text = account.lastName;
            this.PhoneNumberTextBox.Text = account.phoneNumber;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            client.UpdateUserAsync(account.clientID, this.PhoneNumberTextBox.Text.Trim(), "", this.UsernameTextBox.Text.Trim(), null);
            Application.Current.Resources.Remove("User");
            this.DisplaySuccessDialog();
        }

        private void EditPhotoButtonTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Event will launch window for user to select image from their computer
        }

        private async void DisplaySuccessDialog()
        {
            ContentDialog successDialog = new ContentDialog
            {
                Title = "Success",
                Content = "Your profile has been successfully changed.",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await successDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                this.Frame.Navigate(typeof(Login_Page));
            }
        }

        private void AvatarImageBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }
    }
}
