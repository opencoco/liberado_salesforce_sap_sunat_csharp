using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ComboActividadeconomica
    {
        public int IIdComboActividadEco { get; set; }
        public int? IIdCombo { get; set; }
        public int? IIdActividadEconomicai { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual ActividadEconomica IIdActividadEconomicaiNavigation { get; set; }
        public virtual Combo IIdComboNavigation { get; set; }
    }
}
