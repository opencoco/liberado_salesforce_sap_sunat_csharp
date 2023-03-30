using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Antecedente
    {
        public int IIdAntecedente { get; set; }
        public DateTime? DtDesde { get; set; }
        public DateTime? DtHasta { get; set; }
        public string VcIdCie10 { get; set; }
        public int IIdPaciente { get; set; }
        public bool? BConfirmado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual Paciente IIdPacienteNavigation { get; set; }
        public virtual Cie10 VcIdCie10Navigation { get; set; }
    }
}
