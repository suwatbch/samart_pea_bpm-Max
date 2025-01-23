using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.Constants
{
    public class CodeNames
    {
        public const string SlipPrintintPoolDir = "slipPrintingPool";
        public const string OfflineDataDir = "offlineData";

        public class BillBookStatus
        {
            public const string Cancel = "C";
            public const string Normal = "N";
            public const string Submitted = "T";
        }

        public class ReceiptType
        {
            public const string NonTax = "1";
            public const string Tax = "2";
        }

        public class GroupInvoiceReceiptType
        {
            public const string Dividual = "1";
            public const string Summary = "2";
        }

        public class DebtType
        {
            public class Electric
            {
                public const string Id = "M0100";
                public const string DummyId = "ELEC_DM_DEBT_CODE";
                public const string DummyName = "ELEC_DM_DEBT_DESC";
            }

            public class SalesOrder
            {
                public const string Id = "M0017";
            }

            public class SecurityDeposit
            {
                public const string Id = "MY020";
            }

            public class Asset
            {
                public const string Id = "MY900";
            }

            public class Interest
            {
                public const string Id = "M00400010";
                public const string Name = "เรียกเก็บค่าดอกเบี้ยชำระช้า";

                public class GroupInvoice
                {
                    public const string Id = "P00020002";
                    public const string Name = "เรียกเก็บค่าดอกเบี้ยชำระช้า (ชำระแบบกลุ่ม)";
                }
            }

            public class AgencyAdvancePayment
            {
                public const string Id = "AG_ADV_DEBT_CODE";
                public const string Name = "AG_ADV_DEBT_DESC";
            }

            public class AgencyReturnPayment
            {
                public const string Id = "AG_RET_DEBT_CODE";
                public const string Name = "AG_RET_DEBT_DESC";
            }

            public class AgencyGroupInvoicing
            {
                public const string Id = "P00020001";
                public const string Name = "หนี้การชำระแบบกลุ่ม";
            }

            public class ReConnectMeter
            {
                public const string Id = "REC_DEBT_CODE";
                public const string Name = "REC_DEBT_DESC";
            }

            public class DepositReceipt
            {
                public const string Id = "M00900010";
                public const string Name = "รับฝาก";
            }
        }

        public const string AccountClassForLargeElectricUser1 = "GGB1";
        public const string AccountClassForLargeElectricUser2 = "GSB1";
        public const string AccountClassForLargeElectricUser3 = "PPB1";

        public class PaymentType
        {
            public class Cash
            {
                public const string Id = "1";
                public const string Name = "เงินสด";
            }

            public class Cheque
            {
                public const string Id = "2";
                public const string Name = "เช็ค";
            }

            public class Deposit
            {
                public const string Id = "3";
                public const string Name = "ใบนำฝาก";
            }
        }

        public class ContractType
        {
            public class Electric
            {
                public const string Id = "01";
            }

            public class Agency
            {
                public const string Id = "51";
            }
        }

        public class PaymentMethod
        {
            public class DirectDebit
            {
                public const string Id = "D";
                public const string Name = "ตัดบัญชีธนาคาร";
            }

            public class GroupInvoicing
            {
                public const string Id = "G";
                public const string Name = "ชำระแบบเป็นกลุ่ม";
            }

            public class Counter
            {
                public const string Id = "C";
                public const string Name = "เคาน์เตอร์ กฟฟ. หรือจุดบริการฯ";
            }
        }

        public class UnitType
        {

            public class Day
            {
                public const string Id = "TAG";
                public const string Name = "วัน";
            }

            public class Book
            {
                public const string Id = "004";
                public const string Name = "เล่ม";
            }

            public class Volume
            {
                public const string Id = "005";
                public const string Name = "ฉบับ";
            }
        }

        public class TaxCode
        {
            public class NoTaxCharge
            {
                public const string TaxCode = "OX";
                public const string TaxRate = "";
            }

            public class ZeroTaxCharge
            {
                public const string TaxCode = "O0";
                public const string TaxRate = "0";
            }

            public class SevenTaxCharge
            {
                public const string TaxCode = "O7";
                public const string TaxRate = "7";
            }
        }

        public class InterestKey
        {
            public class NoInterest
            {
                public const string InterestKey = "Z0";
                public const string InterestRate = "0.00";
            }

            public class FifteenInterest
            {
                public const string InterestKey = "Z1";
                public const string InterestRate = "0.15";
            }        
        }

        public const string AcceptableRoundUp = "0.12";

        public const string BranchCenter = "Z00000";
    }
}
