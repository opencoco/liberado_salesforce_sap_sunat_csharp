using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EntidadAccion
    {
        public EntidadAccion()
        {
            PermisoEntidadAccion = new HashSet<PermisoEntidadAccion>();
        }

        public int IIdEntidadAccion { get; set; }
        public short SiTipo { get; set; }
        public int IIdEntidad { get; set; }

        public virtual Entidad IIdEntidadNavigation { get; set; }
        public virtual ICollection<PermisoEntidadAccion> PermisoEntidadAccion { get; set; }
    }
}
