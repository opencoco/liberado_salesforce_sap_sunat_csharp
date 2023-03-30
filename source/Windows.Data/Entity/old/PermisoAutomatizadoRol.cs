using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoAutomatizadoRol
    {
        public int IIdPermisoAutonatizadoRol { get; set; }
        public int IIdPermisoAutonatizado { get; set; }
        public int IIdRol { get; set; }

        public virtual PermisoAutomatizado IIdPermisoAutonatizadoNavigation { get; set; }
        public virtual Rol IIdRolNavigation { get; set; }
    }
}
