using CECS_550_Program.RTC;
using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace CECS_550_Program.RTC
{
    class RTCHandler
    {
        private Guid Id;
        private Guid roomId;
        private StreamSocket clientSocket = new StreamSocket();
        private EndpointPair ep;
        private bool endpointSet = false;
        private DataWriter writer;
        private DataReader reader;


        public RTCHandler(RTCConnectionObject rtcObject = null)
        {
            if(rtcObject != null)
            {
                SetEndpoint(rtcObject);
            }
        }

        public void SetEndpoint(RTCConnectionObject rtcObject)
        {
            ep = rtcObject.Endpoint;
            endpointSet = true;
        }

        public Task<string> ConnectAsync(EndpointPair Endpoint = null, ConnectionInformationObject connectionInfo = null)
        {
            return Task.Run(async () =>
            {
                if (Endpoint == null && this.ep != null)
                    Endpoint = this.ep;
                else
                    throw new ArgumentNullException("Endpoint", "EndpointPair param is null");

                await clientSocket.ConnectAsync(ep.RemoteHostName, ep.RemoteServiceName);
                writer = new DataWriter(clientSocket.OutputStream);
                reader = new DataReader(clientSocket.InputStream);

                await SendCommandAsync("connect:" + connectionInfo.UserID + ":" + connectionInfo.UserAlias + ":" + connectionInfo.ChatRoomID);
                string response = await ReadAsync();

                return response;
            });
        }

        public Task SendAsync(string message)
        {
            return Task.Run(async () =>
            {
                //MyBuffer buffer = new MyBuffer(message
                string count = Convert.ToString(message.Length);
                byte[] header = { 0, 0, 0, 0, 0 };

                switch (count.Length)
                {
                    case 1:
                        header[4] = byte.Parse(count[0].ToString());
                        break;
                    case 2:
                        header[4] = byte.Parse(count[1].ToString());
                        header[3] = byte.Parse(count[0].ToString());
                        break;
                    case 3:
                        header[4] = byte.Parse(count[2].ToString());
                        header[3] = byte.Parse(count[1].ToString());
                        header[2] = byte.Parse(count[0].ToString());
                        break;
                    case 4:
                        header[4] = byte.Parse(count[3].ToString());
                        header[3] = byte.Parse(count[2].ToString());
                        header[2] = byte.Parse(count[1].ToString());
                        header[1] = byte.Parse(count[0].ToString());
                        break;
                }

                writer.WriteBytes(header);
                await writer.StoreAsync();
                writer.WriteString(message);
                await writer.StoreAsync();
                //uint x = await writer.WriteAsync(buffer.Buffer);
            });
           
        }

        public Task SendCommandAsync(string message)
        {
            return Task.Run(async () =>
            {
                //MyBuffer buffer = new MyBuffer(message
                string count = Convert.ToString(message.Length);
                byte[] header = { 1, 0, 0, 0, 0};

                switch (count.Length)
                {
                    case 1:
                        header[4] = byte.Parse(count[0].ToString());
                        break;
                    case 2:
                        header[4] = byte.Parse(count[1].ToString());
                        header[3] = byte.Parse(count[0].ToString());
                        break;
                    case 3:
                        header[4] = byte.Parse(count[2].ToString());
                        header[3] = byte.Parse(count[1].ToString());
                        header[2] = byte.Parse(count[0].ToString());
                        break;
                    case 4:
                        header[4] = byte.Parse(count[3].ToString());
                        header[3] = byte.Parse(count[2].ToString());
                        header[2] = byte.Parse(count[1].ToString());
                        header[1] = byte.Parse(count[0].ToString());
                        break;
                }


                //string header = "1" + message.Length;
                // writer.WriteString(header);
                writer.WriteBytes(header);
                await writer.StoreAsync();
                writer.WriteString(message);
                await writer.StoreAsync();
                //uint x = await writer.WriteAsync(buffer.Buffer);
            });
        }

        public Task<string> ReadAsync()
        {
            return Task.Run(async () =>
            {
                byte[] header = new byte[4];
                await reader.LoadAsync(4);
                reader.ReadBytes(header);

                string slength = header[0].ToString() + header[1].ToString() + header[2].ToString() + header[3].ToString();
                uint length = Convert.ToUInt32(slength);

                await reader.LoadAsync(length);
                return reader.ReadString(length);
            });
        }
    }
}
