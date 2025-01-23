using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Views.SearchMultiDocView
{
    public interface ISearchMultiDataView
    {

        Button CancelButton { get; }

        void AddSearchByMuliDocList(List<SearchByMultiDoc> documentList);

        List<SearchByMultiDoc> SearchByMuliDocList { get; set; }

        void RefreshGridDataSource();

        int SearchSuccessCount { get; set; }

        int SearchFailCount { get; set; }

        int InvoiceCount { get; set; }

        void initView();

        List<Invoice> InvoiceResultList { get; set; }

        void SearchingMode();

        void NormalMode();

        void DisplaySuccuess();

        void DisplayCancelComplated();

        void PrepareSearch();

        SearchByMultiDocParam SearchType { get; set; }

        int SearchLimitItem { get; set; }

    }
}
