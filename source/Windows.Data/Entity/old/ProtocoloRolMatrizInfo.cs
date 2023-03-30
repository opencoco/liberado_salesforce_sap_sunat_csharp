using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloRolMatrizInfo
    {
        public int IIdProtocoloMatrizInfo { get; set; }
        public int VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdProtocolo { get; set; }
        public short? SiEstado { get; set; }
        public int IIdRol { get; set; }

        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual MatrizInfo VcNombreNavigation { get; set; }
    }
}
