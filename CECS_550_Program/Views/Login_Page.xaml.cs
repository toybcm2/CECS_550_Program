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
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login_Page : Page
    {
        const string ErrorMessageText = "Incorrect Username or Password, please try again";

        public Login_Page()
        {
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //this.ErrorMessage.Text = Login_Page.ErrorMessageText;
            try
            {
                Database_Service.SchedServiceClient client = new Database_Service.SchedServiceClient();
                string testString = await client.LoginCheckAsync(this.EmailTextBox.Text.Trim(), this.PasswordTextBox.Password.Trim());
                if (testString == "Login Successful")
                {
                    var testerString = await client.GetUserAsync(this.EmailTextBox.Text.Trim());
                    Models.User_Account account = new Models.User_Account()
                    {
                        clientID = testerString.ClientID,
                        avatarImage = testerString.Avatar,
                        firstName = testerString.FirstName,
                        lastName = testerString.LastName,
                        phoneNumber = testerString.Phone,
                        address = testerString.Address
                    };
                    //Application.Current.Resources.Add(string userName, "");
                    Menu_System menuSystem = new Menu_System();
                    menuSystem.Login_User(testerString);
                }
                else
                {

                }
            }
            catch (Exception excep)
            {
                string testString;
                testString = excep.ToString();
                string waitString;                
            }
            finally
            {
                //this.Frame.Navigate(typeof(Login_Page));
            }
            
        }

        private void RegisterButtonTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Register_Page));
        }
    }
}
