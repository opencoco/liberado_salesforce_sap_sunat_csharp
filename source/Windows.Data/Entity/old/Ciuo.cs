using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Ciuo
    {
        public int IIdCiuo { get; set; }
        public int InCiuo { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
    }
}
