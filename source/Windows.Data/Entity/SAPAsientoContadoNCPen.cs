using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class SAPAsientoContadoNCPen
    {
        public int? IIdCitaSunatCab { get; set; }
        public int? IIdCitaSunatDet { get; set; }
        public bool? BCabecera { get; set; }
        public string DocEntry { get; set; }
        public string U_VKP_TipoDoc { get; set; }
    }
}
