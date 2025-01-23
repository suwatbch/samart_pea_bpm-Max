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

using System.Collections.Generic;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule
{
    public interface IToBeCancelledReceiptView
    {
        PEA.BPM.CashManagementModule.Interface.BusinessEntities.TrayMoneyInfo moneyInTray { set; }
        bool AddReceipts(List<Receipt> list);
        void EnableSaveButton(bool enable);
    }
}

