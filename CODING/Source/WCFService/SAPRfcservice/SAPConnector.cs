using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAP.Middleware.Connector;
using System.Collections;

namespace SAP
{
    public class SAPConnector
    {
        public SAPConnector()
        {
        }

        public DataTable ToADODataTable(IRfcTable lrfcTable)
        {
            //sapnco_util
            DataTable loTable = new DataTable();

            //... Create ADO.Net table.
            for (int liElement = 0; liElement < lrfcTable.ElementCount; liElement++)
            {
                RfcElementMetadata metadata = lrfcTable.GetElementMetadata(liElement);
                loTable.Columns.Add(metadata.Name);
            }

            //... Transfer rows from lrfcTable to ADO.Net table.
            foreach (IRfcStructure row in lrfcTable)
            {
                DataRow ldr = loTable.NewRow();
                for (int liElement = 0; liElement < lrfcTable.ElementCount; liElement++)
                {
                    RfcElementMetadata metadata = lrfcTable.GetElementMetadata(liElement);
                    ldr[metadata.Name] = row.GetString(metadata.Name);
                }

                loTable.Rows.Add(ldr);
            }

            return loTable;
        }


        public DataSet Zpos_Zcaci029(string destName, string action, Object zCaDoc, string caNo, string posId, string refDoc, string startDate)
        {
            // get the destination
            RfcDestination dest = RfcDestinationManager.GetDestination(destName);
            // create a function object
            IRfcFunction func = dest.Repository.CreateFunction("ZPOS_ZCACI029");
            //prepare input parameters
            func.SetValue("ACTION", action);
            func.SetValue("CA_DOC", zCaDoc);
            func.SetValue("CA_NUMBER", caNo);
            func.SetValue("POS_ID", posId);
            func.SetValue("REFERENCE", refDoc);
            func.SetValue("SEARCH_PERIOD", startDate);

            // submit the RFC call
            func.Invoke(dest);
            // Return the table. The backend has added one more line to it.
            DataTable rfcTable0 = ToADODataTable(func.GetTable("MTR0060"));
            DataTable rfcTable1 = ToADODataTable(func.GetTable("MTR0090"));
            DataTable rfcTable2 = ToADODataTable(func.GetTable("TRR0010"));
            DataTable rfcTable3 = ToADODataTable(func.GetTable("TRR0020"));
            DataTable rfcTable4 = ToADODataTable(func.GetTable("TRR0040"));
            DataTable rfcTable5 = ToADODataTable(func.GetTable("TRR0045"));

            DataSet ds = new DataSet();
            ds.Tables.Add(rfcTable0);
            ds.Tables.Add(rfcTable1);
            ds.Tables.Add(rfcTable2);
            ds.Tables.Add(rfcTable3);
            ds.Tables.Add(rfcTable4);
            ds.Tables.Add(rfcTable5);
            return ds;
        }

        public void Zpos_Submit(string destName, string in_Event)
        {
            // get the destination
            RfcDestination dest = RfcDestinationManager.GetDestination(destName);
            // create a function object
            IRfcFunction func = dest.Repository.CreateFunction("ZPOS_SUBMIT");
            func.SetValue("IN_EVENT", in_Event);

            // submit the RFC call
            func.Invoke(dest);
        }

        public string Zpos_Submit_Post(string destName, string im_File, ArrayList im_Pos_Tab)
        {
            // get the destination
            RfcDestination dest = RfcDestinationManager.GetDestination(destName);
            // create a function object
            IRfcFunction func = dest.Repository.CreateFunction("ZPOS_SUBMIT_POST");
            //prepare input parameters
            func.SetValue("IM_FILE", im_File);
            IRfcTable zTableText = func.GetTable("IM_POS_TAB");

            for (int i = 0; i < im_Pos_Tab.Count; i++)
            {
                zTableText.Append();
                zTableText.SetValue(0, im_Pos_Tab[i].ToString());
            }

            func.SetValue("IM_POS_TAB", zTableText);

            // submit the RFC call
            func.Invoke(dest);
            // Return the table. The backend has added one more line to it.
            string ex_Msg = func.GetString("EX_MSG");
            return ex_Msg;
        }
    }
}
