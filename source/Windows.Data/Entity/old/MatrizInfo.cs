using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class MatrizInfo
    {
        public MatrizInfo()
        {
            MatrizInfoPrueba = new HashSet<MatrizInfoPrueba>();
            ProtocoloRolMatrizInfo = new HashSet<ProtocoloRolMatrizInfo>();
        }

        public int IIdMatrizInformacion { get; set; }
        public string VcNombre { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual ICollection<MatrizInfoPrueba> MatrizInfoPrueba { get; set; }
        public virtual ICollection<ProtocoloRolMatrizInfo> ProtocoloRolMatrizInfo { get; set; }
    }
}
