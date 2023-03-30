using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class MiHistoria
    {
        public long IIdMiHistoriaKey { get; set; }
        public string VcReferenceKey { get; set; }
        public int IIdUsuario { get; set; }
        public short SiTipo { get; set; }
        public string VcConcepto { get; set; }
        public DateTime DtFechaHoraHistoria { get; set; }
        public DateTime? DtFechaLimite { get; set; }
        public string VcSede { get; set; }
        public string VcResultado { get; set; }
        public string VcTitular { get; set; }
        public string VcColorBola { get; set; }
        public string VcGuid { get; set; }
        public short SiEstado { get; set; }
        public int? IFicha { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
    }
}
