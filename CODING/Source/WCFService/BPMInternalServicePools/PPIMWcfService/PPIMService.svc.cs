using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPIMWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PPIMService : IPPIMService
    {
        public List<Payment> SearchPayment(string CaId)
        {
            List<Payment> result = new List<Payment>();
            string VSPPGateWayUrl;
            int VSPPTimeOut = 5000;

            // Initial object. 
            Payment resultitem = new Payment()
            {
                CaId = string.Empty,
                CaName = string.Empty,
                CaAddress = " ",
                CaTaxBranch = " ",
                CaTaxId = " ",
                AmountExVat = 0,
                DebtId = string.Empty,
                DebtType = string.Empty,
                GAmount = 0,
                InvoiceNo = string.Empty,
                NotificationNo = string.Empty,
                Qty = 0,
                TaxCode = string.Empty,
                TaxRate = 0,
                TechBranchId = string.Empty,
                VatAmount = 0
            };

            // Assign URL 
            //VSPPGateWayUrl = "https://vsppdb-dev.pea.co.th:4433/web-services/bpm/services.wsdl";
            VSPPGateWayUrl = Properties.Settings.Default.PEA_PPIMServiceURL;

            // Search payment by CaId.
            try
            {
                var vsppService = new VSPPDev.AppHttpWebServicesBpmWebServiceService();
                vsppService.Url = VSPPGateWayUrl;
                vsppService.Timeout = VSPPTimeOut;

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;

                var resultData = vsppService.SearchPayment(CaId); //VS6200000127

                if (resultData.Count() > 0)
                {
                    // Get first object 
                    var ppimData = resultData.FirstOrDefault();

                    // Assign data for return result.
                    resultitem.CaId = ppimData.CaId;
                    resultitem.CaName = ppimData.CaName;
                    resultitem.CaAddress = ppimData.CaAddress;
                    resultitem.CaTaxBranch = ppimData.CaTaxBranch;
                    resultitem.CaTaxId = ppimData.CaTaxId;
                    resultitem.AmountExVat = ppimData.AmountExVat;
                    resultitem.DebtId = ppimData.DebtId;
                    resultitem.DebtType = ppimData.DebtType;
                    resultitem.GAmount = ppimData.GAmount;
                    resultitem.InvoiceNo = ppimData.InvoiceNo;
                    resultitem.NotificationNo = ppimData.NotificationNo;
                    resultitem.Qty = ppimData.Qty;
                    resultitem.TaxCode = ppimData.TaxCode;
                    resultitem.TaxRate = ppimData.TaxRate;
                    resultitem.TechBranchId = ppimData.TechBranchId;
                    resultitem.VatAmount = ppimData.VatAmount;

                    result.Add(resultitem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" VSPP Error log  " + ex.Message);
                // Keep log.  


            }

            return result;
        }

        public string UpdatePayment(string notificationNo, string invoiceNo, string reciptId, string deptId)
        {
            string VSPPGateWayUrl;
            int VSPPTimeOut = 5000;
            string result = string.Empty;

            // Create instance of web service 
            var vsppService = new VSPPDev.AppHttpWebServicesBpmWebServiceService();

            // Prepare Url and time out. 
            VSPPGateWayUrl = Properties.Settings.Default.PEA_PPIMServiceURL;
            vsppService.Url = VSPPGateWayUrl;
            vsppService.Timeout = VSPPTimeOut;

            try
            {
                // Send data to update status. 
                result = vsppService.UpdatePayment(notificationNo, invoiceNo, reciptId, deptId);

                // Keep log. 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 

                // Keep log. 

            }

            return result;
        }
    }
}
