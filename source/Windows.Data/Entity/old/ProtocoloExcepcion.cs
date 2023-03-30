using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloExcepcion
    {
        public int IIdProtPruebaExcepcion { get; set; }
        public int? IIdCatalogoExcepcion { get; set; }
        public int? IIdProtocolo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual DefCatalogoExcepcion IIdCatalogoExcepcionNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
    }
}
