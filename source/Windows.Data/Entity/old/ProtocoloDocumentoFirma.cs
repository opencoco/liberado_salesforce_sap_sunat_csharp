using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloDocumentoFirma
    {
        public int IIdComboFirmaDocumento { get; set; }
        public bool? BFirmaDeQuienRealiza { get; set; }
        public bool? BFirmaDeQuienValida { get; set; }
        public bool? BFirmaDePaciente { get; set; }
        public int? IIdQuienEmiteInforme { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdProtocolo { get; set; }
        public short? SiEstado { get; set; }

        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual EspecialidadMedica IIdQuienEmiteInformeNavigation { get; set; }
        public virtual DocumentoSubclase IIdSubClaseDocumentoNavigation { get; set; }
    }
}
