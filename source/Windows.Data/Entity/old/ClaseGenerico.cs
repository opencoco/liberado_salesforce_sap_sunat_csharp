using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ClaseGenerico
    {
        public ClaseGenerico()
        {
            ClaseGenericaData = new HashSet<ClaseGenericaData>();
        }

        public string IIdClaseGenerico { get; set; }
        public string VcNombre { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<ClaseGenericaData> ClaseGenericaData { get; set; }
    }
}
