using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoEntidadCampoExclusion
    {
        public int IIdPermEntCamExc { get; set; }
        public int IIdEntidadCampo { get; set; }
        public int IIdPermiso { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual EntidadCampo IIdEntidadCampoNavigation { get; set; }
        public virtual Permiso IIdPermisoNavigation { get; set; }
    }
}
