using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaUsuarioRol
    {
        public int IIdEmpresaUsuRol { get; set; }
        public int? IIdEmpresaUsu { get; set; }
        public int? IIdUsuario { get; set; }
        public int IIdRol { get; set; }
    }
}
