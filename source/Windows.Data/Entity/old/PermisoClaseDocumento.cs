using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoClaseDocumento
    {
        public int IIdPermClaseDoc { get; set; }
        public int IIdPermiso { get; set; }
        public int IIdClaseDocumento { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual DocuementoClase IIdClaseDocumentoNavigation { get; set; }
        public virtual Permiso IIdPermisoNavigation { get; set; }
    }
}
