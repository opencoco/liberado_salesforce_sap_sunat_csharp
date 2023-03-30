using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class SAPAsientoContadoPen
    {
        public int? IIdCitaSunatCab { get; set; }
        public int? ISunatCorrelativo { get; set; }
        public string VcSunatSerie { get; set; }
        public string VcTipoDoc { get; set; }

        public string DocEntry { get; set; }
        public string U_VKP_TipoDoc { get; set; }
        public string TransId { get; set; }
    }
}
