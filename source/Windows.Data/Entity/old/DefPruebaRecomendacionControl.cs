using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaRecomendacionControl
    {
        public int IIdPruebaRecControl { get; set; }
        public short? SiPeriodicidad { get; set; }
        public int IIdPruebaRecomendacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual DefPruebaRecomendacion IIdPruebaRecomendacionNavigation { get; set; }
    }
}
