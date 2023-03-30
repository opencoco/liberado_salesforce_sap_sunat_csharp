using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Visita
    {
        public long IIdVisitaEra { get; set; }
        public DateTime? DtEraInicio { get; set; }
        public DateTime? DtEraFin { get; set; }
        public long IIdConcepto { get; set; }
        public int IIdPaciente { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
    }
}
