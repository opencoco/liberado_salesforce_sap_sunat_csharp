using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeInsumo
    {
        public SedeInsumo()
        {
            InsumoLote = new HashSet<InsumoLote>();
            SedeNsumoServicio = new HashSet<SedeNsumoServicio>();
        }

        public int IIdSedeInsumo { get; set; }
        public int IIdSede { get; set; }
        public int IIdInsumo { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public bool? BDescontarenCitas { get; set; }
        public short SiEstado { get; set; }

        public virtual Insumo IIdInsumoNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
        public virtual ICollection<InsumoLote> InsumoLote { get; set; }
        public virtual ICollection<SedeNsumoServicio> SedeNsumoServicio { get; set; }
    }
}
