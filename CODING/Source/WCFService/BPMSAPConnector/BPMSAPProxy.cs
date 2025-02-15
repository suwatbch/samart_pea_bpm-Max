
//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a SAP. NET Connector Proxy Generator Version 2.0
//     Created at 4/27/2011
//     Created from Windows
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using SAP.Connector;

namespace BPMSAPConnector
{

  /// <summary>
  /// Client SAP proxy class
  /// </summary>
  [WebServiceBinding(Name="dummy.Binding", Namespace="urn:sap-com:document:sap:rfc:functions")]
  [Serializable]
  public class BPMSAPProxy : SAPClient
  {
    /// <summary>
    /// Initializes a new BPMSAPProxy.
    /// </summary>
    public BPMSAPProxy(){}

    /// <summary>
    /// Initializes a new BPMSAPProxy with a new connection based on the specified connection string.
    /// </summary>
    /// <param name="connectionString">A connection string (e.g. RFC or URL) specifying the system where the proxy should connect to.</param>
    public BPMSAPProxy(string connectionString) : base(connectionString){}

    /// <summary>
    /// Initializes a new BPMSAPProxy instance and adds it to the given container.
    /// This allows automated connection mananged by VS component designer:
    /// If container is disposed, it will also dispose this SAPClient instance,
    /// which will dispose a contained connection if needed.
    /// </summary>
    /// <param name="container"<The container where the new SAPClient instance is to be added.>/param<
    public BPMSAPProxy(System.ComponentModel.IContainer container) : base(container) {}    
  
    /// <summary>
    /// Remote Function Module ZPOS_ZCACI029.  
    /// RFC interface to POS
    /// </summary>
    /// <param name="Action">Action</param>
    /// <param name="Ca_Doc">CA Document</param>
    /// <param name="Ca_Number">Contract Account Number</param>
    /// <param name="Pos_Id">POS Identification</param>
    /// <param name="Reference">Reference</param>
    /// <param name="Search_Period">Search Period</param>
    /// <param name="Mtr0060">BP Master : interface with POS</param>
    /// <param name="Mtr0090">Contract Account : interface POS</param>
    /// <param name="Trr0010">Open Items : interface POS</param>
    /// <param name="Trr0020">Payment Items : interface with POS</param>
    /// <param name="Trr0040">Open Items : interface POS</param>
    /// <param name="Trr0045">Open Items : interface POS</param>
    [RfcMethod(AbapName = "ZPOS_ZCACI029")]
    [SoapDocumentMethodAttribute("http://tempuri.org/ZPOS_ZCACI029",
     RequestNamespace = "urn:sap-com:document:sap:rfc:functions",
     RequestElementName = "ZPOS_ZCACI029",
     ResponseNamespace = "urn:sap-com:document:sap:rfc:functions",
     ResponseElementName = "ZPOS_ZCACI029.Response")]
    public virtual void Zpos_Zcaci029 (

     [RfcParameter(AbapName = "ACTION",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = true, Direction = RFCINOUT.IN, Length = 1, Length2 = 2)]
     [XmlElement("ACTION", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string Action,
     [RfcParameter(AbapName = "CA_DOC",RfcType=RFCTYPE.RFCTYPE_XMLDATA, Optional = true, Direction = RFCINOUT.IN)]
     [XmlArray("CA_DOC", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ZCADOC Ca_Doc,
     [RfcParameter(AbapName = "CA_NUMBER",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = true, Direction = RFCINOUT.IN, Length = 12, Length2 = 24)]
     [XmlElement("CA_NUMBER", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string Ca_Number,
     [RfcParameter(AbapName = "POS_ID",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = false, Direction = RFCINOUT.IN, Length = 40, Length2 = 80)]
     [XmlElement("POS_ID", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string Pos_Id,
     [RfcParameter(AbapName = "REFERENCE",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = true, Direction = RFCINOUT.IN, Length = 16, Length2 = 32)]
     [XmlElement("REFERENCE", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string Reference,
     [RfcParameter(AbapName = "SEARCH_PERIOD",RfcType=RFCTYPE.RFCTYPE_DATE, Optional = true, Direction = RFCINOUT.IN, Length = 8, Length2 = 16)]
     [XmlElement("SEARCH_PERIOD", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string Search_Period,
     [RfcParameter(AbapName = "MTR0060",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("MTR0060", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZCA_MTR0060Table Mtr0060,
     [RfcParameter(AbapName = "MTR0090",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("MTR0090", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZCA_MTR0090Table Mtr0090,
     [RfcParameter(AbapName = "TRR0010",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("TRR0010", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZCA_TRR0010Table Trr0010,
     [RfcParameter(AbapName = "TRR0020",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("TRR0020", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZCA_TRR0020Table Trr0020,
     [RfcParameter(AbapName = "TRR0040",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("TRR0040", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZCA_TRR0040Table Trr0040,
     [RfcParameter(AbapName = "TRR0045",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("TRR0045", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZCA_TRR0045Table Trr0045)
    {
        object[]results = null;
        results = this.SAPInvoke("Zpos_Zcaci029",new object[] {
                            Action,Ca_Doc,Ca_Number,Pos_Id,Reference,Search_Period,Mtr0060,Mtr0090,Trr0010,Trr0020,Trr0040,Trr0045 });
        Mtr0060 = (ZCA_MTR0060Table) results[0];
        Mtr0090 = (ZCA_MTR0090Table) results[1];
        Trr0010 = (ZCA_TRR0010Table) results[2];
        Trr0020 = (ZCA_TRR0020Table) results[3];
        Trr0040 = (ZCA_TRR0040Table) results[4];
        Trr0045 = (ZCA_TRR0045Table) results[5];

    }

    /// <summary>
    /// Remote Function Module ZPOS_SUBMIT.  
    /// Submit POS Interface Programs
    /// </summary>
    /// <param name="In_Event">Inbound Event</param>
    [RfcMethod(AbapName = "ZPOS_SUBMIT")]
    [SoapDocumentMethodAttribute("http://tempuri.org/ZPOS_SUBMIT",
     RequestNamespace = "urn:sap-com:document:sap:rfc:functions",
     RequestElementName = "ZPOS_SUBMIT",
     ResponseNamespace = "urn:sap-com:document:sap:rfc:functions",
     ResponseElementName = "ZPOS_SUBMIT.Response")]
    public virtual void Zpos_Submit (

     [RfcParameter(AbapName = "IN_EVENT",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = true, Direction = RFCINOUT.IN, Length = 15, Length2 = 30)]
     [XmlElement("IN_EVENT", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string In_Event)
    {
        object[]results = null;
        results = this.SAPInvoke("Zpos_Submit",new object[] {
                            In_Event });

    }

    /// <summary>
    /// Remote Function Module ZPOS_SUBMIT_POST.  
    /// Submit and Post ZCABI001
    /// </summary>
    /// <param name="Ex_Msg">Char255</param>
    /// <param name="Im_File">Local file for upload/download</param>
    /// <param name="Im_Pos_Tab">Interface Data File</param>
    [RfcMethod(AbapName = "ZPOS_SUBMIT_POST")]
    [SoapDocumentMethodAttribute("http://tempuri.org/ZPOS_SUBMIT_POST",
     RequestNamespace = "urn:sap-com:document:sap:rfc:functions",
     RequestElementName = "ZPOS_SUBMIT_POST",
     ResponseNamespace = "urn:sap-com:document:sap:rfc:functions",
     ResponseElementName = "ZPOS_SUBMIT_POST.Response")]
    public virtual void Zpos_Submit_Post (

     [RfcParameter(AbapName = "IM_FILE",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = false, Direction = RFCINOUT.IN, Length = 128, Length2 = 256)]
     [XmlElement("IM_FILE", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string Im_File,
     [RfcParameter(AbapName = "IM_POS_TAB",RfcType=RFCTYPE.RFCTYPE_XMLDATA, Optional = false, Direction = RFCINOUT.IN)]
     [XmlArray("IM_POS_TAB", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ZTABTEXT Im_Pos_Tab,
     [RfcParameter(AbapName = "EX_MSG",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = true, Direction = RFCINOUT.OUT, Length = 255, Length2 = 510)]
     [XmlElement("EX_MSG", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     out string Ex_Msg)
    {
        object[]results = null;
        results = this.SAPInvoke("Zpos_Submit_Post",new object[] {
                            Im_File,Im_Pos_Tab });
        Ex_Msg = (string) results[0];

    }

  } 

}
