using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class UsuarioRol
    {
        public int IIdUsuarioRol { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdRol { get; set; }
        public int IIdUsuario { get; set; }

        public virtual Rol IIdRolNavigation { get; set; }
        public virtual Usuario IIdUsuarioNavigation { get; set; }
    }
}
