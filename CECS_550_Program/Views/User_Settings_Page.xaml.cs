using CECS_550_Program.Common;
using CECS_550_Program.RTC;
using System;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
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
            EventViewModel em = new EventViewModel();
            if (account.avatarImage != null)
                em.newAvatar(account.avatarImage);
            this.DataContext = em;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
            client.UpdateUserAsync(account.clientID, this.PhoneNumberTextBox.Text.Trim(), "", this.UsernameTextBox.Text.Trim(), account.avatarImage);
            Application.Current.Resources.Remove("User");
            this.DisplaySuccessDialog();
        }

        //private void EditPhotoButtonTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    //Event will launch window for user to select image from their computer
        //}

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
            GetImage();
        }

        private async void GetImage()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage image = new BitmapImage();
                await image.SetSourceAsync(stream.CloneStream());
                image.UriSource = new Uri(file.Path);

                MyBuffer buffer = new MyBuffer(new byte[stream.Size]);
                await stream.ReadAsync(buffer.Buffer, (uint)stream.Size, InputStreamOptions.None);
                var newAvatar = buffer.AsByteArray();

                Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
                account.avatarImage = newAvatar;
                Application.Current.Resources["User"] = account;
                await client.UpdateUserAsync(account.clientID,account.phoneNumber,account.address,account.username, newAvatar);

                EventViewModel e = this.DataContext as EventViewModel;
                e.newAvatar(newAvatar);
                DataContext = e;
            }
        }
    }
}
