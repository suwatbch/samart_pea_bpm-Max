//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using System.ComponentModel;
using PEA.BPM.ToolModule.Interface.BusinessEntities;

namespace PEA.BPM.ToolModule
{
    [SmartPart]
    public partial class RemarkListView : UserControl, IRemarkListView
    {
        BindingList<UnlockRemark> _unlockRemark;

        public Function SetFunction
        {
            set
            {
                fncIdTextBox.Text = value.FunctionId;
                fncDescTextBox.Text = value.Description;
            }
        }

        public BindingList<UnlockRemark> RemarkList
        {
            set
            {
                remarkDataGridView.AutoGenerateColumns = false;
                remarkDataGridView.DataSource = value;

                if (remarkDataGridView.Rows.Count > 1)
                    remarkDataGridView.CurrentCell = remarkDataGridView.Rows[0].Cells[0];
            }
        }

        public RemarkListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public RemarkListViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
        }

        public void RefreshRemarkList()
        {
            Function fnc = new Function();
            fnc.FunctionId = fncIdTextBox.Text;
            fnc.Description = fncDescTextBox.Text;

            _presenter.RefreshRemarkListView(fnc);
        }

        private void addRemarkButton_Click(object sender, EventArgs e)
        {
            try
            {
                _presenter.ShowCreateRemarkDialog(fncIdTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ͼԴ��Ҵ㹡������ remark : " + ex.ToString(), "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editRemarkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (remarkDataGridView.SelectedRows.Count > 0)
                {
                    UnlockRemark remark = (UnlockRemark)remarkDataGridView.SelectedRows[0].DataBoundItem;
                    _presenter.ShowEditRemarkDialog(remark);
                }
                else
                {
                    MessageBox.Show("��辺������ Remark", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ͼԴ��Ҵ㹡����� remark : " + ex.ToString(), "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteRemarkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsAnyRowChecked())
                {
                    DialogResult dlg = MessageBox.Show(null, "�س���ѧ��ź������ Remark", "��س��׹�ѹ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dlg == DialogResult.Yes)
                    {
                        _presenter.Delete(_unlockRemark);
                    }
                }
                else
                {
                    MessageBox.Show(null, "��س����͡��¡�ü�������ͧ���ź", "���͡��¡��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ͼԴ��Ҵ㹡��ź remark : " + ex.ToString(), "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsAnyRowChecked()
        {
            _unlockRemark = new BindingList<UnlockRemark>();

            for (int i = 0; i < remarkDataGridView.Rows.Count; i++)
            {
                if ((bool)remarkDataGridView.Rows[i].Cells["GCheck"].FormattedValue)
                {
                    UnlockRemark rmk = new UnlockRemark();
                    rmk = (UnlockRemark)remarkDataGridView.Rows[i].DataBoundItem;
                    _unlockRemark.Add(rmk);
                }
            }

            return _unlockRemark.Count > 0 ? true : false;
        }
    }
}

