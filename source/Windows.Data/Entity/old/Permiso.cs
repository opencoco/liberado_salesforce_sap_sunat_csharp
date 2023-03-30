using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Permiso
    {
        public Permiso()
        {
            PermisoClaseDocumento = new HashSet<PermisoClaseDocumento>();
            PermisoEntidadAccion = new HashSet<PermisoEntidadAccion>();
            PermisoEntidadCampoExclusion = new HashSet<PermisoEntidadCampoExclusion>();
            PermisoRuta = new HashSet<PermisoRuta>();
            RolPermiso = new HashSet<RolPermiso>();
        }

        public int IIdPermiso { get; set; }
        public string VcUuid { get; set; }
        public string VcNombre { get; set; }
        public byte TiTipo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<PermisoClaseDocumento> PermisoClaseDocumento { get; set; }
        public virtual ICollection<PermisoEntidadAccion> PermisoEntidadAccion { get; set; }
        public virtual ICollection<PermisoEntidadCampoExclusion> PermisoEntidadCampoExclusion { get; set; }
        public virtual ICollection<PermisoRuta> PermisoRuta { get; set; }
        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
    }
}
