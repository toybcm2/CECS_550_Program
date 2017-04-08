using System.Text.RegularExpressions;
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
            else if (!Regex.IsMatch(EmailAddressTextBox.Text.Trim(), @"^([a-zA-Z_])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$"))
            {
                ErrorMessage.Text = "Email address is not allowed";
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
            }
        }
    }
}
