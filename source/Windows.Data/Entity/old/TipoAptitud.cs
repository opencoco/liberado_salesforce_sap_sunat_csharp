using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class TipoAptitud
    {
        public TipoAptitud()
        {
            DefCatalogoAptitud = new HashSet<DefCatalogoAptitud>();
            ProtocoloAptitudDocumento = new HashSet<ProtocoloAptitudDocumento>();
        }

        public int IIdTipoAptitud { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public bool? BDisponible { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual ICollection<DefCatalogoAptitud> DefCatalogoAptitud { get; set; }
        public virtual ICollection<ProtocoloAptitudDocumento> ProtocoloAptitudDocumento { get; set; }
    }
}
