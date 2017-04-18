using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_550_Program.RTC
{
    class ConnectionInformationObject
    {
        private string userID;
        private string userAlias = "default";
        private string chatID;
        private bool admin;

        public ConnectionInformationObject(string chatID, string userID = null, bool admin = false)
        {
            this.userID = userID;
            this.chatID = chatID;
            this.admin = admin;
        }

        public string UserID
        {
            get { return this.userID; }
            set { this.userID = value; }
        }

        public string UserAlias
        {
            get { return this.userAlias; }
            set { this.userAlias = value; }
        }

        public string ChatRoomID
        {
            get { return this.chatID; }
            set { this.chatID = value; }
        }

        public bool Administrator
        {
            get { return this.admin; }
            set { this.admin = value; }
        }
    }
}
