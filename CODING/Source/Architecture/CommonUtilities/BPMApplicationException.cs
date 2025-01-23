using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Data.SqlClient;
using System.Net;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class BPMApplicationException : Exception
    {
        public bool AutoCreate = false;

        public string ErrorCode = "";
        public string OriginalType = "";
        public DateTime OccurWhen; // ���ҷ���Դ ����繽�� server ��� datetime.now �� set �������� ������ client ��ͧ��� datetime �ҡ session
        public EErrorInModule Module = EErrorInModule.Unknow;
        public EErrorInLayer Layer = EErrorInLayer.Unknow;
        public string DebuggingId = "";
        public string ServerStackTrace = ""; // �� stacktrace ����Ҩҡ��� server        
        public bool CanContinue = true;

        public string THMessage = "";
        public string Cause = "";
        public string Resolve = "";
        public string HelpURL = "";

        private string _capturepointstacktrace = "";
        public string FullStackTrace // stacktrace Ẻ�����ҨѴ format �ͧ
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (ServerStackTrace.Length != 0)
                {
                    sb.AppendLine(ServerStackTrace.Trim());
                    sb.AppendLine(@"<<<< /\ Server /\ >>>>");
                    sb.AppendLine("");
                    sb.AppendLine(@"<<<< \/ Client \/ >>>>");
                }
                int innercount = 0;
                Exception innneree = this.InnerException;
                while (innneree != null)
                {
                    SimpleExceptionInfo seinfo = GetAdditionalInfo(innneree);
                    StringBuilder innersb = new StringBuilder();
                    innersb.AppendLine("*** Inner exception [" + innercount + "] ***");
                    innersb.AppendLine("Message: [" + seinfo.Message + "]");
                    if (seinfo.Additional.Length != 0) innersb.AppendLine("Additional: [" + seinfo.Additional + "]");
                    innersb.AppendLine("Type: [" + seinfo.TypeName + "]");
                    innersb.AppendLine(seinfo.StackTrace.TrimEnd());
                    innersb.AppendLine("*** Inner exception [" + innercount + "] ***" + Environment.NewLine);
                    sb.Insert(0, innersb.ToString());

                    innneree = innneree.InnerException;
                    innercount++;
                }
                if (!string.IsNullOrEmpty(this.StackTrace)) sb.AppendLine(StackTrace);

                if (_capturepointstacktrace != "") sb.AppendLine(_capturepointstacktrace);
                return sb.ToString();
            }
        }
        public override string ToString()
        {
            return FullStackTrace;
        }

        #region ���ҧ exception ������ͧ
        /// <summary>
        /// ���ҧ exception Ẻ���Ǻ����� �·���к�������Դ exception ����ҡ�͹ ��ͧ�� register error code ������
        /// </summary>
        /// <param name="module">Module Id</param>
        /// <param name="layer">Layer Id</param>
        /// <param name="occurwhen">���ҷ���Դ</param>
        /// <param name="errorcode">Error code ��� register �ҡ�͹����</param>
        /// <param name="message">Message ͸Ժ�� ��������ҧ�</param>
        /// <param name="cancontinue">�ѧ run program �������������</param>
        public BPMApplicationException(EErrorInModule module, EErrorInLayer layer,
            DateTime occurwhen, string errorcode, string message, bool cancontinue)
            : base(message)
        {
            AutoCreate = false;
            ErrorCode = errorcode;
            OriginalType = "BPMApplicationException";
            OccurWhen = occurwhen;
            Module = module;
            Layer = layer;
            DebuggingId = ""; // ��ͧ save ŧ database ��͹�֧������
            ServerStackTrace = ""; // �����
            CanContinue = cancontinue;
        }
        /// <summary>
        /// ���ҧ exception Ẻ���Ǻ����� ���� exception ����Դ������㹵�Ǵ��� ��ͧ�� register error code ������
        /// </summary>
        /// <param name="module"></param>
        /// <param name="layer"></param>
        /// <param name="occurwhen"></param>
        /// <param name="errorcode"></param>
        /// <param name="message">Message ͸Ժ�� ��������ҧ�</param>
        /// <param name="cancontinue">�ѧ run program �������������</param>
        /// <param name="ee">inner exception</param>
        public BPMApplicationException(EErrorInModule module, EErrorInLayer layer,
            DateTime occurwhen, string errorcode, string message, bool cancontinue, Exception ee)
            : base(message, ee)
        {
            AutoCreate = false;
            ErrorCode = errorcode;
            OriginalType = ee.GetType().ToString();
            OccurWhen = occurwhen;
            Module = module;
            Layer = layer;
            DebuggingId = ""; // ��ͧ save ŧ database ��͹�֧������
            ServerStackTrace = ""; // �����
            CanContinue = cancontinue;
        }
        #endregion

        #region ��� Gateway service ��
        /// <summary>
        /// ����Ǵѡ�Ѻ exception �ç gateway service �繤���ҹ
        /// </summary>
        /// <param name="errorcode"></param>
        /// <param name="originaltype"></param>
        /// <param name="occurwhen"></param>
        /// <param name="module"></param>
        /// <param name="layer"></param>
        /// <param name="debuggingid"></param>
        /// <param name="servcestacktrace"></param>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="cancontinue"></param>
        public BPMApplicationException(string errorcode, string originaltype, DateTime occurwhen,
            EErrorInModule module, EErrorInLayer layer, string debuggingid, string servcestacktrace,
            string message, string source, bool cancontinue, string thmessage, string cause,
            string resolve, string helpurl)
            : base(message)
        {
            AutoCreate = true;
            ErrorCode = errorcode;
            OriginalType = originaltype;
            OccurWhen = occurwhen;
            Module = module;
            Layer = layer;
            DebuggingId = debuggingid;
            ServerStackTrace = servcestacktrace;

            base.Source = source;
            CanContinue = cancontinue;
            THMessage = thmessage;
            Cause = cause;
            Resolve = resolve;
            HelpURL = helpurl;
        }
        #endregion

        #region ���ǡ��� convert exception ��
        /// <summary>
        /// ����¹ exception ��Դ���� ����� BPMApplicationException
        /// </summary>
        /// <param name="module"></param>
        /// <param name="layer"></param>
        /// <param name="occurwhen"></param>
        /// <param name="ee"></param>
        public BPMApplicationException(EErrorInModule module, EErrorInLayer layer, DateTime occurwhen, Exception ee)
            : base(ee.Message, ee)
        {
            AutoCreate = true;
            ErrorCode = ""; // �ѧ����� ��ͧ��� save � database ��͹�֧������
            OriginalType = ee.GetType().ToString();
            OccurWhen = occurwhen;
            Module = module;
            Layer = layer;
            DebuggingId = ""; // ��ͧ save ŧ database ��͹�֧������
            ServerStackTrace = "";
            CanContinue = true;
        }
        #endregion

        public void UpdateStackTrace(int startat)
        {
            _capturepointstacktrace = ExceptionHelper.GetCurrentStackTrace(startat);
        }

        private SimpleExceptionInfo GetAdditionalInfo(Exception ee)
        {
            if (ee == null) return null;

            SimpleExceptionInfo info = new SimpleExceptionInfo();
            Type eetype = ee.GetType();

            info.Message = ee.Message;
            info.StackTrace = ee.StackTrace;
            info.TypeName = eetype.ToString();

            if (eetype == typeof(SqlException))
            {
                SqlException ex = ee as SqlException;
                info.Additional = ex.Number + ": " + ex.Procedure;
            }
            else if (eetype == typeof(WebException))
            {
                WebException ex = ee as WebException;
                info.Additional = ex.Status.ToString();
            }

            return info;
        }

        public BPMApplicationExceptionInfo GetInfo()
        {
            BPMApplicationExceptionInfo exceptioninfo = new BPMApplicationExceptionInfo();
            exceptioninfo.ErrorCode = this.ErrorCode;
            exceptioninfo.OriginalType = this.OriginalType;
            exceptioninfo.OccurWhen = this.OccurWhen;
            exceptioninfo.Module = (int)this.Module;
            exceptioninfo.Layer = (int)this.Layer;
            exceptioninfo.DebuggingId = this.DebuggingId;

            exceptioninfo.Message = this.Message;
            exceptioninfo.StackTrace = this.FullStackTrace;
            exceptioninfo.Source = this.Source;
            exceptioninfo.CanContinue = this.CanContinue;

            Exception innneree = this.InnerException;
            while (innneree != null)
            {
                exceptioninfo.AdditionalInfo.Add(GetAdditionalInfo(innneree));
                innneree = innneree.InnerException;
            }

            return exceptioninfo;
        }



    }
}