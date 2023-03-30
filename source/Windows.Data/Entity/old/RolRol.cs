using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class RolRol
    {
        public int IIdRolRol { get; set; }
        public int ChildRoleId { get; set; }
        public int ParentRoleId { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Rol ChildRole { get; set; }
        public virtual Rol ParentRole { get; set; }
    }
}
