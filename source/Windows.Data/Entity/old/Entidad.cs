using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Entidad
    {
        public Entidad()
        {
            EntidadAccion = new HashSet<EntidadAccion>();
            EntidadCampo = new HashSet<EntidadCampo>();
        }

        public int IIdEntidad { get; set; }
        public string VcNombre { get; set; }

        public virtual ICollection<EntidadAccion> EntidadAccion { get; set; }
        public virtual ICollection<EntidadCampo> EntidadCampo { get; set; }
    }
}
