using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProcedimientoCampo
    {
        public long IIdProcedimientoFicha { get; set; }
        public long IIdProcedimientoCampo { get; set; }
        public int IIdCampo { get; set; }
        public string VcNombre { get; set; }
        public int? IIdDocumento { get; set; }
        public string VcValor { get; set; }
        public decimal? DeValor { get; set; }
        public DateTime? DtValor { get; set; }
        public DateTime? DtCreacion { get; set; }
        public string DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual DefFichaCampo IIdCampoNavigation { get; set; }
        public virtual Documento IIdDocumentoNavigation { get; set; }
        public virtual ProcedimientoFicha IIdProcedimientoFichaNavigation { get; set; }
    }
}
