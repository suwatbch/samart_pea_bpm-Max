using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.Collections;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Utilities
{
    public class CustomDataGridView : DataGridView
    {       
        private BillBookManagementView _parentControl;
        private System.Data.DataTable _dtValidation = new System.Data.DataTable("ValidationTable");
        private System.ComponentModel.IContainer _components = null;
        private string[] _gvRowFormat;
        private CustomDataGridView _childGridView;
        private CustomDataGridView _parentGridView;
        private bool _isParent;
        private BindingList<BillBookCreationInfo> _billBookItems;
        private BindingList<BillBookCreationExtraInfo> _extraItem_Exp;
        private BindingList<BillBookCreationExtraInfo> _extraItem_Plus;
        private BindingList<BillBookCreationExtraInfo> _extraItem_CExp;
        private BindingList<BillBookCreationExtraInfo> _extraItem_CPlus;
        private int _activeBookCreationIndex;
            
        private bool IsExistInputItem(string branchId, string mruId, int index)
        {
            for (int i=0; i< _billBookItems.Count; i++) 
            {
                if (i == index) continue; //ignore currernt row
                if (string.Equals(_billBookItems[i].PeaCode, branchId, StringComparison.CurrentCultureIgnoreCase) && _billBookItems[i].LineId == mruId)
                    return true;
            }

            return false;
        }

        private bool IsExistExtraItem(CustomDataGridView sender, string branchId, string mruId, string caId, int index)
        {
            BindingList<BillBookCreationExtraInfo> extraList = null;
            if (sender.IsParent)
                extraList = (BindingList<BillBookCreationExtraInfo>)sender.ChildGridView.DataSource;
            else
                extraList = (BindingList<BillBookCreationExtraInfo>)sender.DataSource;

            for (int i = 0; i < extraList.Count; i++)
            {
                if (i == index) continue; //ignore currernt row
                if (string.Equals(extraList[i].NPeaCode, branchId, StringComparison.CurrentCultureIgnoreCase) &&
                            extraList[i].NLineId == mruId && extraList[i].Number == caId)
                    return true;
            }

            return false;
        }

        //parent call only
        public void CleanupUnusedChild()
        {
            if (this.IsParent)
            {
                int two = 0; //no for one
                int three =0;
                int four = 0;
                int five = 0; 
                foreach (BillBookCreationInfo ci in _billBookItems)
                {                    
                    if (ci.CallingBill == "2") two++;
                    else if (ci.CallingBill == "3") three++;
                    else if (ci.CallingBill == "4") four++;
                    else if (ci.CallingBill == "5") five++;
                }

                if (_extraItem_Exp!= null && two == 0) _extraItem_Exp.Clear();
                if (_extraItem_Plus!= null && three == 0) _extraItem_Plus.Clear();
                if (_extraItem_CExp != null && four == 0) _extraItem_CExp.Clear();
                if (_extraItem_CPlus != null && five == 0) _extraItem_CPlus.Clear();
            }
        }

        public bool NewRow()
        {
            bool success = true;
            //find and delete previous default row
            foreach (BillBookCreationInfo b in _billBookItems)
            {
                if (string.Equals(b.PeaCode, Session.Branch.Id, StringComparison.CurrentCultureIgnoreCase) && b.LineId == "9999")
                {
                    success = false;
                    break;
                }
            }

            if (success)
            {
                BillBookCreationInfo cr = new BillBookCreationInfo();
                cr.PeaCode = Session.Branch.Id;
                cr.LineId = "9999";
                cr.BillPeriod = "1";
                cr.CallingBill = "1";
                _billBookItems.Add(cr);
            }

            return success;

        }

        public bool NewRow(string branchId)
        {
            bool success = true;
            //find and delete previous default row
            foreach (BillBookCreationInfo b in _billBookItems)
            {
                if (string.Equals(b.PeaCode, branchId, StringComparison.CurrentCultureIgnoreCase) && b.LineId == "9999")
                {
                    success = false;
                    break;
                }
            }

            if (success)
            {
                BillBookCreationInfo cr = new BillBookCreationInfo();
                cr.PeaCode = branchId;
                cr.LineId = "9999";
                cr.BillPeriod = "1";
                cr.CallingBill = "1";
                _billBookItems.Add(cr);
            }

            return success;
        }

        public bool NewChildRow()
        {
            BillBookCreationExtraInfo extra = new BillBookCreationExtraInfo();
            extra.NPeaCode = "------";
            extra.NLineId = "----";
            extra.Number = "000000000000";

            if (IsParent)
            {
                if (!IsExistExtraItem(this, extra.NPeaCode, extra.NLineId, extra.Number, -1)) //find all rows
                    ((BindingList<BillBookCreationExtraInfo>)_childGridView.DataSource).Add(extra);
                else
                    return false;
            }
            else
            {
                ((BindingList<BillBookCreationExtraInfo>)this.DataSource).Add(extra);
            }

            return true;
        }

        public BillBookManagementView ParentControl
        {
            set { _parentControl = value; }
        }

        public bool IsParent
        {
            set { _isParent = value; }
            get { return _isParent; }
        }

        public void RefreshCollumn()
        {
            this.Columns["Sequence"].DisplayIndex = 0;
            this.Columns["PeaCode"].DisplayIndex = 1;
            this.Columns["LineId"].DisplayIndex = 2;
            this.Columns["BillPeriodType"].DisplayIndex = 3;
            this.Columns["CollectionType"].DisplayIndex = 4;   
        }

        public void RefreashChildCollumn()
        {
            _childGridView.Columns["SeqNo"].DisplayIndex = 0;
            _childGridView.Columns["NPeaCode"].DisplayIndex = 1;
            _childGridView.Columns["NLineId"].DisplayIndex = 2;
            _childGridView.Columns["Number"].DisplayIndex = 3;
        }

        private bool CheckValidPeriodCollectionType(int periodType, int callingType)
        {
            bool ret = true;
            if( (periodType == 2 && (callingType == 4 || callingType == 5)) ||
                (periodType == 3 && (callingType == 4 || callingType == 5)))
            {
                MessageBox.Show(null, "ระบบไม่รองรับเงื่อนไขนี้ \nกรุณาป้อนบิลเดือนเท่ากับ 1 เพื่อกำหนดบิลออกเก็บ 4 หรือ 5", "ไม่รองรับ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }

            return ret;
        }

        public BindingList<BillBookCreationExtraInfo> ExtraItemExp
        {
            set
            {
                _extraItem_Exp = value;
            }
            get { return _extraItem_Exp; }
        }

        public BindingList<BillBookCreationExtraInfo> ExtraItemPlus
        {
            set
            {
                _extraItem_Plus = value;
            }
            get { return _extraItem_Plus; }
        }

        public BindingList<BillBookCreationExtraInfo> ExtraItemCExp
        {
            set
            {
                _extraItem_CExp = value;
            }
            get { return _extraItem_CExp; }
        }

        public BindingList<BillBookCreationExtraInfo> ExtraItemCPlus
        {
            set { _extraItem_CPlus = value;  }
            get { return _extraItem_CPlus; }
        }


        public BindingList<BillBookCreationInfo> BillBookItems
        {
            set {
                if (value == null) return;
                _billBookItems = value;
                _billBookItems.AllowEdit = true;
                _billBookItems.AllowNew = true;
                _billBookItems.AllowRemove = true;
                try
                {
                    this.DataSource = _billBookItems;
                    RefreshCollumn();
                }
                catch (Exception e)
                {

                }
            }
            get { return _billBookItems; }
        }       

        public int ActiveBookCreationRowIndex
        {
            set { _activeBookCreationIndex = value; }
            get { return _activeBookCreationIndex; }
        }

        public string[] GridViewRowFormat
        {
            set { _gvRowFormat = value; }
            get { return _gvRowFormat; }
        }

        public CustomDataGridView ParentGridView
        {
            set { _parentGridView = value; }
        }

        public CustomDataGridView ChildGridView
        {
            set { _childGridView = value; }
            get { return _childGridView; }
        }

        public enum ValidationStyle
        {
            NumericInt, NumericDouble, Date
        }

        private void InitContainers()
        {
        }

        public CustomDataGridView()
        {
            _components = new System.ComponentModel.Container();
            InitContainers();
        }

        public CustomDataGridView(IContainer container)
        {
            container.Add(this);
            _components = new System.ComponentModel.Container();
            InitContainers();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }


        [System.Security.Permissions.UIPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Extract the key code from the key value. 
                        
            Keys key = (keyData & Keys.KeyCode);

            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (key == Keys.Enter)
            {
                return this.ProcessRightKey(keyData);
            }
            else if (key.ToString() == "Tab")
            {
                return false;
            }
            else if (key == Keys.Insert)
            {
                if (!IsParent) //extra gv
                {
                    _parentGridView.NewRow();
                    //MessageBox.Show(null, "การไฟฟ้าหรือสายการเก็บเงินที่ป้อนซ้ำ  ", "ป้อนค่าซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _parentGridView.CurrentCell = _parentGridView.Rows[_parentGridView.Rows.Count - 1].Cells["PeaCode"];
                    _parentGridView.BeginEdit(true);
                    return true;
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        public new bool ProcessRightKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if ((key == Keys.Enter) && (base.CurrentCell != null))
            {
                //add new row and move cursor to the begining of new row's cell
                if (base.CurrentCell.OwningColumn.Name == "CollectionType")
                {
                    base.EndEdit();
                    this.ActiveBookCreationRowIndex = base.CurrentRow.Index; //point at new row
                    int periodType = Convert.ToInt32(base.CurrentRow.Cells["BillPeriodType"].Value);
                    string cBranchId = base.CurrentRow.Cells["PeaCode"].Value.ToString();
                    string cMruId = base.CurrentRow.Cells["LineId"].Value.ToString();
                    int lastCellValue;

                    if (!IsNumericInt(base.CurrentCell.Value))
                    {
                        lastCellValue = Convert.ToInt32(base.CurrentCell.Value);
                    }
                    else
                    {
                        MessageBox.Show(null, "ค่าที่ป้อนเกินขอบเขต ค่าที่รับได้ [1-5]  ", "ป้อนค่าผิด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }

                    if (IsExistInputItem(cBranchId, cMruId, base.CurrentRow.Index)) // not allow duplicate
                    {
                        MessageBox.Show(null, "การไฟฟ้าหรือสายการเก็บเงินที่ป้อนซ้ำ  ", "ป้อนค่าซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        base.CurrentCell = base.CurrentRow.Cells["PeaCode"];
                        base.BeginEdit(true);
                        return true;
                    }

                    if (!CheckValidPeriodCollectionType(periodType, lastCellValue))
                        return true;

                    //check if any calling bill type is not longer existed so remove its child
                    CleanupUnusedChild();

                    if (lastCellValue != 1)  //1 is all bills
                    {
                        //handle case 2->3
                        if (lastCellValue < 1 || lastCellValue > 5)
                        {
                            MessageBox.Show(null, "ค่าที่ป้อนเกินขอบเขต ค่าที่รับได้ [1-5]  ", "ป้อนค่าผิด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return true;
                        }

                        if (lastCellValue == 2)
                            _childGridView.DataSource = _extraItem_Exp;

                        if (lastCellValue == 3)
                            _childGridView.DataSource = _extraItem_Plus;

                        if (lastCellValue == 4)
                            _childGridView.DataSource = _extraItem_CExp;

                        if (lastCellValue == 5)
                            _childGridView.DataSource = _extraItem_CPlus;

                        _parentControl.ShowBillInputStatus(lastCellValue);

                        if (_childGridView != null)  //assum this is a parent
                        {
                            NewChildRow();
                            _childGridView.Rows[_childGridView.RowCount - 1].Cells["NPeaCode"].ReadOnly = true;
                            _childGridView.Rows[_childGridView.RowCount - 1].Cells["NLineId"].ReadOnly = true;
                            //start edit at customer number
                            _childGridView.CurrentCell = _childGridView.Rows[_childGridView.RowCount - 1].Cells["Number"];
                            _childGridView.BeginEdit(true);
                            RefreashChildCollumn();
                            return true;
                        }
                    }

                    if (base.CurrentCell.RowIndex == (base.RowCount - 1)) //last row
                    {
                        bool success = false;
                        if (base.RowCount != 0)
                            success = NewRow(cBranchId);
                        else
                            success = NewRow();

                        if (!success)
                        {
                            MessageBox.Show(null, "การไฟฟ้าหรือสายการเก็บเงินที่ป้อนซ้ำ/ค่าเริ่มต้น  ", "ป้อนค่าซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            base.CurrentCell = base.CurrentRow.Cells["PeaCode"];
                            base.BeginEdit(true);
                            return true;
                        }

                        base.CurrentCell = base.Rows[base.RowCount - 1].Cells["PeaCode"];
                        base.BeginEdit(true);
                    }
                    else  //edit row by changing back from (2-5) to 1  - remove child list
                    {
                        base.CurrentCell = base.Rows[base.CurrentRow.Index + 1].Cells["PeaCode"]; //next row
                    }
                    
                    return true;
                }
                else if (base.CurrentCell.OwningColumn.Name == "BillPeriodType")
                {
                    base.EndEdit();
                    if (!IsNumericInt(base.CurrentCell.Value)) 
                    {
                        int lastCellValue = Convert.ToInt32(base.CurrentCell.Value);
                        if (lastCellValue < 1 || lastCellValue > 3)
                        {
                            MessageBox.Show(null, "ค่าที่ป้อนเกินขอบเขต ค่าที่รับได้ [1-3]  ", "ป้อนค่าผิด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(null, "ค่าที่ป้อนเกินขอบเขต ค่าที่รับได้ [1-3]  ", "ป้อนค่าผิด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
                else if (base.CurrentCell.OwningColumn.Name == "Number")
                {
                    //save previous row 
                    base.EndEdit();

                    //verify and validate cell's value
                    string branchId = base.CurrentRow.Cells["NPeaCode"].Value.ToString();
                    string mruId = base.CurrentRow.Cells["NLineId"].Value.ToString();
                    string caId = null;
                    bool valid = _parentControl.ValidateInput((string)base.CurrentCell.Value, ref caId, ref branchId, ref mruId);
                    if (!valid)
                    {
                        base.CurrentCell.Value = "";
                        base.BeginEdit(true);
                        return true;
                    }
                    else
                        base.CurrentCell.Value = caId;

                    //basic way, use CaId to fine branchId and mruId
                    string[] ret = _parentControl.FindBranchMruOfCA(caId);
                    if (ret != null && ret.Length != 0 && ret.Length >= 2)
                    {
                        branchId = ret[0];
                        mruId = ret[1];
                        base.CurrentRow.Cells["NPeaCode"].Value = branchId;
                        base.CurrentRow.Cells["NLineId"].Value = mruId;
                    }
                    else
                    {
                        MessageBox.Show(null, "ป้อนข้อมูลรหัสผู้ใช้ไฟฟ้าไม่ถูกต้อง", "ป้อนผิด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        base.BeginEdit(true);
                        return true;
                    }

                    //check duplicated
                    bool duplicated = _parentControl.IsDuplicatedCa(caId, base.CurrentRow.Index);
                    if (duplicated)
                    {
                        MessageBox.Show(null, "ป้อนข้อมูลรหัสผู้ใช้ไฟฟ้าซ้ำในรายการ", "ป้อนซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        base.BeginEdit(true);
                        return true;
                    }

                    if ((base.CurrentCell.RowIndex == (base.RowCount - 1)))
                    {
                        NewChildRow();
                        base.Rows[base.RowCount - 1].Cells["NPeaCode"].ReadOnly = true;
                        base.Rows[base.RowCount - 1].Cells["NLineId"].ReadOnly = true;
                        base.CurrentCell = base.Rows[base.RowCount - 1].Cells["Number"];
                    }
                    else //shift to next row
                    {
                        base.CurrentCell = base.Rows[base.CurrentRow.Index + 1].Cells["NPeaCode"];
                    }

                    base.BeginEdit(true);
                    return true;
                }
                else if (base.CurrentCell.OwningColumn.Name == "LineId")
                {
                    base.EndEdit();
                    string branchId = base.CurrentRow.Cells["PeaCode"].Value.ToString().Trim();
                    base.CurrentCell.Value = base.CurrentCell.Value.ToString().Trim().PadLeft(ModuleConfigurationNames.MRUCodeLength, '0');
                    string lineId = base.CurrentCell.Value.ToString().Trim();
                    if (!_parentControl.IsLineAssignedToAgent(branchId, lineId))
                    {
                        base.BeginEdit(true);
                        return true;
                    }
                }
                else if (base.CurrentCell.OwningColumn.Name == "PeaCode") // valid is a must
                {
                    base.EndEdit();
                    string branchId = base.CurrentCell.Value.ToString().Trim();
                    if (!_parentControl.IsValidBranch(branchId))
                    {
                        base.BeginEdit(true);
                        return true;
                    }
                }
            }

            return base.ProcessRightKey(keyData);

        }

        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand, Flags =
            System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (e.KeyCode == Keys.Enter)
            {
                return this.ProcessRightKey(e.KeyData);
            }
            return base.ProcessDataGridViewKey(e);
        }

        #region AddValidation
        public void AddValidation(string ColumnName, bool BlankControl, ValidationStyle ValidationType, string ValidationMessage)
        {
            if (_dtValidation.Columns.Count == 0)
            {
                _dtValidation.Columns.Add("Name", Type.GetType("System.String"));
                _dtValidation.Columns.Add("BlankControl", Type.GetType("System.Boolean"));
                _dtValidation.Columns.Add("Validation", Type.GetType("System.Int16"));
                // ValidationType

                // dtValidation.Columns.Add("Validation", Type.GetType(ValidationType));
                _dtValidation.Columns.Add("ValidationMessage", Type.GetType("System.String"));
            }

            System.Data.DataRow dr;
            dr = _dtValidation.NewRow();
            dr[0] = ColumnName;
            dr[1] = BlankControl;
            dr[2] = ValidationType;
            dr[3] = ValidationMessage;
            _dtValidation.Rows.Add(dr);

        }
        #endregion

        #region AddMask
        /*
        public void AddColumnMask(string ColumnName, string HeaderText, string MaskStr, int columnIndex)
        {
            try
            {
                Columns.Remove(ColumnName);
                DataGridViewMaskedTextBoxColumn mc = new DataGridViewMaskedTextBoxColumn();
                mc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                mc.DisplayIndex = columnIndex;
                mc.DataPropertyName = ColumnName;
                mc.Name = ColumnName;
                mc.HeaderText = HeaderText;
                mc.Mask = MaskStr;
                Columns.Add(mc);
            }
            catch (Exception e)
            {
                throw;
            }
        }
         * */
        #endregion

        #region Events
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"></see> that contains the event data.</param>
        protected override void OnCellBeginEdit(System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            CurrentRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
            base.OnCellBeginEdit(e);
        }


        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"></see> that contains the event data.</param>
        protected override void OnRowValidated(System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (CurrentRow == null) return;
            if (CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.LightYellow)
            {
                CurrentRow.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                base.OnRowValidated(e);
            }
        }


        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"></see> that contains the event data.</param>
        protected override void OnCellEndEdit(System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            this.Rows[e.RowIndex].ErrorText = String.Empty;
            base.OnCellEndEdit(e);
        }

        /*
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValidatingEventArgs"></see> that contains the event data.</param>
        protected override void OnCellValidating(System.Windows.Forms.DataGridViewCellValidatingEventArgs e)
        {
            int i, j;
            j = dtValidation.Rows.Count - 1;
            if (j != -1)
            {

                i = 0;
                bool check = false;
                System.Data.DataView dv = new System.Data.DataView(dtValidation, "Name='" + this.Columns[e.ColumnIndex].Name + "'", null, System.Data.DataViewRowState.CurrentRows);
                if (dv.Count == 1)
                {
                    if ((bool)dv[0][1])
                    {
                        if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                        {
                            this.Rows[e.RowIndex].ErrorText = "Empty Text";
                            e.Cancel = true;
                        }
                    }
                    if ((dv[0][2].ToString()) != "")
                    {
                        switch (dv[0][2].ToString())
                        {
                            case "0":
                                check = IsNumericInt(e.FormattedValue.ToString());
                                break;
                            case "1":
                                check = IsNumericDouble(e.FormattedValue.ToString());
                                break;
                            case "2":
                                check = IsDate(e.FormattedValue.ToString());
                                break;
                        }
                        if (check)
                        {
                            this.Rows[e.RowIndex].ErrorText = dv[0][3].ToString();
                            e.Cancel = check;
                        }
                    }

                }
            }
            base.OnCellValidating(e);
        }
         * */

        #endregion

        #region ValidationControls
        private bool IsNumericDouble(object ValueToCheck)
        {
            double Dummy = 0;
            return !double.TryParse(ValueToCheck.ToString(), System.Globalization.NumberStyles.Any, null, out Dummy);
        }

        private bool IsNumericInt(object ValueToCheck)
        {
            int Dummy = 0;
            return !int.TryParse(ValueToCheck.ToString(), System.Globalization.NumberStyles.Any, null, out Dummy);
        }

        private bool IsDate(object ValueToCheck)
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(ValueToCheck.ToString(), @"^[0-2]?[1-9](/|-|.)[0-3]?[0-9](/|-|.)[1-2][0-9][0-9][0-9]$");

        }


        #endregion

        class MaskedTextBoxCell : DataGridViewTextBoxCell
        {
            private string mask;            
            private char promptChar;
            private DataGridViewTriState includePrompt;
            private DataGridViewTriState includeLiterals;
            private Type validatingType;

            //=------------------------------------------------------------------=
            // MaskedTextBoxCell
            //=------------------------------------------------------------------=
            /// <summary>
            ///   Initializes a new instance of this class.  Fortunately, there's
            ///   not much to do here except make sure that our base class is 
            ///   also initialized properly.
            /// </summary>
            /// 
            public MaskedTextBoxCell()
                : base()
            {
                this.mask = "";
                this.promptChar = '_';
                this.includePrompt = DataGridViewTriState.NotSet;
                this.includeLiterals = DataGridViewTriState.NotSet;
                this.validatingType = typeof(string);
                            }

            ///   Whenever the user is to begin editing a cell of this type, the editing
            ///   control must be created, which in this column type's
            ///   case is a subclass of the MaskedTextBox control.
            /// 
            ///   This routine sets up all the properties and values
            ///   on this control before the editing begins.
            public override void InitializeEditingControl(int rowIndex,
                                                          object initialFormattedValue,
                                                          DataGridViewCellStyle dataGridViewCellStyle)
            {
                MaskedTextBoxEditingControl mtbec;
                DataGridViewMaskedTextBoxColumn mtbcol;
                DataGridViewColumn dgvc;

                base.InitializeEditingControl(rowIndex, initialFormattedValue,
                                              dataGridViewCellStyle);

                mtbec = DataGridView.EditingControl as MaskedTextBoxEditingControl;

                //
                // set up props that are specific to the MaskedTextBox
                //

                dgvc = this.OwningColumn;   // this.DataGridView.Columns[this.ColumnIndex];
                if (dgvc is DataGridViewMaskedTextBoxColumn)
                {
                    mtbcol = dgvc as DataGridViewMaskedTextBoxColumn;

                    //
                    // get the mask from this instance or the parent column.
                    //
                    if (string.IsNullOrEmpty(this.mask))
                    {
                        mtbec.Mask = mtbcol.Mask;
                    }
                    else
                    {
                        mtbec.Mask = this.mask;
                    }

                    //
                    // prompt char.
                    //
                    mtbec.PromptChar = this.PromptChar;

                    //
                    // IncludePrompt
                    //
                    if (this.includePrompt == DataGridViewTriState.NotSet)
                    {
                        //mtbec.IncludePrompt = mtbcol.IncludePrompt;
                    }
                    else
                    {
                        //mtbec.IncludePrompt = BoolFromTri(this.includePrompt);
                    }

                    //
                    // IncludeLiterals
                    //
                    if (this.includeLiterals == DataGridViewTriState.NotSet)
                    {
                        //mtbec.IncludeLiterals = mtbcol.IncludeLiterals;
                    }
                    else
                    {
                        //mtbec.IncludeLiterals = BoolFromTri(this.includeLiterals);
                    }

                    //
                    // Finally, the validating type ...
                    //
                    if (this.ValidatingType == null)
                    {
                        mtbec.ValidatingType = mtbcol.ValidatingType;
                    }
                    else
                    {
                        mtbec.ValidatingType = this.ValidatingType;
                    }
                    if (this.Value == DBNull.Value)
                    {
                        mtbec.Text = string.Empty;
                    }
                    else
                    {
                        mtbec.Text = (string)this.Value;
                    }
                }
            }

            //  Returns the type of the control that will be used for editing
            //  cells of this type.  This control must be a valid Windows Forms
            //  control and must implement IDataGridViewEditingControl.
            public override Type EditType
            {
                get
                {
                    return typeof(MaskedTextBoxEditingControl);
                }
            }

            //   A string value containing the Mask against input for cells of
            //   this type will be verified.
            public virtual string Mask
            {
                get
                {
                    return this.mask;
                }
                set
                {
                    this.mask = value;
                }
            }

            //  The character to use for prompting for new input.
            // 
            public virtual char PromptChar
            {
                get
                {
                    return this.promptChar;
                }
                set
                {
                    this.promptChar = value;
                }
            }


            //  A boolean indicating whether to include prompt characters in
            //  the Text property's value.
            public virtual DataGridViewTriState IncludePrompt
            {
                get
                {
                    return this.includePrompt;
                }
                set
                {
                    this.includePrompt = value;
                }
            }

            //  A boolean value indicating whether to include literal characters
            //  in the Text property's output value.
            public virtual DataGridViewTriState IncludeLiterals
            {
                get
                {
                    return this.includeLiterals;
                }
                set
                {
                    this.includeLiterals = value;
                }
            }

            //  A Type object for the validating type.
            public virtual Type ValidatingType
            {
                get
                {
                    return this.validatingType;
                }
                set
                {
                    this.validatingType = value;
                }
            }

            //   Quick routine to convert from DataGridViewTriState to boolean.
            //   True goes to true while False and NotSet go to false.
            protected static bool BoolFromTri(DataGridViewTriState tri)
            {
                return (tri == DataGridViewTriState.True) ? true : false;
            }
        }

        public class DataGridViewMaskedTextBoxColumn : DataGridViewColumn
        {
            private string mask;
            private char promptChar;
            private bool includePrompt;
            private bool includeLiterals;
            private Type validatingType;

            //  Initializes a new instance of this class, making sure to pass
            //  to its base constructor an instance of a MaskedTextBoxCell 
            //  class to use as the basic template.
            public DataGridViewMaskedTextBoxColumn()
                : base(new MaskedTextBoxCell())
            {
            }

            //  Routine to convert from boolean to DataGridViewTriState.
            private static DataGridViewTriState TriBool(bool value)
            {
                return value ? DataGridViewTriState.True
                             : DataGridViewTriState.False;
            }

            //  The template cell that will be used for this column by default,
            //  unless a specific cell is set for a particular row.
            //
            //  A MaskedTextBoxCell cell which will serve as the template cell
            //  for this column.
            public override DataGridViewCell CellTemplate
            {
                get
                {
                    return base.CellTemplate;
                }

                set
                {
                    //  Only cell types that derive from MaskedTextBoxCell are supported as the cell template.
                    if (value != null && !value.GetType().IsAssignableFrom(typeof(MaskedTextBoxCell)))
                    {
                        string s = "Cell type is not based upon the MaskedTextBoxCell.";//CustomColumnMain.GetResourceManager().GetString("excNotMaskedTextBox");
                        throw new InvalidCastException(s);
                    }

                    base.CellTemplate = value;
                }
            }

            //  Indicates the Mask property that is used on the MaskedTextBox
            //  for entering new data into cells of this type.
            // 
            //  See the MaskedTextBox control documentation for more details.
            public virtual string Mask
            {
                get
                {
                    return this.mask;
                }
                set
                {
                    MaskedTextBoxCell mtbc;
                    DataGridViewCell dgvc;
                    int rowCount;

                    if (this.mask != value)
                    {
                        this.mask = value;

                        //
                        // first, update the value on the template cell.
                        //
                        mtbc = (MaskedTextBoxCell)this.CellTemplate;
                        mtbc.Mask = value;

                        //
                        // now set it on all cells in other rows as well.
                        //
                        if (this.DataGridView != null && this.DataGridView.Rows != null)
                        {
                            rowCount = this.DataGridView.Rows.Count;
                            for (int x = 0; x < rowCount; x++)
                            {
                                dgvc = this.DataGridView.Rows.SharedRow(x).Cells[x];
                                if (dgvc is MaskedTextBoxCell)
                                {
                                    mtbc = (MaskedTextBoxCell)dgvc;
                                    mtbc.Mask = value;
                                }
                            }
                        }
                    }
                }
            }


            //  By default, the MaskedTextBox uses the underscore (_) character
            //  to prompt for required characters.  This propertly lets you 
            //  choose a different one.
            // 
            //  See the MaskedTextBox control documentation for more details.
            public virtual char PromptChar
            {
                get
                {
                    return this.promptChar;
                }
                set
                {
                    MaskedTextBoxCell mtbc;
                    DataGridViewCell dgvc;
                    int rowCount;

                    if (this.promptChar != value)
                    {
                        this.promptChar = value;

                        //
                        // first, update the value on the template cell.
                        //
                        mtbc = (MaskedTextBoxCell)this.CellTemplate;
                        mtbc.PromptChar = value;

                        //
                        // now set it on all cells in other rows as well.
                        //
                        if (this.DataGridView != null && this.DataGridView.Rows != null)
                        {
                            rowCount = this.DataGridView.Rows.Count;
                            for (int x = 0; x < rowCount; x++)
                            {
                                dgvc = this.DataGridView.Rows.SharedRow(x).Cells[x];
                                if (dgvc is MaskedTextBoxCell)
                                {
                                    mtbc = (MaskedTextBoxCell)dgvc;
                                    mtbc.PromptChar = value;
                                }
                            }
                        }
                    }
                }
            }

            //   Indicates whether any unfilled characters in the mask should be
            //   be included as prompt characters when somebody asks for the text
            //   of the MaskedTextBox for a particular cell programmatically.
            // 
            //   See the MaskedTextBox control documentation for more details.
            public virtual bool IncludePrompt
            {
                get
                {
                    return this.includePrompt;
                }
                set
                {
                    MaskedTextBoxCell mtbc;
                    DataGridViewCell dgvc;
                    int rowCount;

                    if (this.includePrompt != value)
                    {
                        this.includePrompt = value;

                        //
                        // first, update the value on the template cell.
                        //
                        mtbc = (MaskedTextBoxCell)this.CellTemplate;
                        mtbc.IncludePrompt = TriBool(value);

                        //
                        // now set it on all cells in other rows as well.
                        //
                        if (this.DataGridView != null && this.DataGridView.Rows != null)
                        {
                            rowCount = this.DataGridView.Rows.Count;
                            for (int x = 0; x < rowCount; x++)
                            {
                                dgvc = this.DataGridView.Rows.SharedRow(x).Cells[x];
                                if (dgvc is MaskedTextBoxCell)
                                {
                                    mtbc = (MaskedTextBoxCell)dgvc;
                                    mtbc.IncludePrompt = TriBool(value);
                                }
                            }
                        }
                    }
                }
            }

            //  Controls whether or not literal (non-prompt) characters should
            //  be included in the output of the Text property for newly entered
            //  data in a cell of this type.
            // 
            //  See the MaskedTextBox control documentation for more details.
            public virtual bool IncludeLiterals
            {
                get
                {
                    return this.includeLiterals;
                }
                set
                {
                    MaskedTextBoxCell mtbc;
                    DataGridViewCell dgvc;
                    int rowCount;

                    if (this.includeLiterals != value)
                    {
                        this.includeLiterals = value;

                        //
                        // first, update the value on the template cell.
                        //
                        mtbc = (MaskedTextBoxCell)this.CellTemplate;
                        mtbc.IncludeLiterals = TriBool(value);

                        //
                        // now set it on all cells in other rows as well.
                        //
                        if (this.DataGridView != null && this.DataGridView.Rows != null)
                        {

                            rowCount = this.DataGridView.Rows.Count;
                            for (int x = 0; x < rowCount; x++)
                            {
                                dgvc = this.DataGridView.Rows.SharedRow(x).Cells[x];
                                if (dgvc is MaskedTextBoxCell)
                                {
                                    mtbc = (MaskedTextBoxCell)dgvc;
                                    mtbc.IncludeLiterals = TriBool(value);
                                }
                            }
                        }
                    }
                }
            }

            //  Indicates the type against any data entered in the MaskedTextBox
            //  should be validated.  The MaskedTextBox control will attempt to
            //  instantiate this type and assign the value from the contents of
            //  the text box.  An error will occur if it fails to assign to this
            //  type.
            //
            //  See the MaskedTextBox control documentation for more details.
            public virtual Type ValidatingType
            {
                get
                {
                    return this.validatingType;
                }
                set
                {
                    MaskedTextBoxCell mtbc;
                    DataGridViewCell dgvc;
                    int rowCount;

                    if (this.validatingType != value)
                    {
                        this.validatingType = value;

                        //
                        // first, update the value on the template cell.
                        //
                        mtbc = (MaskedTextBoxCell)this.CellTemplate;
                        mtbc.ValidatingType = value;

                        //
                        // now set it on all cells in other rows as well.
                        //
                        if (this.DataGridView != null && this.DataGridView.Rows != null)
                        {
                            rowCount = this.DataGridView.Rows.Count;
                            for (int x = 0; x < rowCount; x++)
                            {
                                dgvc = this.DataGridView.Rows.SharedRow(x).Cells[x];
                                if (dgvc is MaskedTextBoxCell)
                                {
                                    mtbc = (MaskedTextBoxCell)dgvc;
                                    mtbc.ValidatingType = value;
                                }
                            }
                        }
                    }
                }
            }

        }


        public class MaskedTextBoxEditingControl : MaskedTextBox, IDataGridViewEditingControl
        {
            protected int rowIndex;
            protected DataGridView dataGridView;
            protected bool valueChanged = false;

            public MaskedTextBoxEditingControl()
            {

            }

            protected override void OnTextChanged(EventArgs e)
            {
                base.OnTextChanged(e);
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }

            //  Notify DataGridView that the value has changed.
            protected virtual void NotifyDataGridViewOfValueChange()
            {
                this.valueChanged = true;
                if (this.dataGridView != null)
                {
                    this.dataGridView.NotifyCurrentCellDirty(true);
                }
            }

            #region IDataGridViewEditingControl Members

            //  Indicates the cursor that should be shown when the user hovers their
            //  mouse over this cell when the editing control is shown.
            public Cursor EditingPanelCursor
            {
                get
                {
                    return Cursors.IBeam;
                }
            }


            //  Returns or sets the parent DataGridView.
            public DataGridView EditingControlDataGridView
            {
                get
                {
                    return this.dataGridView;
                }

                set
                {
                    this.dataGridView = value;
                }
            }


            //  Sets/Gets the formatted value contents of this cell.
            public object EditingControlFormattedValue
            {
                set
                {
                    this.Text = value.ToString();
                    NotifyDataGridViewOfValueChange();
                }
                get
                {
                    return this.Text;
                }

            }

            //   Get the value of the editing control for formatting.
            public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
            {
                return this.Text;
            }

            //  Process input key and determine if the key should be used for the editing control
            //  or allowed to be processed by the grid. Handle cursor movement keys for the MaskedTextBox
            //  control; otherwise if the DataGridView doesn't want the input key then let the editing control handle it.
            public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
            {
                switch (keyData & Keys.KeyCode)
                {
                    case Keys.Right:
                        //
                        // If the end of the selection is at the end of the string
                        // let the DataGridView treat the key message
                        //
                        if (!(this.SelectionLength == 0
                              && this.SelectionStart == this.ToString().Length))
                        {
                            return true;
                        }
                        break;

                    case Keys.Left:
                        //
                        // If the end of the selection is at the begining of the
                        // string or if the entire text is selected send this character 
                        // to the dataGridView; else process the key event.
                        //
                        if (!(this.SelectionLength == 0
                              && this.SelectionStart == 0))
                        {
                            return true;
                        }
                        break;

                    case Keys.Home:
                    case Keys.End:
                        if (this.SelectionLength != this.ToString().Length)
                        {
                            return true;
                        }
                        break;

                    case Keys.Prior:
                    case Keys.Next:
                        if (this.valueChanged)
                        {
                            return true;
                        }
                        break;

                    case Keys.Delete:
                        if (this.SelectionLength > 0 || this.SelectionStart < this.ToString().Length)
                        {
                            return true;
                        }
                        break;
                }

                //
                // defer to the DataGridView and see if it wants it.
                //
                return !dataGridViewWantsInputKey;
            }


            //  Prepare the editing control for edit.
            public void PrepareEditingControlForEdit(bool selectAll)
            {
                if (selectAll)
                {
                    SelectAll();
                }
                else
                {
                    //
                    // Do not select all the text, but position the caret at the 
                    // end of the text.
                    //
                    this.SelectionStart = this.ToString().Length;
                }
            }

            //  Indicates whether or not the parent DataGridView control should
            //  reposition the editing control every time value change is indicated.
            //  There is no need to do this for the MaskedTextBox.
            public bool RepositionEditingControlOnValueChange
            {
                get
                {
                    return false;
                }
            }


            //  Indicates the row index of this cell.  This is often -1 for the
            //  template cell, but for other cells, might actually have a value
            //  greater than or equal to zero.
            public int EditingControlRowIndex
            {
                get
                {
                    return this.rowIndex;
                }

                set
                {
                    this.rowIndex = value;
                }
            }



            //  Make the MaskedTextBox control match the style and colors of
            //  the host DataGridView control and other editing controls 
            //  before showing the editing control.
            public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
            {
                this.Font = dataGridViewCellStyle.Font;
                this.ForeColor = dataGridViewCellStyle.ForeColor;
                this.BackColor = dataGridViewCellStyle.BackColor;
                this.TextAlign = translateAlignment(dataGridViewCellStyle.Alignment);
            }


            //  Gets or sets our flag indicating whether the value has changed.
            public bool EditingControlValueChanged
            {
                get
                {
                    return valueChanged;
                }

                set
                {
                    this.valueChanged = value;
                }
            }

            #endregion // IDataGridViewEditingControl.

            ///   Routine to translate between DataGridView
            ///   content alignments and text box horizontal alignments.
            private static HorizontalAlignment translateAlignment(DataGridViewContentAlignment align)
            {
                switch (align)
                {
                    case DataGridViewContentAlignment.TopLeft:
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.BottomLeft:
                        return HorizontalAlignment.Left;

                    case DataGridViewContentAlignment.TopCenter:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.BottomCenter:
                        return HorizontalAlignment.Center;

                    case DataGridViewContentAlignment.TopRight:
                    case DataGridViewContentAlignment.MiddleRight:
                    case DataGridViewContentAlignment.BottomRight:
                        return HorizontalAlignment.Right;
                }

                throw new ArgumentException("Error: Invalid Content Alignment!");
            }


        }
    }
}
