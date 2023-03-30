using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeZonaArea
    {
        public int IIdSedeZonaArea { get; set; }
        public int IIdSede { get; set; }
        public int? IIdZona { get; set; }
        public int? IIdArea { get; set; }

        public virtual Area IIdAreaNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
        public virtual Zona IIdZonaNavigation { get; set; }
    }
}
