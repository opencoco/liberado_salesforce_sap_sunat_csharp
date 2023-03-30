using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Cliente
    {
        public int IIdClienteRelacion { get; set; }
        public int IIdEmpresaPadre { get; set; }
        public int IIdEmpresaHijo { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Empresa IIdEmpresaHijoNavigation { get; set; }
        public virtual Empresa IIdEmpresaPadreNavigation { get; set; }
    }
}
