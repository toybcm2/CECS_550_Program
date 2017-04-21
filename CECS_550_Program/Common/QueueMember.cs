using CECS_550_Program.RTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace CECS_550_Program.Common
{
    class QueueMember
    {
        private BitmapImage avatar;
        private string username;

        public QueueMember(string name, byte[] image)
        {
            Username = name;
            if(image == null)
                Avatar = new EventViewModel().Avatar;
            else
                newAvatar(image);
        }

        private async void newAvatar(byte[] newAvatar)
        {
            using (var stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(new MyBuffer(newAvatar).Buffer);
                BitmapImage x = new BitmapImage();
                stream.Seek(0);
                await x.SetSourceAsync(stream);
                this.Avatar = x;
            }
        }

        public BitmapImage Avatar
        {
            get { return avatar; }
            set
            {
                avatar = value;
            }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
    }
}
