using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;

namespace PEA.BPM.PaymentCollectionModule.Views.SearchMultiDocView
{
    [SmartPart]
    public partial class SearchMultiDataView : UserControl, ISearchMultiDataView
    {
        List<Invoice> _invoiceResultList;
        List<SearchByMultiDoc> _searchByMuliDocList;
        int _searchSuccessCount;
        int _searchFailCount;
        int _invoiceCount;
        SearchByMultiDocParam _searchType;
        int _searchLimitItem;

        public SearchMultiDataView()
        {
            InitializeComponent();


            _searchByMuliDocList = new List<SearchByMultiDoc>();

        }

        [CreateNew]
        public SearchMultiDataViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
            get
            {
                return _presenter;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #region ISearchMultiDataView Members

        public Button CancelButton
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void btnConvertStringToList_Click(object sender, EventArgs e)
        {
            string documentInput = caIdInputtextBox.Text.Trim();

            try
            {
                if (documentInput.Length == 0)
                {
                    MessageBox.Show("กรุณาระบุเลขเอกสารที่ต้องการค้น", "ค้นหาข้อมูลหนี้", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Init view ถ้ามีข้อมูลอยู่แล้ว 
                if (_searchByMuliDocList.Count > 0)
                {
                    // have data already must confirm.
                    if (_searchByMuliDocList.Count > 0)
                    {
                        // Confirm for clear data. 
                        if (MessageBox.Show("ต้องการค้นหาข้อมูลใหม่ใช่หรือไม่?", "ค้นหาข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            _searchByMuliDocList.Clear();
                        else
                            return;
                    }

                }

                PrepareSearch();

                _presenter.ConvertStringToDocumentNoList(caIdInputtextBox.Text);

                if (_searchByMuliDocList.Count == 0)
                    MessageBox.Show("ไม่มีพบข้อมูลเลขที่เอกสารเพื่อค้นหาหนี้", "ค้นหาข้อมูลหนี้", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception)
            {
                throw;
            }
        }

        #region ISearchMultiDataView Members

        public void AddSearchByMuliDocList(List<SearchByMultiDoc> documentList)
        {
            try
            {
                //// have data already must confirm.
                //if (_searchByMuliDocList.Count > 0 )
                //{
                //    // Confirm for clear data. 
                //    if (MessageBox.Show("ต้องการค้นหาข้อมูลใหม่ใช่หรือไม่?", "ค้นหาข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                //        _searchByMuliDocList.Clear();
                //    else
                //        return;
                //}

                // Add document list to view. 
                _searchByMuliDocList.AddRange(documentList);

                // Assign data to grid.
                documentListDataGridView.DataSource = new BindingList<SearchByMultiDoc>(_searchByMuliDocList);
                documentListDataGridView.Refresh();

                // Assign count all document no 
                this.docNoCountAlltextBox.Text = _searchByMuliDocList.Count.ToString("0.##");
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region ISearchMultiDataView Members


        public List<SearchByMultiDoc> SearchByMuliDocList
        {
            get
            {
                return _searchByMuliDocList;
            }
            set
            {
                _searchByMuliDocList = value;
            }
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_searchByMuliDocList.Count == 0)
            {
                MessageBox.Show("ไม่พบเอกสารที่ต้องการค้นหาข้อมูลหนี้", "ค้นหาข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SearchingMode();
            _presenter.ProcessStepByStep();
        }

        public void RefreshGridDataSource()
        {
            this.documentListDataGridView.Refresh();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // กรณีไม่มีข้อมูลหนี้
            if (InvoiceCount == 0 && !SearchDataFinished())
            {
                MessageBox.Show("กรุณาค้นหาข้อมูลหนี้", "ค้นหาข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Raise event back to user control ToBePaidBill.
            _presenter.InvoicesAddedToList();

            // Close form 
            this.ParentForm.Close();
        }

        private bool SearchDataFinished()
        {
            bool ret = false;

            // Check document list has research. 
            var notProcess = _searchByMuliDocList.Where(e => e.Status != string.Empty).ToList();
            if (notProcess.Count == SearchByMuliDocList.Count)
                ret = true;

            return ret;
        }

        public int SearchSuccessCount
        {
            get
            {
                return _searchSuccessCount;
            }
            set
            {
                _searchSuccessCount = value;
                successSearchDocTextBox.Text = _searchSuccessCount.ToString();
            }
        }

        public int SearchFailCount
        {
            get
            {
                return _searchFailCount;
            }
            set
            {
                _searchFailCount = value;
                this.failSearchDocCounttextBox.Text = _searchFailCount.ToString();
            }
        }

        public int InvoiceCount
        {
            get
            {
                return _invoiceCount;
            }
            set
            {
                _invoiceCount = value;
                invoiceCountTextBox.Text = _invoiceCount.ToString();
            }
        }

        public void initView()
        {
            // Check .Dat Setting 
            string pcMuliSearchString = CodeTable.Instant.GetAppSettingValue("PC_Multi_Search_Limit");
            int searchItemLimit = 0;

            if (int.TryParse(pcMuliSearchString, out searchItemLimit))
            {
                _searchLimitItem = searchItemLimit;
            }
            else
            {
                // Convert fail set limite to 400 , Set to property
                _searchLimitItem = 400;
            }

            // Set group box input text.
            this.inputDocGroupBox.Text = "";

            switch (SearchType.SearchTypeParam)
            {
                case "1":
                    inputDocGroupBox.Text = "กรอกหมายเลขผู้ใช้ไฟ";
                    docNoCountGroupBox.Text = "จำนวนหมายเลขผู้ใช้ไฟ";
                    colDocumentNo.HeaderText = "หมายเลขผู้ใช้ไฟ";
                    break;
                case "2":
                    inputDocGroupBox.Text = "กรอกเลขที่ มท.";
                    docNoCountGroupBox.Text = "จำนวนหมายเลขที่ มท.";
                    colDocumentNo.HeaderText = "เลขที่ มท.";

                    break;
                case "3":
                    inputDocGroupBox.Text = "กรอกเลขที่ใบคำร้อง";
                    docNoCountGroupBox.Text = "จำนวนหมายเลขที่ใบคำร้อง.";
                    colDocumentNo.HeaderText = "เลขที่ใบคำร้อง";
                    break;
                default:
                    break;
            }

            PrepareSearch();

            // Invoice list 
            if (InvoiceResultList == null)
                InvoiceResultList = new List<Invoice>();

            InvoiceResultList.Clear();

            if (_searchByMuliDocList == null)
                _searchByMuliDocList = new List<SearchByMultiDoc>();

            _searchByMuliDocList.Clear();

            successSearchDocTextBox.Text = string.Empty;
            failSearchDocCounttextBox.Text = string.Empty;
            docNoCountAlltextBox.Text = string.Empty;
            invoiceCountTextBox.Text = string.Empty;
            caIdInputtextBox.Text = string.Empty;

            //documentListDataGridView.DataSource = null;
            documentListDataGridView.Refresh();

            NormalMode();
        }

        #region ISearchMultiDataView Members


        public List<Invoice> InvoiceResultList
        {
            get
            {
                return _invoiceResultList;
            }
            set
            {
                _invoiceResultList = value;
            }
        }

        public void SearchingMode()
        {
            btnOK.Enabled = false;
            btnCancel.Enabled = false;
            btnSearch.Enabled = false;
            btnConvertStringToList.Enabled = false;

            btnCancelSearch.Enabled = true;
        }

        public void NormalMode()
        {
            btnOK.Enabled = true;
            btnCancel.Enabled = true;
            btnSearch.Enabled = true;
            btnConvertStringToList.Enabled = true;

            btnCancelSearch.Enabled = false;

            //ตรวจสอบ Invoince. ที่ค้นหาได้ถ้ารายการหนี้ = 0. จะไม่ให้กดปุ่ม OK. 
            if (this.InvoiceCount > 0)
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;

        }

        #endregion

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            // Set stop process.
            _presenter.CancelWorkAsync();
        }


        #region ISearchMultiDataView Members
        public void DisplaySuccuess()
        {
            MessageBox.Show("ค้นหาข้อมูลหนี้เสร็จเรียบร้อย", "ค้นหาข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region ISearchMultiDataView Members


        public void PrepareSearch()
        {
            // Reset for prepare search
            if (this.InvoiceResultList == null)
                this.InvoiceResultList = new List<Invoice>();

            if (this.InvoiceResultList.Count > 0)
                this.InvoiceResultList.Clear();

            //1. Data on griod
            if (_searchByMuliDocList == null)
                _searchByMuliDocList = new List<SearchByMultiDoc>();

            _searchByMuliDocList.ForEach(e =>
            {
                e.Status = string.Empty;
                e.InvoiceCount = 0;
                e.Result = string.Empty;
            });

            // Set view 
            successSearchDocTextBox.Text = string.Empty;
            failSearchDocCounttextBox.Text = string.Empty;
            invoiceCountTextBox.Text = string.Empty;

            // Reset count properties.
            this.SearchSuccessCount = 0;
            this.SearchFailCount = 0;
            this.InvoiceCount = 0;

            // Reset count number.
            this.successSearchDocTextBox.Text = string.Empty;
            this.failSearchDocCounttextBox.Text = string.Empty;
            this.invoiceCountTextBox.Text = string.Empty;
        }

        #endregion

        #region ISearchMultiDataView Members


        public SearchByMultiDocParam SearchType
        {
            get
            {
                return _searchType;
            }
            set
            {
                _searchType = value;
            }
        }

        #endregion

        #region ISearchMultiDataView Members


        public void DisplayCancelComplated()
        {
            MessageBox.Show("ยกเลิกการค้นหาข้อมูลเรียบร้อย", "ค้นหาข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            NormalMode();
        }

        #endregion

        private void clearInputButton_Click(object sender, EventArgs e)
        {
            this.caIdInputtextBox.Text = string.Empty;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void inputDocGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void caIdInputtextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnConvertStringToList.Enabled = true;
                string inputString = caIdInputtextBox.Text.Trim();
                if (inputString.Length == 0)
                {
                    // Set 0 rows count. 
                    this.inputTextRowCountTextBox.Text = "0";
                }
                else
                {
                    // Set rows count split row.
                    var spliteDatas = inputString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList(); ;
                    this.inputTextRowCountTextBox.Text = spliteDatas.Count.ToString();

                    if (spliteDatas.Count > this.SearchLimitItem)
                    {
                        MessageBox.Show("กรอกข้อมูลเกิน " + this.SearchLimitItem.ToString() + " รายการ", "กรอกข้อมูลค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnConvertStringToList.Enabled = false;
                        return;
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        #region ISearchMultiDataView Members


        public int SearchLimitItem
        {
            get
            {
                return _searchLimitItem;
            }
            set
            {
                _searchLimitItem = value;
            }
        }

        #endregion
    }
}
