using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaEquipo
    {
        public int IIdPruebaEquipo { get; set; }
        public decimal? DeTiempoRealizacionMinutos { get; set; }
        public int IIdEquipo { get; set; }
        public int IIdPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Equipo IIdEquipoNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
