using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.IO;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureSG;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Globalization;
using System.Collections;
using System.Windows.Forms;
using System.Linq;

namespace PEA.BPM.Architecture.ArchitectureTool
{
    [Serializable]
    public class CodeTable
    {       
        private static CodeTable _ct = new CodeTable();

        private DataSet _codeTable;
        private string _cacheDataFileName = "CodeTable01.dat";
        private string _cacheSchemaFileName = "CodeTable01.xsd";

        private List<Bank> _banks;
        private List<BankAccount> _bankAccounts;
        private List<MeterSize> _meterSizes;
        private List<DebtType> _debtTypes;
        private List<Branch> _branches;
        private List<Terminal> _terminals;
        private List<CalendarInfo> _calendar;
        private List<UnlockRemark> _unlockRemark;
        private List<InterestKey> _interestKey;
        private List<TaxCode> _taxCode;
        private List<UnitType> _unitType;
        private List<AppSetting> _appSetting;
        private List<Deposit> _deposits;
        private List<PaymentSequence> _paymentSequence;


        public static CodeTable Instant
        {
            get
            {
                return _ct;
            }
        }

        private CodeTable()
        {
            if (this.LoadData())
            {
                _banks = (List<Bank>)this.GetData("Bank");
                _bankAccounts = (List<BankAccount>)this.GetData("BankAccount");
                _meterSizes = (List<MeterSize>)this.GetData("MeterSize");
                _debtTypes = (List<DebtType>)this.GetData("DebtType");
                _branches = (List<Branch>)this.GetData("Branch");
                _terminals = (List<Terminal>)this.GetData("Terminal");
                _calendar = (List<CalendarInfo>)this.GetData("Calendar");
                _unlockRemark = (List<UnlockRemark>)this.GetData("UnlockRemark");
                _interestKey = (List<InterestKey>)this.GetData("InterestKey");
                _taxCode = (List<TaxCode>)this.GetData("TaxCode");
                _unitType = (List<UnitType>)this.GetData("UnitType");
                _appSetting = (List<AppSetting>)this.GetData("AppSetting");
                _deposits = (List<Deposit>)this.GetData("Deposit");
                _paymentSequence = (List<PaymentSequence>)this.GetData("PaymentSequence");
            }
            else
            {
                MessageBox.Show("โปรแกรมไม่สามารถทำงานต่อได้ เนื่องจากไม่สามารถสร้าง CodeTable\n"+
                                "สาเหตุจากการติดตั้งเครื่องครั้งแรกและไม่สามารถเชื่อมต่อแม่ข่ายได้\nกรุณาติดต่อผู้ดูแลระบบ",  "ผิดพลาด",
                                MessageBoxButtons.OK, MessageBoxIcon.Error,   MessageBoxDefaultButton.Button1, 
                                MessageBoxOptions.DefaultDesktopOnly);
                Session.Work.OnCloseNotify = false;
                Application.Exit();
            }
        }

        public ICodeTableService GetService()
        {
            return CodeTableSG.Instance(Session.Branch.OnlineConnection);
        }

        public bool LoadData()
        {
            bool ret = true;
            ICodeTableService bs = this.GetService();
            _codeTable = new DataSet();
            DateTime lastUpdatedDate = new DateTime(2000, 1, 1);

            if (Session.IsNetworkConnectionAvailable && Session.User.Id != null)
            {
                if (File.Exists(_cacheDataFileName))
                {
                    try
                    {
                        _codeTable = LoadDataFromFile();
                        if (_codeTable.Tables["Time"].Rows.Count > 0)
                        {
                            lastUpdatedDate = (DateTime)_codeTable.Tables["Time"].Rows[0]["ReadDate"];
                            _codeTable.Tables["Time"].Rows.Clear();
                        }

                        DataSet dbCodeTable = bs.GetUpdatedData(lastUpdatedDate, Session.Branch.Id);

                        // Remove override table
                        //RemoveOverrideTable(_codeTable, dbCodeTable, "Bank");
                        MergeCodeTable(_codeTable, dbCodeTable);
                        //_codeTable.Merge(dbCodeTable, false, MissingSchemaAction.Add);

                        ClearDeletedData(_codeTable);
                        SaveDataToFile(_codeTable);
                        ret = true;
                    }
                    catch (Exception)
                    {
                        //ret = false;
                    }
                }
                else //load whole stuff
                {
                    _codeTable = bs.GetUpdatedData(lastUpdatedDate, Session.Branch.Id);
                    ClearDeletedData(_codeTable);
                    SaveDataToFile(_codeTable);
                    ret = true;
                }
            }
            else //load from file
            {
                if (File.Exists(_cacheDataFileName))
                {
                    try
                    {
                        _codeTable = LoadDataFromFile();
                        ret = true;
                    }
                    catch (Exception)
                    {
                        //ret = false;
                    }
                }
                else
                {
                    ret = false;
                }
            }

            return ret;
        }

        private void RemoveOverrideTable(DataSet localCodeTable, DataSet dbCodeTable, string tableName)
        {
            if (dbCodeTable.Tables[tableName].Rows.Count > 0)
            {
                localCodeTable.Tables.Remove(tableName);
            }
        }

        private DataSet LoadDataFromFile()
        {
            DataSet codeTable = new DataSet();
            codeTable.ReadXmlSchema(_cacheSchemaFileName);
            codeTable.ReadXml(_cacheDataFileName);

            return codeTable;
        }

        private void SaveDataToFile(DataSet codeTable)
        {
            codeTable.WriteXml(_cacheDataFileName);
            codeTable.WriteXmlSchema(_cacheSchemaFileName);
        }

        private static void MergeCodeTable(DataSet codeTable, DataSet download)
        {
            try
            {
                string tableName = "Time";
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "Bank";
                DataRow[] bankRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                      join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                                on b.Field<string>("BankKey") equals m.Field<string>("BankKey")
                                       select b).ToArray();

                foreach (DataRow row in bankRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "BankAccount";
                DataRow[] bankAccRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                         join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>() on
                                                     new { BankKey = b.Field<string>("BankKey"), AccountNo = b.Field<string>("AccountNo") } equals
                                                     new { BankKey = m.Field<string>("BankKey"), AccountNo = m.Field<string>("AccountNo") }
                                         select b).ToArray();

                foreach (DataRow row in bankAccRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "MeterSize";
                DataRow[] meterRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                       join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                              on b.Field<string>("MeterSizeId") equals m.Field<string>("MeterSizeId")
                                      select b).ToArray();

                foreach (DataRow row in meterRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "DebtType";
                DataRow[] debtTypeRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                          join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                              on b.Field<string>("DtId") equals m.Field<string>("DtId")
                                       select b).ToArray();

                foreach (DataRow row in debtTypeRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);


                tableName = "Branch";
                DataRow[] branchRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                        join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                              on b.Field<string>("BranchId") equals m.Field<string>("BranchId")
                                          select b).ToArray();

                foreach (DataRow row in branchRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "Terminal";
                DataRow[] terminalRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                          join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                              on b.Field<string>("TerminalId") equals m.Field<string>("TerminalId")
                                        select b).ToArray();

                foreach (DataRow row in terminalRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);


                tableName = "Calendar";
                DataRow[] calendarRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                          join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                              on b.Field<DateTime>("NwDay") equals m.Field<DateTime>("NwDay")
                                          select b).ToArray();

                foreach (DataRow row in calendarRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "UnlockRemark";
                DataRow[] unlockRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                        join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>() on
                                                     new { FncId = b.Field<string>("FncId"), RemarkId = b.Field<string>("RemarkId") } equals
                                                     new { FncId = m.Field<string>("FncId"), RemarkId = m.Field<string>("RemarkId") }
                                         select b).ToArray();

                foreach (DataRow row in unlockRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "InterestKey";
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "TaxCode";
                DataRow[] taxRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                     join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                              on b.Field<string>("TaxCode") equals m.Field<string>("TaxCode")
                                          select b).ToArray();

                foreach (DataRow row in taxRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "UnitType";
                DataRow[] unitRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                      join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                              on b.Field<string>("UnitTypeId") equals m.Field<string>("UnitTypeId")
                                     select b).ToArray();

                foreach (DataRow row in unitRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "AppSetting";
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "Deposit";
                DataRow[] depositRows = (from b in codeTable.Tables[tableName].AsEnumerable().Cast<DataRow>()
                                         join m in download.Tables[tableName].AsEnumerable().Cast<DataRow>() on
                                                     new { BankKey = b.Field<string>("BankKey"), BusinessPlace = b.Field<string>("BusinessPlace"), ClearingAccNo = b.Field<string>("ClearingAccNo"), AccountNo = b.Field<string>("AccountNo") } equals
                                                     new { BankKey = m.Field<string>("BankKey"), BusinessPlace = m.Field<string>("BusinessPlace"), ClearingAccNo = m.Field<string>("ClearingAccNo"), AccountNo = m.Field<string>("AccountNo") }
                                        select b).ToArray();

                foreach (DataRow row in depositRows)
                    codeTable.Tables[tableName].Rows.Remove(row);
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);

                tableName = "PaymentSequence";
                codeTable.Tables[tableName].Merge(download.Tables[tableName]);
            }
            catch (Exception)
            {

            }

            codeTable.AcceptChanges();
        }

        private static void ClearDeletedData(DataSet codeTable)
        {
            foreach (DataTable dt in codeTable.Tables)
            {
                try
                {
                    DataRow[] drs = dt.Select("Active = 0");
                    if ((drs != null) && (drs.Length > 0))
                    {
                        foreach (DataRow dr in drs)
                        {
                            dr.Delete();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            codeTable.AcceptChanges();
        }



        internal object GetData(string type)
        {
            DataTable dt;

            switch (type)
            {
                case "Bank":
                    _banks = new List<Bank>();
                    dt = _codeTable.Tables["Bank"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _banks.Add(
                            new Bank(
                                DaHelper.GetString(dr, "BankKey"),
                                DaHelper.GetString(dr, "BankName"),
                                DaHelper.GetString(dr, "BusinessPlace"),
                                DaHelper.GetString(dr, "ClearingAccNo"),
                                DaHelper.GetString(dr, "GroupBankName")
                            )
                            );
                    }
                    return _banks;
                case "BankAccount":
                    _bankAccounts = new List<BankAccount>();
                    dt = _codeTable.Tables["BankAccount"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _bankAccounts.Add(
                            new BankAccount(
                                DaHelper.GetString(dr, "BankKey"),
                                DaHelper.GetString(dr, "AccountNo"),
                                DaHelper.GetString(dr, "BusinessPlace")
                            )
                            );
                    }
                    return _bankAccounts;
                case "MeterSize":
                    _meterSizes = new List<MeterSize>();
                    dt = _codeTable.Tables["MeterSize"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _meterSizes.Add(
                            new MeterSize(
                                DaHelper.GetString(dr, "MeterSizeId"),
                                DaHelper.GetString(dr, "MeterSizeName"),
                                DaHelper.GetDecimal(dr, "ReconnectMeterRate")
                            )
                            );
                    }
                    return _meterSizes;
                case "DebtType":
                    _debtTypes = new List<DebtType>();
                    dt = _codeTable.Tables["DebtType"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _debtTypes.Add(
                            new DebtType(
                                DaHelper.GetString(dr, "DtId"),
                                DaHelper.GetString(dr, "DtName"),                              
                                DaHelper.GetString(dr, "DefaultPaperSize"),
                                DaHelper.GetString(dr, "DefaultTaxCode"),
                                DaHelper.GetString(dr, "DefaultInterestKey"),
                                DaHelper.GetString(dr, "ModReceiptHeaderFlag"),
                                DaHelper.GetString(dr, "IndividualReceiptFlag"),
                                DaHelper.GetString(dr, "NonInvoicePaymentFlag"),
                                DaHelper.GetString(dr, "CategoryPaymentCode"),
                                DaHelper.GetString(dr, "PrintDescription")
                            )
                            );
                    }
                    return _debtTypes;
                case "Branch":
                    _branches = new List<Branch>();
                    dt = _codeTable.Tables["Branch"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _branches.Add(
                            new Branch(
                                DaHelper.GetString(dr, "BranchId"),
                                DaHelper.GetString(dr, "BranchName"),
                                DaHelper.GetString(dr, "BranchName2"),
                                //DaHelper.GetString(dr, "Address"),
                                DaHelper.GetString(dr, "BranchLevel"),
                                //DaHelper.GetString(dr, "CdType"),
                                DaHelper.GetString(dr, "ParentBranchId")
                            )
                            );
                    }
                    return _branches;
                case "Terminal":
                    _terminals = new List<Terminal>();
                    dt = _codeTable.Tables["Terminal"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _terminals.Add(
                            new Terminal(
                                DaHelper.GetString(dr, "TerminalId"),
                                DaHelper.GetString(dr, "TerminalCode"),
                                DaHelper.GetString(dr, "BranchId"),
                                DaHelper.GetString(dr, "TaxCode"),
                                DaHelper.GetString(dr, "IP")
                            )
                            );
                    }
                    return _terminals;
                case "Calendar" :
                    DateTimeFormatInfo _th_dt;
                    CultureInfo th_culture = new CultureInfo("th-TH");

                    _calendar = new List<CalendarInfo>();
                    dt = _codeTable.Tables["Calendar"];                    
                    _th_dt = th_culture.DateTimeFormat;                 
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime nwDate = (DateTime)DaHelper.GetDate(dr, "NWDay");
                        string tempDate = nwDate.ToString("yyyyMMdd", _th_dt);
                       
                        _calendar.Add(
                            new CalendarInfo(
                                tempDate,
                                DaHelper.GetString(dr, "BranchId"),                             
                                DaHelper.GetDate(dr, "ModifiedDt"),                               
                                DaHelper.GetString(dr, "CdType"),
                                DaHelper.GetString(dr, "Active")
                        )
                       );
                    }
                    return _calendar;

                case "UnlockRemark":
                    _unlockRemark = new List<UnlockRemark>();
                    dt = _codeTable.Tables["UnlockRemark"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _unlockRemark.Add(
                            new UnlockRemark(
                                DaHelper.GetString(dr, "FncId"),
                                DaHelper.GetString(dr, "RemarkId"),
                                DaHelper.GetString(dr, "Description"))
                        );
                    }
                    return _unlockRemark;

                case "InterestKey":
                    _interestKey = new List<InterestKey>();
                    dt = _codeTable.Tables["InterestKey"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _interestKey.Add(
                            new InterestKey(
                                DaHelper.GetString(dr, "InterestKey"),
                                DaHelper.GetString(dr, "InterestName"),
                                DaHelper.GetDecimal(dr, "InterestRate"))
                        );
                    }
                    return _interestKey;
                case "TaxCode":
                    _taxCode = new List<TaxCode>();
                    dt = _codeTable.Tables["TaxCode"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _taxCode.Add(
                            new TaxCode(
                                DaHelper.GetString(dr, "TaxCode"),
                                DaHelper.GetString(dr, "TaxName"),
                                DaHelper.GetDecimal(dr, "TaxRate"))
                        );
                    }
                    return _taxCode;
                case "UnitType":
                    _unitType = new List<UnitType>();
                    dt = _codeTable.Tables["UnitType"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _unitType.Add(
                            new UnitType(
                                DaHelper.GetString(dr, "UnitTypeId"),
                                DaHelper.GetString(dr, "UnitTypeName"))
                        );
                    }
                    return _unitType;
                case "AppSetting":
                    _appSetting = new List<AppSetting>();
                    dt = _codeTable.Tables["AppSetting"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _appSetting.Add(
                            new AppSetting(
                                DaHelper.GetString(dr, "SettingCode"),
                                DaHelper.GetString(dr, "Module"),
                                DaHelper.GetString(dr, "SettingValue"))
                        );
                    }
                    return _appSetting;
                case "Deposit":
                    _deposits = new List<Deposit>();
                    dt = _codeTable.Tables["Deposit"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _deposits.Add(
                            new Deposit(
                                DaHelper.GetString(dr, "BankKey"),
                                DaHelper.GetString(dr, "BankName"),
                                DaHelper.GetString(dr, "BusinessPlace"),
                                DaHelper.GetString(dr, "ClearingAccNo"),
                                DaHelper.GetString(dr, "AccountNo"),
                                DaHelper.GetString(dr, "AccountType"),
                                DaHelper.GetString(dr, "AccountNoDesc"),
                                DaHelper.GetString(dr, "GroupBankName"))
                        );
                    }
                    return _deposits;
                case "PaymentSequence":
                    _paymentSequence = new List<PaymentSequence>();
                    dt = _codeTable.Tables["PaymentSequence"];
                    foreach (DataRow dr in dt.Rows)
                    {
                        _paymentSequence.Add(
                            new PaymentSequence(
                                DaHelper.GetString(dr, "Sequence"),
                                DaHelper.GetString(dr, "DtId"))
                        );
                    }
                    return _paymentSequence;
                default:
                    return null;
            }
        }

        public List<Bank> ListBanks()
        {
            return this._banks;
        }

        public List<Deposit> ListDepositsByBusinessPlace(string businessPlace)
        {
            return this._deposits.FindAll(delegate(Deposit b)
                {
                    return b.BusinessPlace == businessPlace;
                }
            );
        }

        public List<Bank> ListBanksByBusinessPlace(string businessPlace)
        {
            return this._banks.FindAll(delegate(Bank b)
                {
                    return b.BusinessPlace == businessPlace;
                }
            );
        }

        public List<Bank> ListBanksFromDepositByBusinessPlace(string businessPlace)
        {
            List<Deposit> deposits = new List<Deposit>();

            deposits = this._deposits.FindAll(delegate(Deposit d)
                {
                    return d.BusinessPlace == businessPlace;
                }
            );

            List<Bank> banks = new List<Bank>(deposits.Count);
            foreach (Deposit aDeposit in deposits)
            { 

                if (!banks.Exists(delegate(Bank b) { return b.BankKey == aDeposit.BankKey; }))
                {
                    Bank aBank = new Bank(aDeposit.BankKey, aDeposit.BankName, aDeposit.BusinessPlace, aDeposit.ClearingAccNo, aDeposit.GroupBankName);
                    banks.Add(aBank);
                }
            }

            return banks;
        }

        public List<Bank> ListBanksByBankKey(string bankKey)
        {
            return this._banks.FindAll(delegate(Bank b)
                {
                    return b.BankKey == bankKey;
                }
            );
        }

        public List<BankAccount> ListBankAccounts(string bankKey)
        {
            return this._bankAccounts.FindAll(delegate(BankAccount bc)
                {
                    return bc.BankKey == bankKey;
                }
            );
        }

        public List<BankAccount> ListBankAccountsByBusinessPlace(string businessPlace)
        {
            return this._bankAccounts.FindAll(delegate(BankAccount bc)
                {
                    return bc.BusinessPlace == businessPlace;
                }
            );
        }

        public List<Deposit> ListDepositByBusinessPlace(string businessPlace)
        {
            return this._deposits.FindAll(delegate(Deposit d)
                {
                    return d.BusinessPlace == businessPlace;
                }
            );
        }

        public List<Deposit> ListDepositByBankKey(string businessPlace, string bankKey)
        {
            return this._deposits.FindAll(delegate(Deposit d)
                {
                    return d.BusinessPlace == businessPlace && d.BankKey == bankKey;
                }
            );
        }

        public List<Deposit> ListDepositByAccountNo(string businessPlace, string accountNo)
        {
            return this._deposits.FindAll(delegate(Deposit d)
                {
                    return d.BusinessPlace == businessPlace && d.AccountNo == accountNo;
                }
            );
        }

        public List<MeterSize> ListMeterSizes()
        {
            return this._meterSizes;
        }

        public List<DebtType> ListDebtTypes()
        {
            return this._debtTypes;
        }

        public List<Branch> ListBranches()
        {
            return this._branches;
        }

        public Branch ListBranches(string branchId)
        {
            return this._branches.Find(delegate(Branch b)
                {
                    return b.BranchId == branchId;
                }
            );
        }

        /// <summary>
        /// List all child of parent branch by cosidering of branch level
        /// Parent L1, L2 - Child L3, L4
        /// Parent L3 - Child L4
        /// Z0000 - List ALL
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<string> ListChildBranchByParent(string parent)
        {
            List<string> retList = new List<string>();
            retList.Add(parent);
            retList.AddRange(Seed(parent));
            retList.Sort();

            return retList;
        }

        //avoid creating class member
        public List<string> Seed(string parent)
        {
            List<string> branchList = new List<string>();
            foreach (Branch b in _branches)
            {
                if (parent!= null && b.ParentBranchId == parent)
                {
                    branchList.Add(b.BranchId);
                    branchList.AddRange(Seed(b.BranchId));
                }
            }
            return branchList;
        }


        public List<Branch> ListChildBranch(Branch parent)
        {
            List<Branch> retList = new List<Branch>();
            Branch p = this._branches.Find(delegate(Branch b) { return b.BranchId.Equals(parent.BranchId); });
            retList.Add(p);
            retList.AddRange(Seed(parent));
            retList.Sort();

            return retList;
        }

        //avoid creating class member
        public List<Branch> Seed(Branch parent)
        {
            List<Branch> branchList = new List<Branch>();
            foreach (Branch b in _branches)
            {
                if (b.ParentBranchId != null && b.ParentBranchId == parent.BranchId)
                {
                    branchList.Add(b);
                    branchList.AddRange(Seed(b));
                }
            }
            return branchList;
        }

        //One upper lever
        public string GetParentOfChild(string child)
        {
            foreach (Branch b in _branches)
            {
                if (b.BranchId == child && b.BranchLevel == "4")
                    return b.ParentBranchId;
            }

            return child;
        }

        public string GetBranchName(string branchId)
        {
            foreach (Branch b in _branches)
            {
                if (b.BranchId == branchId)
                    return b.BranchName;
            }

            return null;
        }

        public List<Terminal> ListTerminals(string branchId)
        {
            List<Terminal> terminals = new List<Terminal>();
            foreach (Terminal terminal in _terminals)
            {
                if (terminal.BranchId == branchId)
                {
                    terminals.Add(terminal);
                }
            }
            return terminals;
        }

        public List<CalendarInfo> ListCendar()
        {
            return this._calendar;
        }

        public List<UnlockRemark> ListUnlockRemark(string functionId)
        {
            return this._unlockRemark.FindAll(delegate(UnlockRemark ur)
            {
                return ur.FncId.Equals(functionId);
            }
            );
        }

        public List<InterestKey> ListInterestKey()
        {
            return this._interestKey;
        }

        public List<TaxCode> ListTaxCode()
        {
            return this._taxCode;
        }

        public List<UnitType> ListUnitType()
        {
            return this._unitType;
        }

        public List<PaymentSequence> ListPaymentSequence()
        {
            return this._paymentSequence;
        }

        public List<AppSetting> ListAppSetting()
        {
            return this._appSetting;
        }

        public string GetAppSettingValue(string code)
        {
            AppSetting apps = this._appSetting.Find(delegate(AppSetting ap)
            {
                return ap.Code.Equals(code);
            }
            );

            if (null != apps)
            {
                return apps.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
