using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PermisoAutomatizado
    {
        public PermisoAutomatizado()
        {
            PermisoAutomatizadoRol = new HashSet<PermisoAutomatizadoRol>();
        }

        public int IIdPermisoAutonatizado { get; set; }
        public string VcApi { get; set; }
        public string VcMetodo { get; set; }
        public string VcRedirect { get; set; }
        public bool BPermiso { get; set; }

        public virtual ICollection<PermisoAutomatizadoRol> PermisoAutomatizadoRol { get; set; }
    }
}
