using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Ruta
    {
        public Ruta()
        {
            PermisoRuta = new HashSet<PermisoRuta>();
            RutaRutaIIdRutaHijoNavigation = new HashSet<RutaRuta>();
            RutaRutaIIdRutaPadreNavigation = new HashSet<RutaRuta>();
        }

        public int IIdRuta { get; set; }
        public string VcRuta { get; set; }
        public string VcNombre { get; set; }
        public bool? BWeb { get; set; }
        public bool? BLinkeable { get; set; }
        public bool? BAgrupador { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<PermisoRuta> PermisoRuta { get; set; }
        public virtual ICollection<RutaRuta> RutaRutaIIdRutaHijoNavigation { get; set; }
        public virtual ICollection<RutaRuta> RutaRutaIIdRutaPadreNavigation { get; set; }
    }
}
