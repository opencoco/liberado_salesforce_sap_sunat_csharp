using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EntidadCampo
    {
        public EntidadCampo()
        {
            PermisoEntidadCampoExclusion = new HashSet<PermisoEntidadCampoExclusion>();
        }

        public int IIdEntidadCampo { get; set; }
        public int IIdEntidad { get; set; }

        public virtual Entidad IIdEntidadNavigation { get; set; }
        public virtual ICollection<PermisoEntidadCampoExclusion> PermisoEntidadCampoExclusion { get; set; }
    }
}
