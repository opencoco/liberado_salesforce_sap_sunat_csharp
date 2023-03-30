using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoUrl
    {
        public PermisoUrl()
        {
            PermisoUrlRol = new HashSet<PermisoUrlRol>();
        }

        public int IIdPermisoUrl { get; set; }
        public bool BWeb { get; set; }
        public string VcModulo { get; set; }
        public int IOrden { get; set; }
        public string VcUrl { get; set; }
        public string VcRedirect { get; set; }

        public virtual ICollection<PermisoUrlRol> PermisoUrlRol { get; set; }
    }
}
