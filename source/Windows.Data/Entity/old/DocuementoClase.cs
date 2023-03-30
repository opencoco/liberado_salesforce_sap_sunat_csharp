using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DocuementoClase
    {
        public DocuementoClase()
        {
            DocumentoSubclase = new HashSet<DocumentoSubclase>();
            PermisoClaseDocumento = new HashSet<PermisoClaseDocumento>();
        }

        public int IIdClaseDocumento { get; set; }
        public string VcNombre { get; set; }
        public int? IIdServicio { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcDescripcion { get; set; }

        public virtual Servicio IIdServicioNavigation { get; set; }
        public virtual ICollection<DocumentoSubclase> DocumentoSubclase { get; set; }
        public virtual ICollection<PermisoClaseDocumento> PermisoClaseDocumento { get; set; }
    }
}
