using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class RoutingRol
    {
        public int IIdRoutingRol { get; set; }
        public int IIdRouting { get; set; }
        public int IIdRol { get; set; }

        public virtual Rol IIdRolNavigation { get; set; }
        public virtual Routing IIdRoutingNavigation { get; set; }
    }
}
