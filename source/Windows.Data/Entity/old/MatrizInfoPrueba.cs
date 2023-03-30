using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class MatrizInfoPrueba
    {
        public MatrizInfoPrueba()
        {
            MatrizInfoPruebaCampo = new HashSet<MatrizInfoPruebaCampo>();
        }

        public int IIdMatrizInfoPrueba { get; set; }
        public short SiOrden { get; set; }
        public int IIdPrueba { get; set; }
        public int IIdProtocoloMatrizInfo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual MatrizInfo IIdProtocoloMatrizInfoNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
        public virtual ICollection<MatrizInfoPruebaCampo> MatrizInfoPruebaCampo { get; set; }
    }
}
