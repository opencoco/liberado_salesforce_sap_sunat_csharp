using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class WorkflowConfiguracion
    {
        public WorkflowConfiguracion()
        {
            Workflow = new HashSet<Workflow>();
            WorkflowPaso = new HashSet<WorkflowPaso>();
        }

        public int IIdWorkflowConfiguracion { get; set; }
        public string VcNombre { get; set; }
        public short? SiTipo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<Workflow> Workflow { get; set; }
        public virtual ICollection<WorkflowPaso> WorkflowPaso { get; set; }
    }
}
