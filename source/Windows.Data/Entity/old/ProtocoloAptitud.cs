using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloAptitud
    {
        public int IIdProtPruebaAptitud { get; set; }
        public int? IIdCatalogoAptitud { get; set; }
        public int? IIdProtocolo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
    }
}
