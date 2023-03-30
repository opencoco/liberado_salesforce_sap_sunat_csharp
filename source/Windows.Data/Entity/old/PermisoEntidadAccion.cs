using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoEntidadAccion
    {
        public int IIdPermEntAccion { get; set; }
        public int IIdPermiso { get; set; }
        public int IIdEntidadAccion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual EntidadAccion IIdEntidadAccionNavigation { get; set; }
        public virtual Permiso IIdPermisoNavigation { get; set; }
    }
}
