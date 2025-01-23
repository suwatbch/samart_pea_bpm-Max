using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Views.ToBePaidInvoiceView
{
    public partial class DisconnectionStatus : Form
    {
        private List<DisconnectionDoc> _disconnectDocs;

        public DisconnectionStatus()
        {
            InitializeComponent();
        }

        public void SetDisconnectStatus(List<DisconnectionDoc> disconnectDocs)
        {
            _disconnectDocs = disconnectDocs;
            BindingForm();
        }

        private void BindingForm()
        {
            if (_disconnectDocs.Count == 1)
            {
                foreach (DisconnectionDoc disconnectionDoc in _disconnectDocs)
                {
                    lbStatusDesc.Text = GetStatusDesc(disconnectionDoc);
                    lbDiscNo.Text = GetString(disconnectionDoc.DiscNo);
                    lbCreatedDt.Text = GetString(disconnectionDoc.CreatedDt);
                    lbReleasedDate.Text = GetString(disconnectionDoc.ReleaseDt);
                    lbWorkOrderNo.Text = GetString(disconnectionDoc.WorkOrderNo);
                    lbDiscPlanStartDate.Text = GetString(disconnectionDoc.DiscPlanStart);
                    lbDiscPlanEndDate.Text = GetString(disconnectionDoc.DiscPlanEnd);
                    lbWorkCenter.Text = GetString(disconnectionDoc.WorkCenter);
                    lbPostponeDate.Text = GetString(disconnectionDoc.PostpConfirm);
                    lbFuseRemoveDate.Text = GetString(disconnectionDoc.FuseRemConfirm);
                    lbMeterRemovedDate.Text = GetString(disconnectionDoc.MeterRemConfirm);
                }
            }
        }

        private string GetStatusDesc(DisconnectionDoc disconnectionDoc)
        {
            string retStr = "";
            //int indexStr = 0; //default : approved
            if (disconnectionDoc.DiscStatusId.Trim() == "10")
            {
                string[] arrStr = disconnectionDoc.DiscStatus.Split('|');
                if (disconnectionDoc.MeterRemConfirm == null)
                    retStr = arrStr[0];//approved
                else
                    retStr = arrStr[1];//meter-removed
            }
            else
                retStr = GetString(disconnectionDoc.DiscStatus);

            return retStr;
        }

        #region +++ Command Handler +++
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region +++ Custom Function +++

        private string GetString(object obj)
        {
            string str = "-";
            if (obj is System.DateTime?)
                str = GetDateFormat(obj as System.DateTime?);
            else if (obj is System.String)
                str = GetStringFormat(obj as System.String);
            else if (obj == null)
                str = "-";
            return str;
        }

        private string GetDateFormat(DateTime? date)
        {
            return (date == null ? "-" : date.Value.ToString("HH:mm:ss") == "00:00:00" ? date.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) : date.Value.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("th-TH")));
        }

        private string GetStringFormat(string st)
        { 
            return ( st==null ? "-" : st );
        }

        #endregion


    }
}