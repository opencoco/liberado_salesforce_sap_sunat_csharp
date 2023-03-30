using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Workflow
    {
        public Workflow()
        {
            WorkflowTarea = new HashSet<WorkflowTarea>();
        }

        public int IIdWorkflow { get; set; }
        public int IIdWorkflowConfiguracion { get; set; }
        public int? IIdEmpresa { get; set; }
        public int? IIdPaciente { get; set; }
        public int IIdWorkflowEstado { get; set; }
        public int? IIdCotiEmpresa { get; set; }
        public int? IIdCotiCliente { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdCitaTrabajador { get; set; }

        public virtual CotiCliente IIdCotiClienteNavigation { get; set; }
        public virtual CotiEmpresa IIdCotiEmpresaNavigation { get; set; }
        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Paciente IIdPacienteNavigation { get; set; }
        public virtual WorkflowConfiguracion IIdWorkflowConfiguracionNavigation { get; set; }
        public virtual WorkflowEstado IIdWorkflowEstadoNavigation { get; set; }
        public virtual ICollection<WorkflowTarea> WorkflowTarea { get; set; }
    }
}
