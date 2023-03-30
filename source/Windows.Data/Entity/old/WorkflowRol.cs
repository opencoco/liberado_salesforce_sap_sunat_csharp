using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class WorkflowRol
    {
        public int IIdWorkflowRol { get; set; }
        public string VcRolNombre { get; set; }
        public string VcRolCodigo { get; set; }
        public int IIdUsuario { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Usuario IIdUsuarioNavigation { get; set; }
    }
}
