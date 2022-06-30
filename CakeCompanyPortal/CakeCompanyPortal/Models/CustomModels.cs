using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakeCompanyPortal.Models
{
    public partial class Order1
    {
        public string OrderNumber { get; set; }

        public string ProductName { get; set; }

        public string ClientName { get; set; }
    }

    public partial class Invoice1
    {

        public string ProductName { get; set; }

        public string ClientName { get; set; }

        public string TransportName { get; set; }

        
    }
}