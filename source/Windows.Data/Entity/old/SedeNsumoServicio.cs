using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeNsumoServicio
    {
        public int IIdServicio { get; set; }
        public int IIdSedeInsumo { get; set; }

        public virtual SedeInsumo IIdSedeInsumoNavigation { get; set; }
        public virtual Servicio IIdServicioNavigation { get; set; }
    }
}
