using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Combo
    {
        public Combo()
        {
            ComboActividadeconomica = new HashSet<ComboActividadeconomica>();
            ComboProtocolo = new HashSet<ComboProtocolo>();
            CotiEmpresa = new HashSet<CotiEmpresa>();
        }

        public int IIdCombo { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short? SiTipo { get; set; }
        public short? SiEstado { get; set; }
        public bool? BResultadoEnDigital { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<ComboActividadeconomica> ComboActividadeconomica { get; set; }
        public virtual ICollection<ComboProtocolo> ComboProtocolo { get; set; }
        public virtual ICollection<CotiEmpresa> CotiEmpresa { get; set; }
    }
}
