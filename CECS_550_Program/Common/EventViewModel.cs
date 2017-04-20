using CECS_550_Program.RTC;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace CECS_550_Program.Common
{
    class EventViewModel : INotifyPropertyChanged
    {
        private string chat = String.Empty;
        private ObservableCollection<Database_Service.Tasks> events;
        private BitmapImage avatar;
        private ObservableCollection<Database_Service.Tasks> members;
        private ObservableCollection<QueueMember> queue = new ObservableCollection<QueueMember>();

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
            StorageFile file = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/weather/01d.png")).AsTask().GetAwaiter().GetResult();
            IRandomAccessStream stream = file.OpenAsync(FileAccessMode.Read).AsTask().GetAwaiter().GetResult();
            MyBuffer buffer = new MyBuffer(new byte[stream.Size]);
            stream.ReadAsync(buffer.Buffer, (uint)stream.Size, InputStreamOptions.None).AsTask().GetAwaiter().GetResult();

            newAvatar(buffer.AsByteArray());
        }

        public void newAvatar(byte[] newAvatar)
        {
            using (var stream = new InMemoryRandomAccessStream())
            {
                stream.WriteAsync(new MyBuffer(newAvatar).Buffer).AsTask().GetAwaiter().GetResult();
                var x = new BitmapImage();
                stream.Seek(0);
                x.SetSource(stream);
                this.Avatar = x;
            } 
        }

        public void AddQueueMember(QueueMember user)
        {
            queue.Add(user);
            NotifyPropertyChanged("Queue");
        }

        public void RemoveQueueMember()
        {
            queue.RemoveAt(0);
            NotifyPropertyChanged("Queue");
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
            set
            {
                events = value;
                NotifyPropertyChanged("EventList");
            }
        }

        public BitmapImage Avatar
        {
            get { return avatar; }
            set
            {
                avatar = value;
                NotifyPropertyChanged("Avatar");
            }
        }

        public ObservableCollection<Database_Service.Tasks> MeetingParticipants
        {
            get { return members; }
            set
            {
                members = value;
                NotifyPropertyChanged("MeetingParticipants");
            }
        }

        public ObservableCollection<QueueMember> Queue
        {
            get { return queue; }
            set
            {
                queue = value;
                NotifyPropertyChanged("Queue");
            }
        }
    }
}
