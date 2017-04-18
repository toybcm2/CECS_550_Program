using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_550_Program.Models
{
    public class User_Account
    {
        public int clientID
        {
            get;
            set;
        }

        public string username
        {
            get;
            set;
        }

        public byte[] avatarImage
        {
            get;
            set;
        }

        public string firstName
        {
            get;
            set;
        }

        public string lastName
        {
            get;
            set;
        }

        public string phoneNumber
        {
            get;
            set;
        }

        public string address
        {
            get;
            set;
        }
    }
}
