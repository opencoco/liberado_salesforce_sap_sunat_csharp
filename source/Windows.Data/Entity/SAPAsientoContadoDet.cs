using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class SAPAsientoContadoDet
    {
        public string Account { get; set; }
        public string FCDebit { get; set; }
        public string Debit { get; set; }
        public string FCCredit { get; set; }
        public string Credit { get; set; }
        public string OcrCode2 { get; set; }
        public string OcrCode3 { get; set; }
        public string OcrCode4 { get; set; }
        public string OcrCode5 { get; set; }
    }
}
