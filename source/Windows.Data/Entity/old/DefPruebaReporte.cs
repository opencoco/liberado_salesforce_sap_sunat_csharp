using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaReporte
    {
        public int IIdPruebaReporte { get; set; }
        public string VcImpactaaReporte { get; set; }
        public string VcReporteQueGenera { get; set; }
        public int? IIdFirmadoPorEspecialista { get; set; }
        public int IIdPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdMensajePlantilla { get; set; }
        public bool BIncluirFirma { get; set; }
        public short SiEstado { get; set; }

        public virtual EspecialidadMedica IIdFirmadoPorEspecialistaNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
