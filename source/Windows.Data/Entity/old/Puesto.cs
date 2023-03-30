using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Puesto
    {
        public Puesto()
        {
            EmpresaPuesto = new HashSet<EmpresaPuesto>();
        }

        public int IIdPuesto { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public int? IIdCiuo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdArea { get; set; }
        public short? SiEstado { get; set; }
        public bool? BExpuesto { get; set; }
        public short? SiGrupoPuesto { get; set; }
        public string VcCodigoMigracion { get; set; }

        public virtual Area IIdAreaNavigation { get; set; }
        public virtual ICollection<EmpresaPuesto> EmpresaPuesto { get; set; }
    }
}
