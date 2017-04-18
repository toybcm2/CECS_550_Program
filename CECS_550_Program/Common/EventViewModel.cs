using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CECS_550_Program.Common
{
    class EventViewModel : INotifyPropertyChanged
    {
        private string chat = String.Empty;

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
            Chat = "testing";
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

    }
}
