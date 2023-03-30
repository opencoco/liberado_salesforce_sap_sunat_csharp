using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Procedimiento
    {
        public Procedimiento()
        {
            ProcedimientoAnotacion = new HashSet<ProcedimientoAnotacion>();
        }

        public long IIdProcedimientoEra { get; set; }
        public DateTime? DtEraInicio { get; set; }
        public DateTime? DtEraFin { get; set; }
        public long IIdConcepto { get; set; }
        public int IIdPaciente { get; set; }
        public int IIdPorQuien { get; set; }
        public int IIdDonde { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Concepto IIdConceptoNavigation { get; set; }
        public virtual Sede IIdDondeNavigation { get; set; }
        public virtual Paciente IIdPacienteNavigation { get; set; }
        public virtual Usuario IIdPorQuienNavigation { get; set; }
        public virtual ICollection<ProcedimientoAnotacion> ProcedimientoAnotacion { get; set; }
    }
}
