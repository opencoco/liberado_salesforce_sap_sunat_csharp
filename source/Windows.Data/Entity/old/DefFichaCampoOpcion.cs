using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefFichaCampoOpcion
    {
        public int IIdFichaCampoOpcion { get; set; }
        public decimal IId { get; set; }
        public string VcNombre { get; set; }
        public int IIdFicha { get; set; }
        public int IIdCampo { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public short? SiOperacion { get; set; }
        public int? IIdCampoDependiente { get; set; }

        public virtual DefFichaCampo IIdCampoNavigation { get; set; }
        public virtual DefFicha IIdFichaNavigation { get; set; }
    }
}
