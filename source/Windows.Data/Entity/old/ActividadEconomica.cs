using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ActividadEconomica
    {
        public ActividadEconomica()
        {
            ComboActividadeconomica = new HashSet<ComboActividadeconomica>();
            CotiEmpresa = new HashSet<CotiEmpresa>();
        }

        public int IIdActividadEconomicai { get; set; }
        public string VcCiiucodigo { get; set; }
        public string VcCiiudescripcion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual ICollection<ComboActividadeconomica> ComboActividadeconomica { get; set; }
        public virtual ICollection<CotiEmpresa> CotiEmpresa { get; set; }
    }
}
