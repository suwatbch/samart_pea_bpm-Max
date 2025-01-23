//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This interface defines the contract between the Presenter and its View, following the
// Model-View-Presenter pattern. 
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.CompositeUI.WinForms;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.AgencyManagementModule
{
    public interface IBillBookSearchView
    {
        DeckWorkspace BookManagerDeckWorkspace { get; }
        DeckWorkspace CallingBillDeckWorkSpace { get; }
        DeckWorkspace NoneCallingBillDeckWorkSpace { get; }
        DeckWorkspace SumCallingBillDeckWorkSpace { get; }
        DeckWorkspace LineBillDeckWorkSpace { get; }
        DeckWorkspace LineNoBillDeckWorkSpace { get; }
        //DeckWorkspace TheOtherBillDeckWorkSpace { get; }
        void ActivateFindResultPanel(int index);
        BillBookHeaderInfo GetBillBookHeader();
        AgentInfo SearchAgentInfo { set; }
        string ReceiveCount { set; }
        void FillPastBillBookHeader(BillBookItemListInputInfo bookItemList, bool freeze);
        void ClearHeader();
        void SetDefaultCursor();
        void ActivateBookCreationPanel();
        void StartInput();
        void BillbookHeaderEnable(bool enable);
        void FocusAgentInput();
        void FocusBillManInput();
        void FocusReceiveCount();
        void FocusCancelBillBook();
        void FocusEmployeeInput();       
        decimal? UsedDeposit { set; get; }
        string BillPeriod { set; get; }
        void EnableReceiveCount(bool enable);
        void SetCancelBillBook(bool enable);
        void RenewOldBook();
        IBillBookManagementView BillBookMgtView { set; }
        bool CheckAdvPaymentDt();
        bool CheckReturnDt();
        void FoundEmployeeNo();
        string ControllerId { get; }     
        string CancelBillBookId { get; }
    }
}

