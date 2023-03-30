using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaSedeZonaAreaServicio
    {
        public int IIdEmpSedeZonaAreaServ { get; set; }
        public int IIdSedeZonaAreaServ { get; set; }
        public int IIdPrueba { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual DefPrueba IIdPruebaNavigation { get; set; }
        public virtual SedeZonaAreaServicio IIdSedeZonaAreaServNavigation { get; set; }
    }
}
