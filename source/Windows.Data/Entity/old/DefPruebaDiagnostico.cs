using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaDiagnostico
    {
        public DefPruebaDiagnostico()
        {
            DefPruebaConclusion = new HashSet<DefPruebaConclusion>();
            DefPruebaRecomendacion = new HashSet<DefPruebaRecomendacion>();
        }

        public int IIdPruebaDiagnostico { get; set; }
        public byte? TiTipo { get; set; }
        public string VcDescripcion { get; set; }
        public string VcInterpretacion { get; set; }
        public string VcFormula { get; set; }
        public string VcIdCie10 { get; set; }
        public int IIdPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual DefPrueba IIdPruebaNavigation { get; set; }
        public virtual Cie10 VcIdCie10Navigation { get; set; }
        public virtual ICollection<DefPruebaConclusion> DefPruebaConclusion { get; set; }
        public virtual ICollection<DefPruebaRecomendacion> DefPruebaRecomendacion { get; set; }
    }
}
