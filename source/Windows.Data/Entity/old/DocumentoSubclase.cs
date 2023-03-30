using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DocumentoSubclase
    {
        public DocumentoSubclase()
        {
            CotiEmpresaProtAptitudDocumento = new HashSet<CotiEmpresaProtAptitudDocumento>();
            CotiEmpresaProtDocumentoFirma = new HashSet<CotiEmpresaProtDocumentoFirma>();
            CotiEmpresaProtRolDocumento = new HashSet<CotiEmpresaProtRolDocumento>();
            Documento = new HashSet<Documento>();
            ProtocoloAptitudDocumento = new HashSet<ProtocoloAptitudDocumento>();
            ProtocoloDocumentoFirma = new HashSet<ProtocoloDocumentoFirma>();
            ProtocoloRolDocumento = new HashSet<ProtocoloRolDocumento>();
        }

        public int IIdSubClaseDocumento { get; set; }
        public string VcNombre { get; set; }
        public int IIdClaseDocumento { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public string VcDescripcion { get; set; }

        public virtual DocuementoClase IIdClaseDocumentoNavigation { get; set; }
        public virtual ICollection<CotiEmpresaProtAptitudDocumento> CotiEmpresaProtAptitudDocumento { get; set; }
        public virtual ICollection<CotiEmpresaProtDocumentoFirma> CotiEmpresaProtDocumentoFirma { get; set; }
        public virtual ICollection<CotiEmpresaProtRolDocumento> CotiEmpresaProtRolDocumento { get; set; }
        public virtual ICollection<Documento> Documento { get; set; }
        public virtual ICollection<ProtocoloAptitudDocumento> ProtocoloAptitudDocumento { get; set; }
        public virtual ICollection<ProtocoloDocumentoFirma> ProtocoloDocumentoFirma { get; set; }
        public virtual ICollection<ProtocoloRolDocumento> ProtocoloRolDocumento { get; set; }
    }
}
