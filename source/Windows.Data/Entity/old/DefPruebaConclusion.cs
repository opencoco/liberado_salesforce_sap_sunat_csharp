using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaConclusion
    {
        public int IIdPruebaConclusion { get; set; }
        public string VcConclusion { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdPruebaDiagnostico { get; set; }
        public short? SiEstado { get; set; }

        public virtual DefPruebaDiagnostico IIdPruebaDiagnosticoNavigation { get; set; }
    }
}
