using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaFicha
    {
        public int IIdPruebaFicha { get; set; }
        public byte? TiOrden { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdFicha { get; set; }
        public int IIdPrueba { get; set; }
        public short? SiEstado { get; set; }

        public virtual DefFicha IIdFichaNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
