using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeZona
    {
        public int IIdSedeZona { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdSede { get; set; }
        public int IIdZona { get; set; }

        public virtual Sede IIdSedeNavigation { get; set; }
        public virtual Zona IIdZonaNavigation { get; set; }
    }
}
