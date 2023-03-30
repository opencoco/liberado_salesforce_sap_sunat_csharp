using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaUnidadUsuario
    {
        public int IIdEmpresaUnidadUsu { get; set; }
        public int IIdUsuario { get; set; }
        public int IIdEmpresaUnidad { get; set; }
        public byte? TiTipo { get; set; }
        public int? IIdRol { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiTipo { get; set; }
        public short? SiEstado { get; set; }

        public virtual EmpresaUnidad IIdEmpresaUnidadNavigation { get; set; }
        public virtual Rol IIdRolNavigation { get; set; }
        public virtual Usuario IIdUsuarioNavigation { get; set; }
    }
}
