using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.Constants
{
    public class CodeNames
    {
        public const string SlipPrintintPoolDir = "slipPrintingPool";
        public const string OfflineDataDir = "offlineData";
        public const string OfflineFakeDir = "offlinePayment";

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

            public class NonInvoiceY900
            {
                public const string Id = "MY900";
            }

            public class NonInvoiceY905
            {
                public const string Id = "MY905";
            }

            public class NonInvoiceY910
            {
                public const string Id = "MY910";
            }

            public class NonInvoiceY920
            {
                public const string Id = "MY920";
            }

            public class NonInvoiceY930
            {
                public const string Id = "MY930";
            }

            public class NonInvoiceY990
            {
                public const string Id = "MY990";
            }

            public class Interest
            {
                public const string Id = "M00400010";
                public const string Name = "���¡�纤�Ҵ͡���ª��Ъ��";

                public class GroupInvoice
                {
                    public const string Id = "M00400010";
                    public const string Name = "���¡�纤�Ҵ͡���ª��Ъ�� (����Ẻ�����)";
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
                public const string Name = "˹���õѴ����Ẻ�����";
            }

            public class ReConnectMeter
            {
                public const string Id = "REC_DEBT_CODE";
                public const string Name = "REC_DEBT_DESC";
            }

            public class DepositReceipt
            {
                public const string Id = "M00900010";
                public const string Name = "�Թ�Ѻ�ҡ";
            }

            public class OtherBranchElectric
            {
                public const string Id = "MY9900010";
                public const string Name = "�Ѻ�Թ�ͧ ���. ����ѧ����� ISU";
            }

            public class APPayment
            {
                public const string Id = "MZ9100010";
                public const string Name = "�Ѻ�����Թ�ͧ���˹��";
            }

            public class BailDebt
            {
                public const string Id = "BAIL_DEBT_CODE";
                public const string Name = "BAIL_DEBT_DESC";
            }

            public class ExceptioalBailDebt
            {
                public const string Id = "EXCEPTIONAL_BAIL_DEBT_CODE";
                public const string Name = "EXCEPTIONAL_BAIL_DEBT_DESC";
            }

            public class DebitNoteDebt
            {
                public const string Id = "DEBIT_NOTE_DEBT_CODE";
                public const string Name = "DEBIT_NOTE_DEBT_DESC";
            }

            //TODO: INSTALLMENT CASE
            //public class ExceptioalInstallmentDebt
            //{
            //    public const string Id = "EXCEPTIONAL_INSTMENT_DEBT_CODE";
            //    public const string Name = "EXCEPTIONAL_INSTMENT_DEBT_DESC";
            //}

        }

        public const string AccountClassForLargeElectricUser1 = "GGB1";
        public const string AccountClassForLargeElectricUser2 = "GSB1";
        public const string AccountClassForLargeElectricUser3 = "PPB1";

        public class PaymentType
        {
            public class Cash
            {
                public const string Id = "1";
                public const string Name = "�Թʴ";
            }

            public class Cheque
            {
                public const string Id = "2";
                public const string Name = "��";
            }

            public class Deposit
            {
                public const string Id = "3";
                public const string Name = "㺹ӽҡ";
            }

            public class QRPayment
            {
                public const string Id = "5";
                public const string Name = "QR Payment";
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
                public const string Name = "�Ѵ�ѭ�ո�Ҥ��";
            }

            public class GroupInvoicing
            {
                public const string Id = "G";
                public const string Name = "����Ẻ�繡����";
            }

            public class Counter
            {
                public const string Id = "C";
                public const string Name = "�ҹ����� ���. ���ͨش��ԡ���";
            }

            public class EPayment
            {
                public const string Id = "M";
                public const string Name = "���м�ҹ (EPayment)";
            }
        }

        public class UnitType
        {

            public class Day
            {
                public const string Id = "TAG";
                public const string Name = "�ѹ";
            }

            public class Book
            {
                public const string Id = "004";
                public const string Name = "����";
            }

            public class Volume
            {
                public const string Id = "005";
                public const string Name = "��Ѻ";
            }

            public class KWH
            {
                public const string Id = "KWH";
                public const string Name = "KWH";
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

            public class DS
            {
                public const string TaxCode = "DS";
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


        public class DisconnectionStatus
        {

            public class ProposeDisconnect
            {
                public const string Id = "00";
                public const string Name = "���ҧ";
            }

            public class Approved
            {
                public const string Id = "10";
                public const string Name = "͹��ѵ�/�ʹ������";
            }

            public class OrderGenerated
            {
                public const string Id = "20";
                public const string Name = "��ç�������������";
            }

            public class FuseRemoved
            {
                public const string Id = "21";
                public const string Name = "�Ŵ���";
            }

            public class ReconnectMeter
            {
                public const string Id = "22";
                public const string Name = "��õ�͡�Ѻ�������";
            }

            public class ProcessCompleted
            {
                public const string Id = "99";
                public const string Name = "�Դ/��������ó�";
            }
        }
        public class DisconnectElecStatus
        {
            public class FuseRemoved
            {
                public const string Id = "X";
                public const string Name = "�Ŵ���";
            }
            public class MeterRemoved
            {
                public const string Id = "Y";
                public const string Name = "�ʹ������";
            }
            public class CancelFuseRemoved
            {
                public const string Id = "Z";
                public const string Name = "¡��ԡ�Ŵ���";
            }
        }


    }
}
