using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class RutaRuta
    {
        public int IIdRutaRelacion { get; set; }
        public int IIdRutaPadre { get; set; }
        public int IIdRutaHijo { get; set; }

        public virtual Ruta IIdRutaHijoNavigation { get; set; }
        public virtual Ruta IIdRutaPadreNavigation { get; set; }
    }
}
