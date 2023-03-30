using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Proveedor
    {
        public int IIdProveedorRelacion { get; set; }
        public int IIdEmpresaPadre { get; set; }
        public int IIdEmpresaHijo { get; set; }
        public short? SiEstado { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public bool? BPuedeCargarFactura { get; set; }

        public virtual Empresa IIdEmpresaHijoNavigation { get; set; }
        public virtual Empresa IIdEmpresaPadreNavigation { get; set; }
    }
}
