using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProcedimientoAnotacion
    {
        public ProcedimientoAnotacion()
        {
            PrecedimientoFicha = new HashSet<ProcedimientoFicha>();
            ProcedimientoEntregable = new HashSet<ProcedimientoEntregable>();
        }

        public long IIdProcedimiento { get; set; }
        public long IIdProcedimientoEra { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdPrueba { get; set; }

        public virtual Procedimiento IIdProcedimientoEraNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
        public virtual ICollection<ProcedimientoFicha> PrecedimientoFicha { get; set; }
        public virtual ICollection<ProcedimientoEntregable> ProcedimientoEntregable { get; set; }
    }
}
