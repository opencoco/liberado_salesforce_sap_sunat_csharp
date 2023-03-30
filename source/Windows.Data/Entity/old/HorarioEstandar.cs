using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class HorarioEstandar
    {
        public HorarioEstandar()
        {
            HorarioEstandarDia = new HashSet<HorarioEstandarDia>();
        }

        public int IIdHorarioEstandar { get; set; }
        public string VcNombre { get; set; }
        public short? SiEstado { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }

        public virtual ICollection<HorarioEstandarDia> HorarioEstandarDia { get; set; }
    }
}
