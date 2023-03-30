using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaTipoResultado
    {
        public int IIdPruebaTipoResultado { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdTipoResultado { get; set; }
        public int IIdPrueba { get; set; }

        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
