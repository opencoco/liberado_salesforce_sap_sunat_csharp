using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoUrlRol
    {
        public int IIdPermisoUrlRol { get; set; }
        public int IIdPermisoUrl { get; set; }
        public int IIdRol { get; set; }

        public virtual PermisoUrl IIdPermisoUrlNavigation { get; set; }
        public virtual Rol IIdRolNavigation { get; set; }
    }
}
