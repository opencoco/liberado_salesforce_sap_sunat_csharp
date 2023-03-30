using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class TipoPrueba
    {
        public TipoPrueba()
        {
            Protocolo = new HashSet<Protocolo>();
        }

        public int IIdTipoExamen { get; set; }
        public string VcNombre { get; set; }
        public string VcCodigoMigracion { get; set; }
        public short? SiEstado { get; set; }
        public bool? BEsEspecial { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<Protocolo> Protocolo { get; set; }
    }
}
