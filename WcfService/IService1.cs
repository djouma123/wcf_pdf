using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.Entity;

namespace WcfService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        float GetPrice(string id);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebGet(UriTemplate="/GetOrderTotal/{OrderID}", 
            ResponseFormat=WebMessageFormat.Json)]
        string GetOrderTotal(string OrderID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetOrderDetails/{OrderID}",
            RequestFormat= WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        OrderContrat GetOrderDetails(string OrderID);

        [OperationContract]
        [WebInvoke(UriTemplate = "/PlaceOder",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        Method = "POST")]
        bool PlaceOrder(OrderContrat order);

      
        [OperationContract]
        [WebGet(UriTemplate = "/genere_pdf/{id}",ResponseFormat= WebMessageFormat.Xml)]
        Stream generate_pdf(string id);
        // TODO: ajoutez vos opérations de service ici

        [OperationContract]
        List<personne> getAll_personne();
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
