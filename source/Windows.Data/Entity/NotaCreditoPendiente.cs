using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class NotaCreditoPendiente
    {
       
        public string Tipo { get; set; }
        public long? Id { get; set; }
        public long? IIdCitaSunatCab { get; set; }
        public long? IIdCita { get; set; }
        public string Serie { get; set; }
        public int Correlativo { get; set; }
        public string tipoComprobante { get; set; }
        public bool BOkGeneracionNCPdf { get; set; }
        public bool BOkGeneracionNCXml { get; set; }
    }
}
