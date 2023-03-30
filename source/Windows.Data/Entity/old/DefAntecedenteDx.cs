using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefAntecedenteDx
    {
        public int IIdAntecedenteDx { get; set; }
        public string VcIdCie10 { get; set; }
        public string VcInterpretacion { get; set; }
        public string VcNombre { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Cie10 VcIdCie10Navigation { get; set; }
    }
}
