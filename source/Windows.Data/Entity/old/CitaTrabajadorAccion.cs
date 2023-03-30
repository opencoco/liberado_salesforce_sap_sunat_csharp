using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaTrabajadorAccion
    {
        public int IIdCitaTrabajadorAccion { get; set; }
        public short SiConfirmado { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdCitaTrabajador { get; set; }
        public short? SiRealizadoPor { get; set; }

        public virtual CitaTrabajador IIdCitaTrabajadorNavigation { get; set; }
    }
}
