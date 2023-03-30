using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Provincia
    {
        public int IIdProvincia { get; set; }
        public string VcNombre { get; set; }
        public int? IIdPais { get; set; }
        public DateTime? DtCreado { get; set; }
        public byte? TOrden { get; set; }
    }
}
