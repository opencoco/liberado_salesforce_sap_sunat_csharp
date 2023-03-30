using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ComboProtocolo
    {
        public int IIdComboProtocolo { get; set; }
        public int IIdCombo { get; set; }
        public int IIdProtocolo { get; set; }
        public DateTime? DtCreacion { get; set; }
        public byte? TiOrden { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual Combo IIdComboNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
    }
}
