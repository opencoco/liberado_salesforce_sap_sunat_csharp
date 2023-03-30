using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiClientePruebaAdd
    {
        public int IIdCotiCpruebaAdd { get; set; }
        public int IIdProtocoloPlan { get; set; }
        public int IIdPrueba { get; set; }
        public int IIdCotiCliente { get; set; }
        public bool? BObligatorio { get; set; }
        public short SiOrden { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual CotiCliente IIdCotiClienteNavigation { get; set; }
    }
}
