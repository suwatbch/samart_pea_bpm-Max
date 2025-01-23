
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
  /// Contract Account : interface POS
  /// </summary>
  [RfcStructure(AbapName ="ZCA_MTR0090" , Length = 1019, Length2 = 2038)]
  [Serializable]
  public class ZCA_MTR0090 : SAPStructure
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
    /// Contract Account
    /// </summary>
 
    [RfcField(AbapName = "CA_NO", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 12, Length2 = 24, Offset = 53, Offset2 = 106)]
    [XmlElement("CA_NO", Form=XmlSchemaForm.Unqualified)]
    public string Ca_No
    { 
       get
       {
          return _Ca_No;
       }
       set
       {
          _Ca_No = value;
       }
    }
    private string _Ca_No;


    /// <summary>
    /// Business Partner Number
    /// </summary>
 
    [RfcField(AbapName = "BP_NO", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 10, Length2 = 20, Offset = 65, Offset2 = 130)]
    [XmlElement("BP_NO", Form=XmlSchemaForm.Unqualified)]
    public string Bp_No
    { 
       get
       {
          return _Bp_No;
       }
       set
       {
          _Bp_No = value;
       }
    }
    private string _Bp_No;


    /// <summary>
    /// TRSG
    /// </summary>
 
    [RfcField(AbapName = "TRSG", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 75, Offset2 = 150)]
    [XmlElement("TRSG", Form=XmlSchemaForm.Unqualified)]
    public string Trsg
    { 
       get
       {
          return _Trsg;
       }
       set
       {
          _Trsg = value;
       }
    }
    private string _Trsg;


    /// <summary>
    /// CRSG
    /// </summary>
 
    [RfcField(AbapName = "CRSG", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 83, Offset2 = 166)]
    [XmlElement("CRSG", Form=XmlSchemaForm.Unqualified)]
    public string Crsg
    { 
       get
       {
          return _Crsg;
       }
       set
       {
          _Crsg = value;
       }
    }
    private string _Crsg;


    /// <summary>
    /// MRU
    /// </summary>
 
    [RfcField(AbapName = "MRU", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 91, Offset2 = 182)]
    [XmlElement("MRU", Form=XmlSchemaForm.Unqualified)]
    public string Mru
    { 
       get
       {
          return _Mru;
       }
       set
       {
          _Mru = value;
       }
    }
    private string _Mru;


    /// <summary>
    /// BP Name
    /// </summary>
 
    [RfcField(AbapName = "BP_NAME", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 160, Length2 = 320, Offset = 99, Offset2 = 198)]
    [XmlElement("BP_NAME", Form=XmlSchemaForm.Unqualified)]
    public string Bp_Name
    { 
       get
       {
          return _Bp_Name;
       }
       set
       {
          _Bp_Name = value;
       }
    }
    private string _Bp_Name;


    /// <summary>
    /// Address1
    /// </summary>
 
    [RfcField(AbapName = "BP_ADDRESS1", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 255, Length2 = 510, Offset = 259, Offset2 = 518)]
    [XmlElement("BP_ADDRESS1", Form=XmlSchemaForm.Unqualified)]
    public string Bp_Address1
    { 
       get
       {
          return _Bp_Address1;
       }
       set
       {
          _Bp_Address1 = value;
       }
    }
    private string _Bp_Address1;


    /// <summary>
    /// Address2
    /// </summary>
 
    [RfcField(AbapName = "BP_ADDRESS2", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 75, Length2 = 150, Offset = 514, Offset2 = 1028)]
    [XmlElement("BP_ADDRESS2", Form=XmlSchemaForm.Unqualified)]
    public string Bp_Address2
    { 
       get
       {
          return _Bp_Address2;
       }
       set
       {
          _Bp_Address2 = value;
       }
    }
    private string _Bp_Address2;


    /// <summary>
    /// Contract Account Category
    /// </summary>
 
    [RfcField(AbapName = "CA_CAT", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 2, Length2 = 4, Offset = 589, Offset2 = 1178)]
    [XmlElement("CA_CAT", Form=XmlSchemaForm.Unqualified)]
    public string Ca_Cat
    { 
       get
       {
          return _Ca_Cat;
       }
       set
       {
          _Ca_Cat = value;
       }
    }
    private string _Ca_Cat;


    /// <summary>
    /// Incoming Payment Method
    /// </summary>
 
    [RfcField(AbapName = "PAY_METHOD", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 591, Offset2 = 1182)]
    [XmlElement("PAY_METHOD", Form=XmlSchemaForm.Unqualified)]
    public string Pay_Method
    { 
       get
       {
          return _Pay_Method;
       }
       set
       {
          _Pay_Method = value;
       }
    }
    private string _Pay_Method;


    /// <summary>
    /// Account Class
    /// </summary>
 
    [RfcField(AbapName = "ACCOUNT_CLASS", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 4, Length2 = 8, Offset = 592, Offset2 = 1184)]
    [XmlElement("ACCOUNT_CLASS", Form=XmlSchemaForm.Unqualified)]
    public string Account_Class
    { 
       get
       {
          return _Account_Class;
       }
       set
       {
          _Account_Class = value;
       }
    }
    private string _Account_Class;


    /// <summary>
    /// Security Deposit
    /// </summary>
 
    [RfcField(AbapName = "SEC_DEPOSIT", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 20, Length2 = 40, Offset = 596, Offset2 = 1192)]
    [XmlElement("SEC_DEPOSIT", Form=XmlSchemaForm.Unqualified)]
    public string Sec_Deposit
    { 
       get
       {
          return _Sec_Deposit;
       }
       set
       {
          _Sec_Deposit = value;
       }
    }
    private string _Sec_Deposit;


    /// <summary>
    /// Meter Installation Date
    /// </summary>
 
    [RfcField(AbapName = "DATE_METER", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 616, Offset2 = 1232)]
    [XmlElement("DATE_METER", Form=XmlSchemaForm.Unqualified)]
    public string Date_Meter
    { 
       get
       {
          return _Date_Meter;
       }
       set
       {
          _Date_Meter = value;
       }
    }
    private string _Date_Meter;


    /// <summary>
    /// Function Class
    /// </summary>
 
    [RfcField(AbapName = "FUNCTION_CLASS", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 624, Offset2 = 1248)]
    [XmlElement("FUNCTION_CLASS", Form=XmlSchemaForm.Unqualified)]
    public string Function_Class
    { 
       get
       {
          return _Function_Class;
       }
       set
       {
          _Function_Class = value;
       }
    }
    private string _Function_Class;


    /// <summary>
    /// Collection Area
    /// </summary>
 
    [RfcField(AbapName = "COLLECTION_AREA", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 200, Length2 = 400, Offset = 632, Offset2 = 1264)]
    [XmlElement("COLLECTION_AREA", Form=XmlSchemaForm.Unqualified)]
    public string Collection_Area
    { 
       get
       {
          return _Collection_Area;
       }
       set
       {
          _Collection_Area = value;
       }
    }
    private string _Collection_Area;


    /// <summary>
    /// Paid By
    /// </summary>
 
    [RfcField(AbapName = "PAID_BY", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 12, Length2 = 24, Offset = 832, Offset2 = 1664)]
    [XmlElement("PAID_BY", Form=XmlSchemaForm.Unqualified)]
    public string Paid_By
    { 
       get
       {
          return _Paid_By;
       }
       set
       {
          _Paid_By = value;
       }
    }
    private string _Paid_By;


    /// <summary>
    /// Travel Supporting Fee
    /// </summary>
 
    [RfcField(AbapName = "TRAVEL_FEE", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 10, Length2 = 20, Offset = 844, Offset2 = 1688)]
    [XmlElement("TRAVEL_FEE", Form=XmlSchemaForm.Unqualified)]
    public string Travel_Fee
    { 
       get
       {
          return _Travel_Fee;
       }
       set
       {
          _Travel_Fee = value;
       }
    }
    private string _Travel_Fee;


    /// <summary>
    /// Penalty Charge (Y/N)
    /// </summary>
 
    [RfcField(AbapName = "PENALTY_FLAG", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 854, Offset2 = 1708)]
    [XmlElement("PENALTY_FLAG", Form=XmlSchemaForm.Unqualified)]
    public string Penalty_Flag
    { 
       get
       {
          return _Penalty_Flag;
       }
       set
       {
          _Penalty_Flag = value;
       }
    }
    private string _Penalty_Flag;


    /// <summary>
    /// n/a
    /// </summary>
 
    [RfcField(AbapName = "NA1", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 855, Offset2 = 1710)]
    [XmlElement("NA1", Form=XmlSchemaForm.Unqualified)]
    public string Na1
    { 
       get
       {
          return _Na1;
       }
       set
       {
          _Na1 = value;
       }
    }
    private string _Na1;


    /// <summary>
    /// n/a
    /// </summary>
 
    [RfcField(AbapName = "NA2", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 856, Offset2 = 1712)]
    [XmlElement("NA2", Form=XmlSchemaForm.Unqualified)]
    public string Na2
    { 
       get
       {
          return _Na2;
       }
       set
       {
          _Na2 = value;
       }
    }
    private string _Na2;


    /// <summary>
    /// n/a
    /// </summary>
 
    [RfcField(AbapName = "NA3", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 857, Offset2 = 1714)]
    [XmlElement("NA3", Form=XmlSchemaForm.Unqualified)]
    public string Na3
    { 
       get
       {
          return _Na3;
       }
       set
       {
          _Na3 = value;
       }
    }
    private string _Na3;


    /// <summary>
    /// Contract start date
    /// </summary>
 
    [RfcField(AbapName = "CON_BEGIN", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 858, Offset2 = 1716)]
    [XmlElement("CON_BEGIN", Form=XmlSchemaForm.Unqualified)]
    public string Con_Begin
    { 
       get
       {
          return _Con_Begin;
       }
       set
       {
          _Con_Begin = value;
       }
    }
    private string _Con_Begin;


    /// <summary>
    /// Contract end date
    /// </summary>
 
    [RfcField(AbapName = "CON_END", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 866, Offset2 = 1732)]
    [XmlElement("CON_END", Form=XmlSchemaForm.Unqualified)]
    public string Con_End
    { 
       get
       {
          return _Con_End;
       }
       set
       {
          _Con_End = value;
       }
    }
    private string _Con_End;


    /// <summary>
    /// Receipt Type
    /// </summary>
 
    [RfcField(AbapName = "RECEIPT_TYPE", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 874, Offset2 = 1748)]
    [XmlElement("RECEIPT_TYPE", Form=XmlSchemaForm.Unqualified)]
    public string Receipt_Type
    { 
       get
       {
          return _Receipt_Type;
       }
       set
       {
          _Receipt_Type = value;
       }
    }
    private string _Receipt_Type;


    /// <summary>
    /// Note: Name of Payer
    /// </summary>
 
    [RfcField(AbapName = "PAYER_NAME", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 132, Length2 = 264, Offset = 875, Offset2 = 1750)]
    [XmlElement("PAYER_NAME", Form=XmlSchemaForm.Unqualified)]
    public string Payer_Name
    { 
       get
       {
          return _Payer_Name;
       }
       set
       {
          _Payer_Name = value;
       }
    }
    private string _Payer_Name;


    /// <summary>
    /// Interest Key
    /// </summary>
 
    [RfcField(AbapName = "INTEREST_KEY", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 2, Length2 = 4, Offset = 1007, Offset2 = 2014)]
    [XmlElement("INTEREST_KEY", Form=XmlSchemaForm.Unqualified)]
    public string Interest_Key
    { 
       get
       {
          return _Interest_Key;
       }
       set
       {
          _Interest_Key = value;
       }
    }
    private string _Interest_Key;


    /// <summary>
    /// Bill Consolidator
    /// </summary>
 
    [RfcField(AbapName = "BILL_CONSOL", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Length2 = 16, Offset = 1009, Offset2 = 2018)]
    [XmlElement("BILL_CONSOL", Form=XmlSchemaForm.Unqualified)]
    public string Bill_Consol
    { 
       get
       {
          return _Bill_Consol;
       }
       set
       {
          _Bill_Consol = value;
       }
    }
    private string _Bill_Consol;


    /// <summary>
    /// Flag Delete
    /// </summary>
 
    [RfcField(AbapName = "FLAG", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 1017, Offset2 = 2034)]
    [XmlElement("FLAG", Form=XmlSchemaForm.Unqualified)]
    public string Flag
    { 
       get
       {
          return _Flag;
       }
       set
       {
          _Flag = value;
       }
    }
    private string _Flag;


    /// <summary>
    /// O=Overwrite
    /// </summary>
 
    [RfcField(AbapName = "ACTION", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Length2 = 2, Offset = 1018, Offset2 = 2036)]
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

    /// <summary>
    /// Ca Tax ID
    /// </summary>
    /// 
    [RfcField(AbapName = "TAXID", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 13, Length2 = 20, Offset = 1018, Offset2 = 2036)]
    [XmlElement("TAXID", Form = XmlSchemaForm.Unqualified)]
    public string TaxId
    {
        get
        {
            return _TaxId;
        }
        set
        {
            _TaxId = value;
        }
    }
    private string _TaxId;


    /// <summary>
    /// Tax Branch
    /// </summary>
    [RfcField(AbapName = "BRANCH", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 13, Length2 = 20, Offset = 1018, Offset2 = 2036)]
    [XmlElement("BRANCH", Form = XmlSchemaForm.Unqualified)]
    public string Branch
    {
        get
        {
            return _branch;
        }
        set
        {
            _branch = value;
        }
    }
    private string _branch;

  }

}
