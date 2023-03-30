using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class MedicamentoAnotacion
    {
        public long IIdMedicamentoEra { get; set; }
        public long IIdMedicamento { get; set; }
        public string VcNombreMedicamento { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdPrueba { get; set; }

        public virtual Medicamento IIdMedicamentoEraNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
