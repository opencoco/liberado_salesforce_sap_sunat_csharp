using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeServicioHorario
    {
        public SedeServicioHorario()
        {
            SedeServicioHorarioDia = new HashSet<SedeServicioHorarioDia>();
        }

        public int IIdSedeServicioHorario { get; set; }
        public int IIdSede { get; set; }
        public int IIdServicio { get; set; }
        public int? InCitaMinima { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual Sede IIdSedeNavigation { get; set; }
        public virtual Servicio IIdServicioNavigation { get; set; }
        public virtual ICollection<SedeServicioHorarioDia> SedeServicioHorarioDia { get; set; }
    }
}
