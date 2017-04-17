using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace CECS_550_Program.RTC
{
    class RTCConnectionObject
    {
        private EndpointPair ep;
        //private string server = "rtc.bcmart03.dynu.net";
        private string host = "10.0.0.7";
        //private string host = "74.136.104.121";
        private string port = "8888";
        public RTCConnectionObject()
        {
            //ResolveServerHost();
            ep = new EndpointPair(new HostName("localhost"), port, new HostName(host), port);
        }
        /// <summary>
        /// Host name of the server your connecting too
        /// </summary>
        public string HostName
        {
            get { return host; }
            set { host = value; }
        }
        /// <summary>
        /// Port for connecting to server
        /// </summary>
        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        public EndpointPair Endpoint
        {
            get { return ep; }
        }
    }
}
