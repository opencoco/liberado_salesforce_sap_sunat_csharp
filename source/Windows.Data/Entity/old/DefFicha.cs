using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefFicha
    {
        public DefFicha()
        {
            DefFichaCampo = new HashSet<DefFichaCampo>();
            DefFichaCampoOpcion = new HashSet<DefFichaCampoOpcion>();
            DefPruebaFicha = new HashSet<DefPruebaFicha>();
            PrecedimientoFicha = new HashSet<ProcedimientoFicha>();
        }

        public int IIdFicha { get; set; }
        public string VcNombre { get; set; }
        public bool? BPrincipal { get; set; }
        public short SiTipo { get; set; }
        public short? SiEstado { get; set; }
        public string VcDescripcion { get; set; }
        public short SiTipoPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<DefFichaCampo> DefFichaCampo { get; set; }
        public virtual ICollection<DefFichaCampoOpcion> DefFichaCampoOpcion { get; set; }
        public virtual ICollection<DefPruebaFicha> DefPruebaFicha { get; set; }
        public virtual ICollection<ProcedimientoFicha> PrecedimientoFicha { get; set; }
    }
}
