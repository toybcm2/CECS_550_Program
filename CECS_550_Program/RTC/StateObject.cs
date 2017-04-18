
using System.Net.Sockets;
using System.Text;

namespace Chat_Client
{
    class StateObject
    {
        // Server stream.
        //public NetworkStream serverStream = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
}
