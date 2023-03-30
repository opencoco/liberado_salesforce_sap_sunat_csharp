using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaEspecialista
    {
        public int IIdPruebaEspecialista { get; set; }
        public bool? BRealiza { get; set; }
        public bool? BValida { get; set; }
        public int IIdPrueba { get; set; }
        public int IIdEspecialidadMedica { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual EspecialidadMedica IIdEspecialidadMedicaNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
