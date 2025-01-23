using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public partial class ConfirmBankDelivering : Form
    {
        private string _cashAmt;
        private decimal _chqSameBranch;
        private decimal _chqSameBank;
        private decimal _chqAnoBank;
        private string _totalAmt;
        private int _sameBranch;
        private int _sameBank;
        private int _otherBank;
        private bool _sepCheque = false;

        public ConfirmBankDelivering(string cashAmt, decimal chqSameBranch, decimal chqSameBank, decimal chqAnoBank, string totalAmt,
                                int sameBranch, int sameBank, int otherBank)
        {
            _cashAmt = cashAmt;
            _chqSameBranch = chqSameBranch;
            _chqSameBank = chqSameBank;
            _chqAnoBank = chqAnoBank;
            _totalAmt = totalAmt;
            _sameBranch = sameBranch;
            _sameBank = sameBank;
            _otherBank = otherBank;

            InitializeComponent();
            InitLabel();

            if (Session.Branch.Id == "Z00000")
                separatedPayInCheck.Checked = false;
            else
                separatedPayInCheck.Checked = true;
        }

        public bool SeparatedCheque
        {
            get { return _sepCheque; }
        }

        private void InitLabel()
        {
            cashAmtTxt.Text = _cashAmt;

            if ((_chqSameBranch + _chqSameBank + _chqAnoBank) <= 0)
                separatedPayInCheck.Enabled = false;

            if (_chqSameBranch == 0)
                chqSameBranchTxt.Text = "0.00";
            else
                chqSameBranchTxt.Text = _chqSameBranch.ToString("#,###.00");

            if (_chqSameBank == 0)
                chqSameBankTxt.Text = "0.00";
            else
                chqSameBankTxt.Text = _chqSameBank.ToString("#,###.00");

            if (_chqAnoBank == 0)
                chqAnoBankTxt.Text = "0.00";
            else
                chqAnoBankTxt.Text = _chqAnoBank.ToString("#,###.00");

            totalAmtTxt.Text = _totalAmt;

            sameBranchLb.Text = _sameBranch.ToString() + "   รายการ";
            sameBankLb.Text = _sameBank.ToString() + "   รายการ";
            otherBankLb.Text = _otherBank.ToString() + "   รายการ";

            printBt.Focus();
        }

        //prevoisly it is printall
        private void printAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            //if (printAllCheck.Checked)
            //{
            //    sameBranchLb.Text = Convert.ToString(_sameBranch + _sameBank + _otherBank) + "   รายการ";
            //    sameBankLb.Text = "";
            //    otherBankLb.Text = "";
            //    totalAmtTxt.Text = _totalAmt;

            //    decimal totalChqAmout = _chqSameBranch + _chqSameBank + _chqAnoBank;
            //    if (totalChqAmout == 0)
            //        chqSameBranchTxt.Text = "0.00";
            //    else
            //        chqSameBranchTxt.Text = totalChqAmout.ToString("#,###.00");                

            //    c0.Text = "เช็ค รวมทุกธนาคาร";
            //    c1.Visible = false;
            //    c2.Visible = false;
            //    b1.Visible = false;
            //    b2.Visible = false;
            //    chqAnoBankTxt.Visible = false;
            //    chqSameBankTxt.Visible = false;
            //}
            //else
            //{
            //    InitLabel();
            //    c0.Text = "เช็ค ธนาคารสาขาเดียวกับบัญชี";
            //    chqAnoBankTxt.Visible = true;
            //    chqSameBankTxt.Visible = true;
            //    c1.Visible = true;
            //    c2.Visible = true;
            //    b1.Visible = true;
            //    b2.Visible = true;
            //}
        }

        private void printBt_Click(object sender, EventArgs e)
        {
            _sepCheque = separatedPayInCheck.Checked;
        }


    }
}