using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class MatrizInfoPruebaCampo
    {
        public int IIdMatrizInforPruebaCampo { get; set; }
        public short SiOrden { get; set; }
        public int IIdCampo { get; set; }
        public int IIdMatrizInfoPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual DefFichaCampo IIdCampoNavigation { get; set; }
        public virtual MatrizInfoPrueba IIdMatrizInfoPruebaNavigation { get; set; }
    }
}
