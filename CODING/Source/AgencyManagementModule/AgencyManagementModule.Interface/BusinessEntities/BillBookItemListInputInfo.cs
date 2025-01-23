using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Collections;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookItemListInputInfo 
    {
        //book header information
        private BillBookHeaderInfo _headerInfo;
        private AgentInfo _agencyInfo;
        //book item information
        private BindingList<BillBookCreationInfo> _creationItemList = new BindingList<BillBookCreationInfo>();
        private List<CallingBillInfo> _exceptionalBillList = new List<CallingBillInfo>();
        private bool _enableSavePrint;
        private bool _isEditBillBook = false;
        private int _panelIndex = 0;
        private string _controllerId;

        //five cases for input extra bills
        private BindingList<BillBookCreationExtraInfo> _extraItem_Exp = new BindingList<BillBookCreationExtraInfo>();
        private BindingList<BillBookCreationExtraInfo> _extraItem_Plus = new BindingList<BillBookCreationExtraInfo>();
        private BindingList<BillBookCreationExtraInfo> _extraItem_CExp = new BindingList<BillBookCreationExtraInfo>();
        private BindingList<BillBookCreationExtraInfo> _extraItem_CPlus = new BindingList<BillBookCreationExtraInfo>();
        private BindingList<BillBookCreationExtraInfo> _extraItem_All = new BindingList<BillBookCreationExtraInfo>();
        

        public BillBookItemListInputInfo()
        {
            _headerInfo = new BillBookHeaderInfo();
            _agencyInfo = new AgentInfo();
            _creationItemList = new BindingList<BillBookCreationInfo>();
            _extraItem_Exp = new BindingList<BillBookCreationExtraInfo>();
            _extraItem_Plus = new BindingList<BillBookCreationExtraInfo>();
            _extraItem_CExp = new BindingList<BillBookCreationExtraInfo>();
            _extraItem_CPlus = new BindingList<BillBookCreationExtraInfo>();
            _extraItem_All = new BindingList<BillBookCreationExtraInfo>();
        }


        [DataMember(Order=1)]
        public BillBookHeaderInfo HeaderInfo
        {
            get { return _headerInfo; }
            set { _headerInfo = value; }
        }


        [DataMember(Order=2)]
        public AgentInfo AgencyInfo
        {
            get { return _agencyInfo; }
            set { _agencyInfo = value; }
        }


        [DataMember(Order=3)]
        public BindingList<BillBookCreationInfo> CreationItemList
        {
            get { return _creationItemList; }
            set { _creationItemList = value; }
        }

       

        [DataMember(Order=4)]
        public List<CallingBillInfo> ExceptionalBill
        {
            get { return _exceptionalBillList; }
            set { _exceptionalBillList = value; }
        }


        [DataMember(Order=5)]
        public bool EnableSavePrint
        {
            get { return _enableSavePrint; }
            set { _enableSavePrint = value; }
        }


        [DataMember(Order=6)]
        public int PanelIndex
        {
            get { return _panelIndex; }
            set { _panelIndex = value; }
        }


        [DataMember(Order=7)]
        public BindingList<BillBookCreationExtraInfo> ExtraItemExp
        {
            set { _extraItem_Exp = value; }
            get { return _extraItem_Exp; }
        }


        [DataMember(Order=8)]
        public BindingList<BillBookCreationExtraInfo> ExtraItemPlus
        {
            set { _extraItem_Plus = value; }
            get { return _extraItem_Plus; }
        }


        [DataMember(Order=9)]
        public BindingList<BillBookCreationExtraInfo> ExtraItemCurExp
        {
            set { _extraItem_CExp = value; }
            get { return _extraItem_CExp; }
        }


        [DataMember(Order=10)]
        public BindingList<BillBookCreationExtraInfo> ExtraItemCurPlus
        {
            set { _extraItem_CPlus = value; }
            get { return _extraItem_CPlus; }
        }

        private void ClearExist(BillBookCreationExtraInfo extraInfo)
        {
            foreach (BillBookCreationExtraInfo e in _extraItem_All)
            {
                //remove
                if (e.Number == extraInfo.Number)
                {
                    _extraItem_All.Remove(e);
                    break;
                }
            }
        }

        private void MergeAllList()
        {
            _extraItem_All = new BindingList<BillBookCreationExtraInfo>();
            foreach (BillBookCreationExtraInfo e in _extraItem_Exp)
            {
                if ( !e.NPeaCode.Contains("-") && ! e.NLineId.Contains("-") )
                {
                    e.FilterType = "2";
                    //ClearExist(e);
                    _extraItem_All.Add(e);
                }
            }

            foreach (BillBookCreationExtraInfo e in _extraItem_Plus)
            {
                if (!e.NPeaCode.Contains("-") && !e.NLineId.Contains("-"))
                {
                    e.FilterType = "3";
                    //ClearExist(e);
                    _extraItem_All.Add(e);
                }
            }

            foreach (BillBookCreationExtraInfo e in _extraItem_CExp)
            {
                if (!e.NPeaCode.Contains("-") && !e.NLineId.Contains("-"))
                {
                    e.FilterType = "4";
                    //ClearExist(e);
                    _extraItem_All.Add(e);
                }
            }

            foreach (BillBookCreationExtraInfo e in _extraItem_CPlus)
            {
                if (!e.NPeaCode.Contains("-") && !e.NLineId.Contains("-"))
                {
                    e.FilterType = "5";
                    //ClearExist(e);
                    _extraItem_All.Add(e);
                }
            }
        }


        [DataMember(Order=11)]
        public BindingList<BillBookCreationExtraInfo> BookExtraItems
        {
            get {
                MergeAllList();
                return _extraItem_All;             
            }
            set
            {
                _extraItem_Exp.Clear();
                _extraItem_Plus.Clear();
                _extraItem_CExp.Clear();
                _extraItem_CPlus.Clear();

                foreach (BillBookCreationExtraInfo e in value)
                {
                    if (e.FilterType == "2")
                        _extraItem_Exp.Add(e);
                    else if (e.FilterType == "3")
                        _extraItem_Plus.Add(e);
                    else if (e.FilterType == "4")
                        _extraItem_CExp.Add(e);
                    else if (e.FilterType == "5")
                        _extraItem_CPlus.Add(e);
                }
            }
        }

        //public string ControllerId
        //{
        //    get { return this._controllerId; }
        //    set { this._controllerId = value; }
        //}


        [DataMember(Order=12)]
        public bool IsEditBillBook
        {
            get { return this._isEditBillBook; }
            set { this._isEditBillBook = value; }
        }

    }
}
