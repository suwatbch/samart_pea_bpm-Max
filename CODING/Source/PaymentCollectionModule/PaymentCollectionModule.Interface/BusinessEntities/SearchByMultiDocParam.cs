using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    public class SearchByMultiDocParam
    {
        /// <summary>
        /// "1" : Search by CaId 
        /// "2" : Group invoincing.
        /// "3" : 
        /// </summary>
        public string SearchTypeParam { get; set; }
    }
}
