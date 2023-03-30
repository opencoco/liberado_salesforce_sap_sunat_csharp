using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class SAPAsientoContadoCab
    {
        public string TransCode { get; set; }
        public string DocCur { get; set; }
        public string DocDate { get; set; }
        public string DocDueDate { get; set; }
        public string TaxDate { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Memo { get; set; }
    }
}
