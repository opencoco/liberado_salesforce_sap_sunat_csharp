using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloPrueba
    {
        public int IIdPruebaProtocoloRelacion { get; set; }
        public byte? TiOrden { get; set; }
        public bool? BObligatorio { get; set; }
        public int IIdProtocolo { get; set; }
        public int IIdPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
