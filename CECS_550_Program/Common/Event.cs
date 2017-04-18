using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CECS_550_Program.Common
{
    class Event : INotifyPropertyChanged
    {
        private string name = String.Empty;
        private Collection<object> events;

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

        public Event() {
            Name = "asdf";
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value;
                NotifyPropertyChanged("Name"); 
            }
        }
        public Collection<object> EventList
        {
            get { return events; }
            set { events = value; }
        }
        public string Day
        {
            get;
            set;
        }
        public string Location
        {
            get;
            set;
        }
        public DateTime DateTime
        {
            get;
            set;
        }
    }
}
