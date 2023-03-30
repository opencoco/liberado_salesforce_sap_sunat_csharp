using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeServicioHorarioTurno
    {
        public int IIdSedeServicioHorarioTurno { get; set; }
        public DateTime DtHoraIniico { get; set; }
        public DateTime DtHoraFin { get; set; }
        public short SiNumeroSalas { get; set; }
        public short SiAforoPorSala { get; set; }
        public short SiDuracion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdSedeServicioHorarioDia { get; set; }

        public virtual SedeServicioHorarioDia IIdSedeServicioHorarioDiaNavigation { get; set; }
    }
}
