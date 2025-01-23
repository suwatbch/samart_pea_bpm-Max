using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.ePaymentsModule.DA;
using System.ComponentModel;

namespace PEA.BPM.ePaymentsModule.BS
{
    public class ReportBS : IReportService
    {


        #region IReportService Members


        public List<RE01_ReportInfo> GetRe01ReportService(RE01Param param)
        {
            try
            {
                ReportDA conData = new ReportDA();
                List<RE01_ReportInfo> re01List = conData.GetRe01ReportData(param);
                return re01List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RE02_ReportInfo> GetRe02ReportService(RE02ParamInfo param)
        {
            try
            {
                ReportDA conData = new ReportDA();
                List<RE02_ReportInfo> re02List = conData.GetRe02ReportData(param);
                return re02List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RE03_ReportInfo> GetRe03ReportService(RE03ParamInfo param)
        {
            try
            {
                ReportDA conData = new ReportDA();
                List<RE03_ReportInfo> re03List = conData.GetRe03ReportData(param);
                return re03List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RE04_ReportInfo> GetRe04ReportService(RE04ParamInfo param)
        {
            try
            {
                ReportDA conData = new ReportDA();
                List<RE04_ReportInfo> re04List = conData.GetRe04ReportData(param);
                return re04List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RE05_ReportInfo> GetRe05ReportService(RE05ParamInfo param)
        {
            try
            {
                ReportDA conData = new ReportDA();
                List<RE05_ReportInfo> re05List = conData.GetRe05ReportData(param);
                return re05List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RE06_ReportInfo> GetRe06ReportInfo(RE06ParamInfo param)
        {
            try
            {
                ReportDA conData = new ReportDA();
                List<RE06_ReportInfo> re06List = conData.GetRe06ReportData(param);
                return re06List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RE07_ReportInfo> GetRe07ReportInfo(RE07ParamInfo param)
        {
            try
            {
                
                ReportDA conData = new ReportDA();
                List<RE07_ReportInfo> re07List = conData.GetRe07ReportData(param);
                return re07List;
            }
            catch (Exception e)
            {
                throw;
            }
        }
      

        public List<RE08_ReportInfo> GetRe08ReportInfo(RE08ParamInfo param)
        {
            try
            {

                ReportDA conData = new ReportDA();
                List<RE08_ReportInfo> re08List = conData.GetRe08ReportData(param);
                return re08List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<RE09_ReportInfo> GetRe09ReportInfo(RE09ParamInfo param)
        {
            try
            {

                ReportDA conData = new ReportDA();
                List<RE09_ReportInfo> re09List = conData.GetRe09ReportData(param);
                return re09List;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion
    }
}
