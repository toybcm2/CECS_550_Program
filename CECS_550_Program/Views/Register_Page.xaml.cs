using System.Text.RegularExpressions;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    public sealed partial class Register_Page : Page
    {
        public Register_Page()
        {
            this.InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text == "" || LastNameTextBox.Text == "" || EmailAddressTextBox.Text == "" || PasswordTextBox.Password == "" || ConfirmPasswordTextBox.Password == "")
            {
                ErrorMessage.Text = "Something has been left incomplete";
            }
            else if (!Regex.IsMatch(FirstNameTextBox.Text.Trim(), @"^[A-Z][a-zA-Z]*$"))
            {
                ErrorMessage.Text = "First name is not allowed";                
            }
            else if (!Regex.IsMatch(LastNameTextBox.Text.Trim(), @"^[A-Z][a-zA-Z]*$"))
            {
                ErrorMessage.Text = "Last name is not allowed";
            }
            else if (!Regex.IsMatch(UsernameTextBox.Text.Trim(), @"^([a-zA-Z_])([a-zA-Z0-9]*)"))
            {
                ErrorMessage.Text = "Username is not allowed (Must begin with letter and only letters/numbers are allowed)";
            }
            else if (!Regex.IsMatch(EmailAddressTextBox.Text.Trim(), @"^([a-zA-Z_])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$"))
            {
                ErrorMessage.Text = "Email address is not allowed";
            }
            else if (!Regex.IsMatch(PhoneNumberTextBox.Text.Trim(), @"^[0-9][0-9]*$"))
            {
                ErrorMessage.Text = "Phone number is not allowed (Must be numbers only)";
            }
            else if (PasswordTextBox.Password.Length < 6)
            {
                ErrorMessage.Text = "Password length should be a minimum of 6 characters";
            }
            else if ((string.Compare(PasswordTextBox.Password, ConfirmPasswordTextBox.Password) == -1))
            {
                ErrorMessage.Text = "Password does not match";
            }
            else
            {
                ErrorMessage.Text = "";
                //Run data and create account in database
                try
                {
                    Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
                    client.CreateUserAsync(this.FirstNameTextBox.Text.Trim(), this.LastNameTextBox.Text.Trim(), this.PhoneNumberTextBox.Text.Trim(), this.EmailAddressTextBox.Text.Trim(), "", this.UsernameTextBox.Text.Trim(), this.PasswordTextBox.Password.Trim());
                }
                catch (Exception)
                {

                }
                finally
                {
                    this.DisplaySuccessDialog();
                }
            }
        }

        private async void DisplaySuccessDialog()
        {
            ContentDialog successDialog = new ContentDialog
            {
                Title = "Success",
                Content = "Your username has been successfully created.",
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await successDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                this.Frame.Navigate(typeof(Home_Page));
            }
        }
    }
}
