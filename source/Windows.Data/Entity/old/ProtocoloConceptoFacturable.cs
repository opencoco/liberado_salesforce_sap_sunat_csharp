using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloConceptoFacturable
    {
        public ProtocoloConceptoFacturable()
        {
            CitaTrabajadorPrecio = new HashSet<CitaTrabajadorPrecio>();
        }

        public int IIdProtConceptoFacturable { get; set; }
        public int IIdConceptoFacturable { get; set; }
        public decimal? DePrecio { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public int IIdProtocolo { get; set; }

        public virtual CitaConceptoFacturable IIdConceptoFacturableNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual ICollection<CitaTrabajadorPrecio> CitaTrabajadorPrecio { get; set; }
    }
}
