using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.SmartPlus.Interface.BusinessEntities
{
    public class InterestPendingModel
    {
        public string       ItemId      {get;set;}
        public string       CaId        {get;set;}
        public string       DtId        {get;set;}
        public string       DtName      {get;set;}
        public string       Description {get;set;}
        public string       InvoiceNo   {get;set;}
        public decimal?     Qty         {get;set;}
        public string       UnitTypeId  {get;set;}
        public decimal?     Amount      {get;set;}
        public string       TaxCode     {get;set;}
        public decimal?     VatAmount   {get;set;}
        public decimal?     GAmount     {get;set;}
        public string       POSDebtFlag {get;set;}
        public string       SyncFlag    {get;set;}        
        public DateTime     PostDt      {get;set;}
        public DateTime     ModifiedDt  {get;set;}
        public string       ModifiedBy  {get;set;}
        public string       Active      {get;set;}
        public string       RefInvoiceNo            { get; set; }
        public string       PostBranchServerId      { get; set; }
        public string       Remark                  { get; set; }
        public string       WebServiceCallGroupId   { get; set; }
        public string       OriginalInvoiceNo       { get; set; }
        public string       Period                  { get; set; }
        public Decimal?     FTAmount                { get; set; }
        public string       InstallmentPeriodText   { get; set; }
    }
}
