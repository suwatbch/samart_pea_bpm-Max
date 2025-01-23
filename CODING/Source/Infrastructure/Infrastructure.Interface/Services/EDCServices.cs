using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.Infrastructure.Interface.Services
{
    public class EDCServices
    {
        private string ErrorText;    

        public EDCInfo.ResponseMessageModel ExtractResponseMessages(string prm)
        {
            
            var prms = prm.Substring(2, prm.Length - 2);
            prms     = Cleaner(prm);
           
            var r = new EDCInfo.ResponseMessageModel
            {
                TransactionType4Byte    = ExtractTransactionType(prms),
                DataLen3Byte            = ExtractDataLen(prms),
                ReponseCode2Byte        = ExtractReponseCode(prms),
                SystemTrace6Byte        = ExtractSystemTrace(prms),
                BatchNumber6Byte        = ExtractBatchNumber(prms),
                TraceInvoice6Byte       = ExtractTraceInvoice(prm),
                ApproveCode6Byte        = ExtractApproveCode(prms),
                ReferenceNumber12Byte   = ExtractReferenceNumber(prms),
                CardNumber20Byte        = ExtractCardNumber(prms),
                CardExpireDateYYMM4Byte = ExtractCardExpireDate(prms),
                CardLabel10Byte         = ExtractCardLabel(prms),
                ReponseText10Byte       = ExtractReponseText(prms),
                TxnDateYYMMDD6Byte      = ExtractTxnDate(prms),
                TxnTimeHHMMSS6Byte      = ExtractTxnTime(prms),
                Tid8Byte                = ExtractTid(prms),
                Mid15Byte               = ExtractMid(prms),
                Nli4Byte                = ExtractNli(prms),
                SlipHeadingLineA23Byte  = ExtractSlipHeadingA(prms),
                SlipHeadingLineB23Byte  = ExtractSlipHeadingB(prms),
                SlipHeadingLineC23Byte  = ExtractSlipHeadingC(prms),
                CardHolderName22Byte    = ExtractCardHolderName(prms)
            };
            return r;
        }

        private string CalculateLRC(byte[] bytes)
        {
            byte LRC = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                LRC ^= bytes[i];
            }

            string result = Convert.ToString(int.Parse(LRC.ToString()), 16).ToUpper();

            return result;
        }

        #region extract response messege function 

        private string ExtractTransactionType(string prm)
        {
            //'SALE'
            var result = "";
            try
            {                
                result      = prm.Substring(0, 4).Trim();  
            }
            catch(Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }  
        private string ExtractDataLen(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(4, 3).Trim();   
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }     
        private string ExtractReponseCode(string prm)
        {             
            var result = "";
            try
            {                  
                result = prm.Substring(7, 2).Trim(); 
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractSystemTrace(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(9, 6).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractBatchNumber(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(15, 6).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractTraceInvoice(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(21, 6).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractApproveCode(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(27, 6).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractReferenceNumber(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(33, 12).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractCardNumber(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(45, 20).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractCardExpireDate(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(65, 4).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractCardLabel(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(69, 10).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractReponseText(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(79, 16).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractTxnDate(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(95, 6).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractTxnTime(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(101, 6).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractTid(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(107, 8).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractMid(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(115, 15).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractNli(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(130, 4).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractSlipHeadingA(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(134, 23).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        } 
        private string ExtractSlipHeadingB(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(157, 23).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }  
        private string ExtractSlipHeadingC(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(180, 23).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }
        private string ExtractCardHolderName(string prm)
        {
            //'SALE'
            var result = "";
            try
            {
                result = prm.Substring(203, 22).Trim();
            }
            catch (Exception ex)
            {
                result = ErrorText + ex.Message;
            }
            return result;
        }      
        private string Cleaner(string prm)
        {
            var cleaned = "";
            try
            {
                cleaned = prm.Replace("\0", string.Empty);
                cleaned = cleaned.Replace("\t", string.Empty);
                cleaned = cleaned.Replace("\b", string.Empty);
                //cleaned = cleaned.Replace(@"q", string.Empty);
                cleaned = cleaned.Replace(@"&", string.Empty);
                cleaned = cleaned.Trim();
            }
            catch (Exception ex)
            {
                cleaned = ex.Message;
            }
            return cleaned;
        }

        #endregion
    }
}
