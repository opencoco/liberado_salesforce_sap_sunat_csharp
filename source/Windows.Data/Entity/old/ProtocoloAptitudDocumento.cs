using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloAptitudDocumento
    {
        public int IIdDocumentoProtocoloRelacion { get; set; }
        public byte? TiOrden { get; set; }
        public int IIdProtocolo { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdtipoAptitud { get; set; }
        public short? SiEstado { get; set; }

        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual DocumentoSubclase IIdSubClaseDocumentoNavigation { get; set; }
        public virtual TipoAptitud IIdtipoAptitudNavigation { get; set; }
    }
}
