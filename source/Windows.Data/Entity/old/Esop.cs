using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Esop
    {
        public int IIdEsop { get; set; }
        public int InEsop { get; set; }
        public string VcAgenteDeRiesgo { get; set; }
        public string VcDescripcion { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public bool? BCancerigeno { get; set; }
        public string VcCas { get; set; }
        public bool? BSemestral { get; set; }
        public string VcObservaciones { get; set; }
        public short? SiTipoAgente { get; set; }
    }
}
