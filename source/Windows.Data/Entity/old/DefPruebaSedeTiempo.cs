using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaSedeTiempo
    {
        public int IIdPruebaSedeTiempo { get; set; }
        public decimal? DeTiempoEspera { get; set; }
        public decimal? DeTiempoDuracion { get; set; }
        public decimal? DeTiempoObtenerResult { get; set; }
        public short SiTiempoEsperaUm { get; set; }
        public short SiTiempoDuracionUm { get; set; }
        public short SiTiempoObtenerResultUm { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdSede { get; set; }
        public int IIdPrueba { get; set; }

        public virtual DefPrueba IIdPruebaNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
    }
}
