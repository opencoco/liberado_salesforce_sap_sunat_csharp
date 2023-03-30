using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloObservacion
    {
        public int IIdProtocoloObservacion { get; set; }
        public string VcDescripcion { get; set; }
        public int IIdProtocolo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Protocolo IIdProtocoloNavigation { get; set; }
    }
}
