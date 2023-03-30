using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class HorarioEstandarTurno
    {
        public int IIdHorarioEstandarTurno { get; set; }
        public int IIdHorarioEstandarConf { get; set; }
        public DateTime DtHoraIniico { get; set; }
        public DateTime DtHoraFin { get; set; }
        public short SiNumeroSalas { get; set; }
        public short SiAforoPorSala { get; set; }
        public short SiDuracion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }

        public virtual HorarioEstandarDia IIdHorarioEstandarConfNavigation { get; set; }
    }
}
