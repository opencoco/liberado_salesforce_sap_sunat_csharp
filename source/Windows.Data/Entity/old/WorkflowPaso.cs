using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class WorkflowPaso
    {
        public WorkflowPaso()
        {
            InverseIProximoPasoAprobacionNavigation = new HashSet<WorkflowPaso>();
            InverseIProximoPasoRechazoNavigation = new HashSet<WorkflowPaso>();
            WorkflowTarea = new HashSet<WorkflowTarea>();
        }

        public int IIdWorkflowPaso { get; set; }
        public int? IProximoPasoRechazo { get; set; }
        public int? IProximoPasoAprobacion { get; set; }
        public int IIdWorkflowConfiguracion { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short SiDuracionEnDias { get; set; }
        public bool? BParalelo { get; set; }
        public string VcAsignarARolesCodigo { get; set; }
        public byte? TiOrden { get; set; }
        public string NvFuncion { get; set; }

        public virtual WorkflowConfiguracion IIdWorkflowConfiguracionNavigation { get; set; }
        public virtual WorkflowPaso IProximoPasoAprobacionNavigation { get; set; }
        public virtual WorkflowPaso IProximoPasoRechazoNavigation { get; set; }
        public virtual ICollection<WorkflowPaso> InverseIProximoPasoAprobacionNavigation { get; set; }
        public virtual ICollection<WorkflowPaso> InverseIProximoPasoRechazoNavigation { get; set; }
        public virtual ICollection<WorkflowTarea> WorkflowTarea { get; set; }
    }
}
