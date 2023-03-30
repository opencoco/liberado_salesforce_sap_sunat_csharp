using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CondicionAnotacion
    {
        public long IIdCondicionAnotacion { get; set; }
        public long IIdCondicionEra { get; set; }
        public string VcIdCie10 { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public bool? BRestriccion { get; set; }
        public DateTime? DtControl { get; set; }
        public int? IIdPrueba { get; set; }

        public virtual Condicion IIdCondicionEraNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
        public virtual Cie10 VcIdCie10Navigation { get; set; }
    }
}
