using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aloe.Models
{
    public class DealMRecieverM
    {
        public string  ID { get; set; }
        public string Receiver { get; set; }
    }
    public class DNotificationM
    {
        public int id { get; set; }
        public string reciever { get; set; }
        public string message { get; set; }
        public DateTime LogDate { get; set; }
    }

}