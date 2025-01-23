using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using System.IO;
using System.Collections;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BranchInfo : IListUtility<BranchInfo>
    {
       	private string _BranchId;
	    private string _BranchName;
	    private string _BranchName2;
	    private string _BranchNo;
	    private string _Address;
	    private string _BranchLevel;
	    private string _BusinessArea;
	    private string _BusinessPlace;
	    private string _CdType;
	    private string _ParentBranchId;
	    private string _SyncFlag;
	    private DateTime? _ModifiedDt;
	    private string _ModifiedBy;
	    private bool _Active;
        public string _Action;

	    [DataMember(Order = 1)]
	    public string BranchId
	    {
		    get { return _BranchId; }
		    set { _BranchId = value; }
	    }
	    [DataMember(Order = 2)]
	    public string BranchName
	    {
		    get { return _BranchName; }
		    set { _BranchName = value; }
	    }
	    [DataMember(Order = 3)]
	    public string BranchName2
	    {
		    get { return _BranchName2; }
		    set { _BranchName2 = value; }
	    }
	    [DataMember(Order = 4)]
	    public string BranchNo
	    {
		    get { return _BranchNo; }
		    set { _BranchNo = value; }
	    }
	    [DataMember(Order = 5)]
	    public string Address
	    {
		    get { return _Address; }
		    set { _Address = value; }
	    }
	    [DataMember(Order = 6)]
	    public string BranchLevel
	    {
		    get { return _BranchLevel; }
		    set { _BranchLevel = value; }
	    }
	    [DataMember(Order = 7)]
	    public string BusinessArea
	    {
		    get { return _BusinessArea; }
		    set { _BusinessArea = value; }
	    }
	    [DataMember(Order = 8)]
	    public string BusinessPlace
	    {
		    get { return _BusinessPlace; }
		    set { _BusinessPlace = value; }
	    }
	    [DataMember(Order = 9)]
	    public string CdType
	    {
		    get { return _CdType; }
		    set { _CdType = value; }
	    }
	    [DataMember(Order = 10)]
	    public string ParentBranchId
	    {
		    get { return _ParentBranchId; }
		    set { _ParentBranchId = value; }
	    }
	    [DataMember(Order = 11)]
	    public string SyncFlag
	    {
		    get { return _SyncFlag; }
		    set { _SyncFlag = value; }
	    }
	    [DataMember(Order = 12)]
	    public DateTime? ModifiedDt
	    {
		    get { return _ModifiedDt; }
		    set { _ModifiedDt = value; }
	    }
	    [DataMember(Order = 13)]
	    public string ModifiedBy
	    {
		    get { return _ModifiedBy; }
		    set { _ModifiedBy = value; }
	    }
	    [DataMember(Order = 14)]
	    public bool Active
	    {
		    get { return _Active; }
		    set { _Active = value; }
	    }
	    [DataMember(Order = 15)]
        public string Action
        {
            get { return this._Action; }
            set { this._Action = value; }
        }
        

        #region IListUtility<BranchInfo> Members

        public string ToStream()
        {
            //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            //string[] template = { BranchId, BranchName, Address1, Address2, BranchLevel, BusinessPlace,
            //                CdType, ParentBranchId, Action };
            //return string.Join("\t", template);
            throw new Exception("The Method is not implement yet");
        }

        public BranchInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            BranchInfo it = new BranchInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.Branch;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            it.BranchId = StringConvert.ToString(sp[i++].Trim());
            it.BusinessArea = StringConvert.ToString(sp[i++].Trim());
            it.BusinessPlace = StringConvert.ToString(sp[i++].Trim());
            it.BranchName = StringConvert.ToString(sp[i++].Trim());
            it.BranchName2 = StringConvert.ToString(sp[i++].Trim());
            it.Address = StringConvert.ToString(sp[i++].Trim());
            it.BranchNo = StringConvert.ToString(sp[i++].Trim());
            it.BranchLevel = StringConvert.ToString(sp[i++].Trim());
            it.CdType = StringConvert.ToString(sp[i++].Trim());
            it.ParentBranchId = StringConvert.ToString(sp[i++].Trim());
            it.Action = sp[i++].Trim();

            if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.ModifiedBy = "BATCH";
            it.ModifiedDt = DateTime.Now;
            it.SyncFlag = "1";
            it.Active = true;
            return it;
        }

        #endregion


    }

    public class BranchComparator : IComparer<BranchInfo>
    {
        #region IComparer<BranchInfo> Members

        public int Compare(BranchInfo x, BranchInfo y)
        {
            return string.Compare(x.BranchId, y.BranchId);
        }

        #endregion

    }

}