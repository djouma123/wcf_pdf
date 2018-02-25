using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService
{
    [DataContract ]
    public class personne
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string nom_personne { get; set; }

        [DataMember]
        public string prenom_persone { get; set; }
    }
}