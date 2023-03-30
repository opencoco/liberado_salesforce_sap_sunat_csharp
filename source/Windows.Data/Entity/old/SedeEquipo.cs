using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeEquipo
    {
        public int IIdSedeEquipo { get; set; }
        public int IIdSede { get; set; }
        public int IIdEquipo { get; set; }
        public string VcMarca { get; set; }
        public string VcModelo { get; set; }
        public string VcSerie { get; set; }
        public DateTime? DtUltimaCalibracion { get; set; }
        public DateTime? DtInicioOperaciones { get; set; }
        public DateTime? DtFabricacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual Equipo IIdEquipoNavigation { get; set; }
    }
}
