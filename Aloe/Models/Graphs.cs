using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aloe.Models
{
    public class ProductivityGraph
    {
        public int CompletedDeals { get; set; }
        public int Productivity { get; set; }
        public int CancelledDeals { get; set; }
        public int DealsWaiting { get; set; }
        public int NewDeals { get; set; }
    }


    public class CircleChart
    {
        public int Clients { get; set; }
        public int Completed { get; set; }
    }


}