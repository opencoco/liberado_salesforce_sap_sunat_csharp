using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloRolDocumento
    {
        public int IIdComboRolDocumento { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public int IIdRol { get; set; }
        public byte? TiTipo { get; set; }
        public byte TiOrden { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdProtocolo { get; set; }
        public short? SiEstado { get; set; }

        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual Rol IIdRolNavigation { get; set; }
        public virtual DocumentoSubclase IIdSubClaseDocumentoNavigation { get; set; }
    }
}
