//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;

namespace PEA.BPM.PaymentCollectionModule
{
    public class ReceiptNoPrefix
    {
        private Dictionary<string, string> _prefix;
        private static ReceiptNoPrefix _instance = new ReceiptNoPrefix();

        private ReceiptNoPrefix()
        {
            _prefix = new Dictionary<string, string>();
            _prefix.Add(
                string.Format("S.2.{0}", CodeNames.DebtType.Electric.Id),
                "A"); // Slip ������Ѻ�Թ/㺡ӡѺ���� ���俿��
            _prefix.Add(
                string.Format("P.2.{0}", CodeNames.DebtType.Electric.Id),
                "B"); // Prep ������Ѻ�Թ/㺡ӡѺ���� ���俿��


            _prefix.Add(
                string.Format("S.2.{0}", "M0200"),
                "A"); // Slip ������Ѻ�Թ/㺡ӡѺ���� ���俿��
            _prefix.Add(
                string.Format("P.2.{0}", "M0200"),
                "B"); // Prep ������Ѻ�Թ/㺡ӡѺ���� ���俿��


            _prefix.Add(
                string.Format("S.2.{0}", "M9000"),
                "A"); // Slip ������Ѻ�Թ/㺡ӡѺ���� ���俿��
            _prefix.Add(
                string.Format("P.2.{0}", "M9000"),
                "B"); // Prep ������Ѻ�Թ/㺡ӡѺ���� ���俿��


            _prefix.Add("S.1", "C"); // Slip (������Ѻ�Թ)
            _prefix.Add("P.1", "D"); // Preprinted (������Ѻ�Թ)            
            _prefix.Add("S.2", "E"); // Slip (������Ѻ�Թ/㺡ӡѺ����)
            _prefix.Add("P.2", "F"); // Preprinted (������Ѻ�Թ/㺡ӡѺ����)

            _prefix.Add(
                string.Format("S.1.{0}", CodeTable.Instant.GetAppSettingValue(CodeNames.DebtType.AgencyAdvancePayment.Id)),
                "G"); // POS (������Ѻ�Թ) �Թ��ǧ˹�� 30%
            _prefix.Add(
                string.Format("S.1.{0}", CodeTable.Instant.GetAppSettingValue(CodeNames.DebtType.AgencyReturnPayment.Id)),
                "H"); // POS (������Ѻ�Թ) �Թ��ǹ��������       

            //_prefix.Add("B.1.2", "C"); // Slip ������Ѻ�Թ �͡����
            //_prefix.Add("A.1.2", "D"); // Prep ������Ѻ�Թ �͡����            
            //_prefix.Add("B.3", "I"); // POS (��Ѻ�Թ)
        }

        public static string Get(string paperSize, string receiptType, string debtType)
        {
            string prefix;

            string code = string.Format("{0}.{1}.{2}",
                paperSize,
                receiptType,
                debtType.Trim()
                );

            if (_instance._prefix.ContainsKey(code))
            {
                prefix = _instance._prefix[code];
            }
            else if(_instance._prefix.ContainsKey(code.Substring(0, code.Length-4)))
            {
                prefix = _instance._prefix[code.Substring(0, code.Length-4)];
            }
            else
            {
                prefix = _instance._prefix[code.Substring(0, code.Length-10)];
            }

            return prefix;
        }
    }
}

