using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using System.Threading;
using System.Collections;
using PEA.BPM.ePaymentsModule.SG;
using System.Data;
using System.Configuration;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;

namespace PEA.BPM.ePaymentsModule.Services
{
    [Service(typeof(IReportService))]
    public class ReportService : IReportService
    {
        #region Service Factory

        public IReportService GetService()
        {
            return GetService(false);
        }

        public IReportService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new ReportSG(true);
            }
            else
            {
                return new ReportSG(false);
            }
        }
      
        #endregion

     
        #region IReportService Members

        public List<RE01_ReportInfo> GetRe01ReportService(RE01Param param)
        {
            try
            {
                return GetService().GetRe01ReportService(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RE02_ReportInfo> GetRe02ReportService(RE02ParamInfo param)
        {
            try
            {
                return GetService().GetRe02ReportService(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RE03_ReportInfo> GetRe03ReportService(RE03ParamInfo param)
        {
            try
            {
                return GetService().GetRe03ReportService(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RE04_ReportInfo> GetRe04ReportService(RE04ParamInfo param)
        {
            try
            {
                return GetService().GetRe04ReportService(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RE05_ReportInfo> GetRe05ReportService(RE05ParamInfo param)
        {
            try
            {
                return GetService().GetRe05ReportService(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RE06_ReportInfo> GetRe06ReportInfo(RE06ParamInfo param)
        {
            try
            {
                return GetService().GetRe06ReportInfo(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<RE07_ReportInfo> GetRe07ReportInfo(RE07ParamInfo param)
        {
            try
            {
                return GetService().GetRe07ReportInfo(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }     

        public List<RE08_ReportInfo> GetRe08ReportInfo(RE08ParamInfo param)
        {
            try
            {
                return GetService().GetRe08ReportInfo(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RE09_ReportInfo> GetRe09ReportInfo(RE09ParamInfo param)
        {
            try
            {
                return GetService().GetRe09ReportInfo(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
