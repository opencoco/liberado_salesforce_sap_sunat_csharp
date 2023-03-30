using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProcedimientoEntregable
    {
        public long IIdProcedimientoEntregable { get; set; }
        public long IIdProcedimiento { get; set; }
        public int IIdDocumento { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Documento IIdDocumentoNavigation { get; set; }
        public virtual ProcedimientoAnotacion IIdProcedimientoNavigation { get; set; }
    }
}
