using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using WCFExtras.Soap;


namespace BPMService.BLAN
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IBillPrintingWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<Bills> PrintBlueBill_NoCompress(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintBlueBill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintGreenBill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<Bills> PrintA4Bill_NoCompress(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintA4Bill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ReprintBlueBill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ReprintGreenBill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintGroupInvoiceA4Bill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ReprintGroupInvoiceA4Bill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintGreenBillByBank(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ReprintGreenBillByBank(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintBlueBillByBank(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ReprintBlueBillByBank(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<PrintableId> CheckExistRecord_NoCompress(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistRecord(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistReprintRecord(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistGroupInvoiceRecord(GroupInvoiceParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistReprintGroupInvoiceRecord(GroupInvoiceParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistByBank(GreenBillParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistReprintByBank(GreenBillReprintParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistGreenReceipt(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData CheckExistReprintGreenReceipt(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintGreenReceipt(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ReprintGreenReceipt(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData PrintSpotBill(BillPrintingConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData ReprintSpotBill(BillPrintingConditionParam param);

    }
}
