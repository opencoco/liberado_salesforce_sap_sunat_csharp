using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Pais
    {
        public int IIdPais { get; set; }
        public string VcNombre { get; set; }
        public byte? TOrden { get; set; }
        public DateTime? DtCreado { get; set; }
    }
}
