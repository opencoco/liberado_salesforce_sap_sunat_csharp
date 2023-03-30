using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class HorarioEspecial
    {
        public int IIdHorarioEspecial { get; set; }
        public DateTime DtFecha { get; set; }
        public bool BAbierto { get; set; }
        public DateTime? DtHoraInicio { get; set; }
        public DateTime? DtHoraFin { get; set; }
        public short? SiNumeroSalas { get; set; }
        public short? SiAforoPorSalas { get; set; }
        public short? SiDuracion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdSede { get; set; }
        public short SiEstado { get; set; }

        public virtual Sede IIdSedeNavigation { get; set; }
    }
}
