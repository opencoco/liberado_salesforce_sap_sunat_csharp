using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Cpt
    {
        public Cpt()
        {
            DefPrueba = new HashSet<DefPrueba>();
        }

        public int IIdCpt { get; set; }
        public string VcNombre { get; set; }
        public string VcCodigo { get; set; }
        public string VcDescripcion { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public bool? BEsOcupacional { get; set; }

        public virtual ICollection<DefPrueba> DefPrueba { get; set; }
    }
}
