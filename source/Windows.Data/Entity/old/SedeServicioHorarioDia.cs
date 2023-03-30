using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeServicioHorarioDia
    {
        public SedeServicioHorarioDia()
        {
            SedeServicioHorarioTurno = new HashSet<SedeServicioHorarioTurno>();
        }

        public int IIdSedeServicioHorarioDia { get; set; }
        public short SiDia { get; set; }
        public bool? BAbierto { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdSedeServicioHorario { get; set; }

        public virtual SedeServicioHorario IIdSedeServicioHorarioNavigation { get; set; }
        public virtual ICollection<SedeServicioHorarioTurno> SedeServicioHorarioTurno { get; set; }
    }
}
