
//------------------------------------------------------------------------------
// 
//     This code was generated by a SAP. NET Connector Proxy Generator Version 2.0
//     Created at 4/27/2011
//     Created from Windows
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// 
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
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
  /// Open Items : interface POS
  /// </summary>
  [RfcStructure(AbapName ="ZCA_TRR0045" , Length = 83, Length2 = 166)]
  [Serializable]
  public class ZCA_TRR0045 : SAPStructure
  {
   

    /// <summary>
    /// Client
    /// </summary>
 
    [RfcField(AbapName = "MANDT", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 3, Length2 = 6, Offset = 0, Offset2 = 0)]
    [XmlElement("MANDT", Form=XmlSchemaForm.Unqualified)]
    public string Mandt
    { 
       get
       {
          return _Mandt;
       }
       set
       {
          _Mandt = value;
       }
    }
    private string _Mandt;


    /// <summary>
    /// POS Identification
    /// </summary>
 
    [RfcField(AbapName = "POS_ID", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 40, Length2 = 80, Offset = 3, Offset2 = 6)]
    [XmlElement("POS_ID", Form=XmlSchemaForm.Unqualified)]
    public string Pos_Id
    { 
       get
       {
          return _Pos_Id;
       }
       set
       {
          _Pos_Id = value;
       }
    }
    private string _Pos_Id;


    /// <summary>
    /// Template ID
    /// </summary>
 
    [RfcField(AbapName = "ID", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 10, Length2 = 20, Offset = 43, Offset2 = 86)]
    [XmlElement("ID", Form=XmlSchemaForm.Unqualified)]
    public string Id
    { 
       get
       {
          return _Id;
       }
       set
       {
          _Id = value;
       }
    }
    private string _Id;


    /// <summary>
    /// Disconnection No.
    /// </summary>
 
    [RfcField(AbapName = "DISCNO", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 12, Length2 = 24, Offset = 53, Offset2 = 106)]
    [XmlElement("DISCNO", Form=XmlSchemaForm.Unqualified)]
    public string Discno
    { 
       get
       {
          return _Discno;
       }
       set
       {
          _Discno = value;
       }
    }
    private string _Discno;


    /// <summary>
    /// CA Document No.
    /// </summary>
 
    [RfcField(AbapName = "CA_DOC", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 16, Length2 = 32, Offset = 65, Offset2 = 130)]
    [XmlElement("CA_DOC", Form=XmlSchemaForm.Unqualified)]
    public string Ca_Doc
    { 
       get
       {
          return _Ca_Doc;
       }
       set
       {
          _Ca_Doc = value;
       }
    }
    private string _Ca_Doc;


    /// <summary>
    /// Cancel
    /// </summary>
 
    [RfcField(AbapName = "CANCEL", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 81, Offset2 = 162)]
    [XmlElement("CANCEL", Form=XmlSchemaForm.Unqualified)]
    public string Cancel
    { 
       get
       {
          return _Cancel;
       }
       set
       {
          _Cancel = value;
       }
    }
    private string _Cancel;


    /// <summary>
    /// Fix = '0'
    /// </summary>
 
    [RfcField(AbapName = "ACTION", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 82, Offset2 = 164)]
    [XmlElement("ACTION", Form=XmlSchemaForm.Unqualified)]
    public string Action
    { 
       get
       {
          return _Action;
       }
       set
       {
          _Action = value;
       }
    }
    private string _Action;

  }

}
