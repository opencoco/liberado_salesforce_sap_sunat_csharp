using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaRecomendacion
    {
        public DefPruebaRecomendacion()
        {
            DefPruebaRecomendacionControl = new HashSet<DefPruebaRecomendacionControl>();
        }

        public int IIdPruebaRecomendacion { get; set; }
        public string VcRecomendacion { get; set; }
        public string VcDescripcion { get; set; }
        public short SiSeveridad { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdPruebaDiagnostico { get; set; }
        public short? SiEstado { get; set; }
        public short? SiControl { get; set; }

        public virtual DefPruebaDiagnostico IIdPruebaDiagnosticoNavigation { get; set; }
        public virtual ICollection<DefPruebaRecomendacionControl> DefPruebaRecomendacionControl { get; set; }
    }
}
