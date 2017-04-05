using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CECS_550_Program
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Contacts_Page : Page
    {
        public Contacts_Page()
        {
            this.InitializeComponent();
        }

        private void Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Menu_System chatSystem = new Menu_System();
            chatSystem.Create_New_Contact_Chat_Page();
        }
    }
}
