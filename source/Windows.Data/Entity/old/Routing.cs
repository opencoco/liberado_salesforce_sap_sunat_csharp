using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Routing
    {
        public Routing()
        {
            InverseIIdRoutingPadreNavigation = new HashSet<Routing>();
            RoutingRol = new HashSet<RoutingRol>();
        }

        public int IIdRouting { get; set; }
        public string VcNombre { get; set; }
        public bool BAgrupador { get; set; }
        public string VcRouting { get; set; }
        public int? IIdRoutingPadre { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public bool? BAdmin { get; set; }
        public short? SiOrden { get; set; }

        public virtual Routing IIdRoutingPadreNavigation { get; set; }
        public virtual ICollection<Routing> InverseIIdRoutingPadreNavigation { get; set; }
        public virtual ICollection<RoutingRol> RoutingRol { get; set; }
    }
}
