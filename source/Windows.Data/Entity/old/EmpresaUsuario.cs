using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaUsuario
    {
        public int IIdEmpresaUsu { get; set; }
        public int IIdEmpresa { get; set; }
        public int IIdUsuario { get; set; }
        public int? IIdRol { get; set; }
        public short? SiTipo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Rol IIdRolNavigation { get; set; }
        public virtual Usuario IIdUsuarioNavigation { get; set; }
    }
}
