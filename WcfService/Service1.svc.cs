using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using WcfService.Entity;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Windows;
using Microsoft.Reporting.Common;
using System.Web;


namespace WcfService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        string extension = string.Empty;
        string enconding = string.Empty;
        string mimetype = string.Empty;
        string[] streams;
        Warning[] warrings;
        GestionData ges = new GestionData();
        int rowCount;
        public string rps;
        HttpResponse response;
        public float GetPrice(string id)
        {
            switch (id)
            {
                case "chaussures":
                    {
                        return 99.0f;
                    }
                case "pentalon":
                    {
                        return 80.0f;
                    }
                case "chemises":
                    {
                        return 91.0f;
                    }
                default:
                    {
                        return 100.15f;
                    }


            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string GetOrderTotal(string OrderID)
        {
            string OrderTotal = string.Empty;

            try
            {
                XDocument doc = XDocument.Load("D:\\Orders.xml");

                OrderTotal =
                (from result in doc.Descendants("DocumentElement")
                .Descendants("Orders")
                 where result.Element("OrderID").Value == OrderID.ToString()
                 select result.Element("OrderTotal").Value)
                .FirstOrDefault<string>();
            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                        ("GetOrderTotal: " + ex.Message);
            }
            return OrderTotal;
        }

        public OrderContrat GetOrderDetails(string OrderID)
        {
            OrderContrat order = new OrderContrat();

            try
            {
                XDocument doc = XDocument.Load("C:\\Orders.xml");

                IEnumerable<XElement> orders =
                    (from result in doc.Descendants("DocumentElement")
                        .Descendants("Orders")
                     where result.Element("OrderID").Value == OrderID.ToString()
                     select result);

                order.OrderID = orders.ElementAt(0)
                                            .Element("OrderID").Value;
                order.OrderDate = orders.ElementAt(0)
                                            .Element("OrderDate").Value;
                order.ShippedDate = orders.ElementAt(0)
                                            .Element("ShippedDate").Value;
                order.ShipCountry = orders.ElementAt(0)
                                            .Element("ShipCountry").Value;
                order.OrderTotal = orders.ElementAt(0)
                                            .Element("OrderTotal").Value;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                        ("GetOrderDetails: " + ex.Message);
            }
            return order;
        }

        public bool PlaceOrder(OrderContrat order)
        {
            try
            {
                XDocument doc = XDocument.Load("C:\\Orders.xml");

                doc.Element("DocumentElement").Add(
                        new XElement("Products",
                        new XElement("OrderID", order.OrderID),
                        new XElement("OrderDate", order.OrderDate),
                        new XElement("ShippedDate", order.ShippedDate),
                        new XElement("ShipCountry", order.ShipCountry),
                        new XElement("OrderTotal", order.OrderTotal)));

                doc.Save("C:\\Orders.xml");
            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                        ("PlaceOrder:" + ex.Message);
            }
            return true;
        }



        public Stream  generate_pdf(string id )
        {
            try
            {

               //ReportDataSource rds = new ReportDataSource(); 
                // LocalReport report = new LocalReport();
                //report.Dispose();


            //Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
          Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource();
                    
                DataSet n = new DataSet();
                n = ges.ReturnData("select * from rapport_bon where id_commande=" + Convert.ToInt16 (id)+" ", "rapport_bon");
                rowCount = n.Tables[0].Rows.Count;
                rds.Value = n.Tables[0];
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file

          
                // //ReportViewer1.LocalReport.ReportPath = PrjNamespace + "." + " Report1.rdlc";
                //report.ReportPath = "/Report1.rdlc";
                ////report.ReportPath = "Report1.rdlc"; 
                
         
             
              //  report.DataSources.Add(rds);


                //ReportViewer1.LocalReport.DataSources.Add(rds);


                //Byte[] mybytes = ReportViewer1.LocalReport.Render("PDF", null, out extension, out enconding, out mimetype, out streams, out warrings);

                Microsoft.Reporting.WinForms.ReportViewer viewer = new Microsoft.Reporting.WinForms.ReportViewer();

                viewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                 ////Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                 ////viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
               //  viewer.LocalReport.ReportPath = "C:\\Users\\440Z2\\Documents\\Visual Studio 2013\\Projects\\WebApplication4\\WebApplication4\\Report1.rdlc";

               // viewer.LocalReport.ReportPath = "C:\\Users\\440Z2\\Documents\\Visual Studio 2013\\Projects\\WcfService\\WcfService\\Report1.rdlc";
                viewer.LocalReport.ReportPath = "C:\\Users\\440Z2\\Documents\\Visual Studio 2013\\Projects\\web service pdf\\WcfService\\Report1.rdlc";
               
                  viewer.LocalReport.DataSources.Add(rds); // Add datasource here
               
                

                //Here is the part the program stop at 
                //Byte[] mybytes = report.Render("PDF", null, out extension, out enconding, out mimetype, out streams, out warrings);
               byte[] mybytes = viewer.LocalReport.Render("PDF");
               //    byte[] mybytes = viewer.ServerReport.Render("PDF");
             //  Byte[] mybytes = report.Render("PDF"); //for exporting to PDF
               FileStream fs;

               using ( fs = File.Create(@"C:\Users\440Z2\SalSlip.pdf"))
               {
                   fs.Write(mybytes, 0, mybytes.Length);
               }


               WebOperationContext.Current.OutgoingResponse.Headers.Add("Content_Type", "Application/pdf");
               WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
               WebOperationContext.Current.OutgoingResponse.ContentLength = mybytes.Length;

               return new MemoryStream(mybytes);
              

               //response = HttpContext.Current.Response;
               //response.AddHeader("Content-Type", "Application/pdf");
               //response.BinaryWrite(mybytes); // create the file
               //response.Flush();
                    // rps = "toute est bi1 passe";
              
                    //return rps;
       
            }

             catch (Exception ex)
            {

                return null ;
             

            }

        }


        public List<personne> getAll_personne()
        {
            return personne_data();

        }

        private List<personne> personne_data() 
        {
            DataSet ds = new DataSet ();
            List<personne> list_personne = new List<personne>();

          ds = ges.ReturnData("select * from personne ", "personne");

          foreach (DataRow r in ds.Tables[0].Rows) 
          {

              list_personne.Add(new personne { id = Convert.ToInt16(r["id"].ToString()), nom_personne = r["nom"].ToString(), prenom_persone = r["prenom"].ToString() });
          
          }

          return list_personne;
        }

    }
}