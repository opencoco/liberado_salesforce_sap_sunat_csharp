using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoRuta
    {
        public int IIdPermisoRuta { get; set; }
        public int IIdRuta { get; set; }
        public int IIdPermiso { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Permiso IIdPermisoNavigation { get; set; }
        public virtual Ruta IIdRutaNavigation { get; set; }
    }
}
