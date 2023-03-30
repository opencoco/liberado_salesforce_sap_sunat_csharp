using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaConceptoFacturable
    {
        public CitaConceptoFacturable()
        {
            ProtocoloConceptoFacturable = new HashSet<ProtocoloConceptoFacturable>();
        }

        public int IIdConceptoFacturable { get; set; }
        public decimal? DePrecio { get; set; }
        public string VcNombre { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiTipo { get; set; }

        public virtual ICollection<ProtocoloConceptoFacturable> ProtocoloConceptoFacturable { get; set; }
    }
}
