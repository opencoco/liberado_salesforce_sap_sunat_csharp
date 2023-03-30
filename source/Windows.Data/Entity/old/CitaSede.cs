using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaSede
    {
        public int IIdCita { get; set; }
        public int IIdSede { get; set; }

        public virtual Cita IIdCitaNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
    }
}
