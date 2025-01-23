using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPIMWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPPIMService
    {

        // TODO: Add your service operations here

        [OperationContract]
        List<Payment> SearchPayment(string CaId);

        [OperationContract]
        string UpdatePayment(string notificationNo, string invoiceNo, string reciptId, string deptId); 
    }

}
