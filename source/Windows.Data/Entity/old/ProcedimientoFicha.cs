using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProcedimientoFicha
    {
        public ProcedimientoFicha()
        {
            ProcedimientoCampo = new HashSet<ProcedimientoCampo>();
        }

        public long IIdProcedimiento { get; set; }
        public long IIdProcedimientoFicha { get; set; }
        public int IIdFicha { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual DefFicha IIdFichaNavigation { get; set; }
        public virtual ProcedimientoAnotacion IIdProcedimientoNavigation { get; set; }
        public virtual ICollection<ProcedimientoCampo> ProcedimientoCampo { get; set; }
    }
}
