using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Entity
{
    [DataContract]
    public class OrderContrat
    {
        [DataMember]
        public string OrderID { get; set; }

        [DataMember]
        public string OrderDate { get; set; }

        [DataMember]
        public string ShippedDate { get; set; }

        [DataMember]
        public string ShipCountry { get; set; }

        [DataMember]
        public string OrderTotal { get; set; }
    }
}