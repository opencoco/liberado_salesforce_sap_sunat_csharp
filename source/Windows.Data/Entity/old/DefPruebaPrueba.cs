using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaPrueba
    {
        public int IIdPruebaPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiTipo { get; set; }
        public string BGeneraEntregable { get; set; }
        public byte? TiOrden { get; set; }
        public int IIdPruebaPadre { get; set; }
        public int IIdPruebaHija { get; set; }
        public short? SiEstado { get; set; }

        public virtual DefPrueba IIdPruebaHijaNavigation { get; set; }
        public virtual DefPrueba IIdPruebaPadreNavigation { get; set; }
    }
}
