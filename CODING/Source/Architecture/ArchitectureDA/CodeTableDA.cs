using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace PEA.BPM.Architecture.ArchitectureDA
{
    public class CodeTableDA
    {
        public  DataSet GetUpdatedData(DateTime lastUpdatedDate, string branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_CodeTable");
            db.AddInParameter(cmd, "LastUpdateDt", DbType.DateTime, lastUpdatedDate);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataSet ds = db.ExecuteDataSet(cmd);

            int i = 0;
            ds.Tables[i].TableName = "Time";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["ReadDate"] };

            i = i + 1;
            ds.Tables[i].TableName = "Bank";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["BankKey"] };

            i = i + 1;
            ds.Tables[i].TableName = "BankAccount";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["BankKey"], ds.Tables[i].Columns["AccountNo"] };

            i = i + 1;
            ds.Tables[i].TableName = "MeterSize";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["MeterSizeId"] };

            i = i + 1;
            ds.Tables[i].TableName = "DebtType";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["DtId"] };

            i = i + 1;
            ds.Tables[i].TableName = "Branch";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["BranchId"] };

            i = i + 1;
            ds.Tables[i].TableName = "Terminal";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["TerminalId"] };

            i = i + 1;
            ds.Tables[i].TableName = "Calendar";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["NwDay"]};

            i = i + 1;
            ds.Tables[i].TableName = "UnlockRemark";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["FncId"], ds.Tables[i].Columns["RemarkId"] };

            i = i + 1;
            ds.Tables[i].TableName = "InterestKey";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["InterestKey"] };

            i = i + 1;
            ds.Tables[i].TableName = "TaxCode";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["TaxCode"] };

            i = i + 1;
            ds.Tables[i].TableName = "UnitType";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["UnitTypeId"] };

            i = i + 1;
            ds.Tables[i].TableName = "AppSetting";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["SettingCode"] };

            i = i + 1;
            ds.Tables[i].TableName = "Deposit";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["BankKey"], ds.Tables[i].Columns["BusinessPlace"], ds.Tables[i].Columns["ClearingAccNo"], ds.Tables[i].Columns["AccountNo"] };

            i = i + 1;
            ds.Tables[i].TableName = "PaymentSequence";
            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns["Sequence"] };

            return ds;
        }
    }
}
