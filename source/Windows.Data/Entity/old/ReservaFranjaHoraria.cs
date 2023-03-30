using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ReservaFranjaHoraria
    {
        public int IIdReservaFranjaHoraria { get; set; }
        public int IIdSede { get; set; }
        public int IIdEmpresa { get; set; }
        public short SiPara { get; set; }
        public DateTime? DtDesde { get; set; }
        public DateTime? DtHasta { get; set; }
        public DateTime DtHoraInicio { get; set; }
        public DateTime? DtHoraFin { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public short SiEstado { get; set; }

        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
    }
}
