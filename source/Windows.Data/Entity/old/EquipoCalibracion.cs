using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EquipoCalibracion
    {
        public int IIdEquipoCali { get; set; }
        public short SiCantidadPruebas { get; set; }
        public short SiTiempoDias { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdEquipo { get; set; }

        public virtual Equipo IIdEquipoNavigation { get; set; }
    }
}
