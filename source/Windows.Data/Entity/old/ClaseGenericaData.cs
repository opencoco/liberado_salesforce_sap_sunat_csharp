using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ClaseGenericaData
    {
        public int IIdClaseGenericaData { get; set; }
        public string IIdClaseGenerico { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short SiCodigo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public byte? TiOrden { get; set; }
        public short? SiEstado { get; set; }

        public virtual ClaseGenerico IIdClaseGenericoNavigation { get; set; }
    }
}
