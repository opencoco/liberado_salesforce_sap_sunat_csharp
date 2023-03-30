using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefGrupoPrueba
    {
        public DefGrupoPrueba()
        {
            DefPrueba = new HashSet<DefPrueba>();
        }

        public int IIdGrupoPrueba { get; set; }
        public string VcCodigoChaman { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual ICollection<DefPrueba> DefPrueba { get; set; }
    }
}
