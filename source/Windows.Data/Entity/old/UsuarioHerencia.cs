using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class UsuarioHerencia
    {
        public int IIdRelacionUsuario { get; set; }
        public int ParentUser { get; set; }
        public int ChildUser { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Usuario ChildUserNavigation { get; set; }
        public virtual Usuario ParentUserNavigation { get; set; }
    }
}
