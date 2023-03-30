using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class WorkflowTarea
    {
        public int IIdWorkflowTarea { get; set; }
        public int IIdWorkflow { get; set; }
        public int IIdWorkflowPaso { get; set; }
        public int IIdUsuario { get; set; }
        public byte? TiPrioridad { get; set; }
        public string VcDescripcion { get; set; }
        public short? SiEstadoTarea { get; set; }
        public DateTime? DtEjecutado { get; set; }
        public DateTime? DEjecucionEstimada { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcComentario { get; set; }

        public virtual Usuario IIdUsuarioNavigation { get; set; }
        public virtual Workflow IIdWorkflowNavigation { get; set; }
        public virtual WorkflowPaso IIdWorkflowPasoNavigation { get; set; }
    }
}
