using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class WorkflowEstado
    {
        public WorkflowEstado()
        {
            Workflow = new HashSet<Workflow>();
        }

        public int IIdWorkflowEstado { get; set; }
        public string VcNombre { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdWorkflowConfiguracion { get; set; }

        public virtual ICollection<Workflow> Workflow { get; set; }
    }
}
