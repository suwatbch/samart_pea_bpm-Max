using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Views.QRPaymentView
{
    public interface IQRPaymentView
    {
        PaymentMethod QRPaymentMethod { get; set; }

        decimal balancePaid { get; set; }

        bool processedQR { get; set; }

        QRPaymentInfo QRPaymentInfo { get; set; }

        QRPaymentInfo PostOfflineResultInfo { get; set; }

        QRPaymentInfo QRPaymentInfoStateOfStatus { get; set; } 

        void InitialMode();

        void CheckStateOfQR();

        void ResetForm();

        void OfflineProcess();

        void OfflineEnabled();

        void OfflineDisabled();

        void CheckStatusQREnabled(bool enableFlag);

        void CheckStatusQRProcess();

        void DisplayReferenceNo();

        void QRPaymentSueecess();

        string QRPaymentCheckStatusResult { set; get; }

        void PostOfflinePaymentProcess();

    }
}
