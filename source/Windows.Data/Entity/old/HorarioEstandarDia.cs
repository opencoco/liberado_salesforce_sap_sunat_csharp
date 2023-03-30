using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class HorarioEstandarDia
    {
        public HorarioEstandarDia()
        {
            HorarioEstandarTurno = new HashSet<HorarioEstandarTurno>();
        }

        public int IIdHorarioEstandarConf { get; set; }
        public int IIdHorarioEstandar { get; set; }
        public short SiDia { get; set; }
        public bool? BAbierto { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }

        public virtual HorarioEstandar IIdHorarioEstandarNavigation { get; set; }
        public virtual ICollection<HorarioEstandarTurno> HorarioEstandarTurno { get; set; }
    }
}
