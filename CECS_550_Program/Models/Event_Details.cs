using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_550_Program.Models
{
    public class Event_Details
    {
        public int taskID
        {
            get;
            set;
        }

        public string organizerFirstName
        {
            get;
            set;
        }

        public string organizerLastName
        {
            get;
            set;
        }

        public string address
        {
            get;
            set;
        }

        public string eventName
        {
            get;
            set;
        }

        public string chatID
        {
            get;
            set;
        }

        public string topics
        {
            get;
            set;
        }

        public bool isCancelled
        {
            get;
            set;
        }

        public DateTime eventTime
        {
            get;
            set;
        }
    }
}
