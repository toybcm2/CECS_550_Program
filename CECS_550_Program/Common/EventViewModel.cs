using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CECS_550_Program.Common
{
    class EventViewModel : INotifyPropertyChanged
    {
        private string chat = String.Empty;
        private ObservableCollection<Database_Service.Tasks> events;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public EventViewModel()
        {
            Chat = "";
        }

        public string Chat
        {
            get
            {
                return this.chat;
            }
            set
            {
                this.chat = value;
                NotifyPropertyChanged("Chat");
            }
        }

        public ObservableCollection<Database_Service.Tasks> EventList
        {
            get { return events; }
            set { events = value; }
        }

    }
}
