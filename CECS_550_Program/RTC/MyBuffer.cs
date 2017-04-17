using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Storage.Streams;

namespace CECS_550_Program.RTC
{
    class MyBuffer
    {
        private IBuffer buffer;

        public MyBuffer(IBuffer buf)
        {
            this.buffer = buf;
        }

        public MyBuffer(byte[] byteArr)
        {
            this.buffer = byteArr.AsBuffer();
        }

        public MyBuffer(string s)
        {
            byte[] byteArr = Encoding.UTF8.GetBytes(s.ToCharArray());
            this.buffer = byteArr.AsBuffer();
        }

        public byte[] AsByteArray()
        {
            return WindowsRuntimeBufferExtensions.ToArray(this.buffer);
        }

        public string AsString()
        {
            return Encoding.UTF8.GetString(this.AsByteArray());
        }

        public IBuffer Buffer
        {
            get { return buffer; }
        }
    }
}
