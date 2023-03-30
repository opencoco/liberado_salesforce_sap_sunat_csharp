using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloDocumento
    {
        public int IIdScdocumentoProtocolo { get; set; }
        public byte? TiOrden { get; set; }
        public int IIdProtocolo { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
    }
}
