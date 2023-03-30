using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Usuario
    {
        public Usuario()
        {
            Condicion = new HashSet<Condicion>();
            CotiClienteIIdSolicitadoPorNavigation = new HashSet<CotiCliente>();
            CotiClienteIIdVendedorAsignadoNavigation = new HashSet<CotiCliente>();
            CotiEmpresaIIdSolicitadoPorNavigation = new HashSet<CotiEmpresa>();
            CotiEmpresaIIdVendedorAsignadoNavigation = new HashSet<CotiEmpresa>();
            EmpresaUnidadUsuario = new HashSet<EmpresaUnidadUsuario>();
            EmpresaUsuario = new HashSet<EmpresaUsuario>();
            HoldingUsuario = new HashSet<HoldingUsuario>();
            Medicamento = new HashSet<Medicamento>();
            Notificacion = new HashSet<Notificacion>();
            Precedimiento = new HashSet<Procedimiento>();
            SedeEspecialista = new HashSet<SedeEspecialista>();
            SedeEspecialistaRol = new HashSet<SedeEspecialistaRol>();
            UsuarioHerenciaChildUserNavigation = new HashSet<UsuarioHerencia>();
            UsuarioHerenciaParentUserNavigation = new HashSet<UsuarioHerencia>();
            UsuarioPassword = new HashSet<UsuarioPassword>();
            UsuarioRol = new HashSet<UsuarioRol>();
            WorkflowRol = new HashSet<WorkflowRol>();
            WorkflowTarea = new HashSet<WorkflowTarea>();
        }

        public int IIdUsuario { get; set; }
        public string NvProfileSync { get; set; }
        public short? SiEstado { get; set; }
        public string VcEmail { get; set; }
        public string VcCelularPrincipal { get; set; }
        public DateTime? DtActivoDesde { get; set; }
        public int IIdPersona { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string NvUuid { get; set; }
        public string VcTelefono { get; set; }

        public virtual Persona IIdPersonaNavigation { get; set; }
        public virtual ICollection<Condicion> Condicion { get; set; }
        public virtual ICollection<CotiCliente> CotiClienteIIdSolicitadoPorNavigation { get; set; }
        public virtual ICollection<CotiCliente> CotiClienteIIdVendedorAsignadoNavigation { get; set; }
        public virtual ICollection<CotiEmpresa> CotiEmpresaIIdSolicitadoPorNavigation { get; set; }
        public virtual ICollection<CotiEmpresa> CotiEmpresaIIdVendedorAsignadoNavigation { get; set; }
        public virtual ICollection<EmpresaUnidadUsuario> EmpresaUnidadUsuario { get; set; }
        public virtual ICollection<EmpresaUsuario> EmpresaUsuario { get; set; }
        public virtual ICollection<HoldingUsuario> HoldingUsuario { get; set; }
        public virtual ICollection<Medicamento> Medicamento { get; set; }
        public virtual ICollection<Notificacion> Notificacion { get; set; }
        public virtual ICollection<Procedimiento> Precedimiento { get; set; }
        public virtual ICollection<SedeEspecialista> SedeEspecialista { get; set; }
        public virtual ICollection<SedeEspecialistaRol> SedeEspecialistaRol { get; set; }
        public virtual ICollection<UsuarioHerencia> UsuarioHerenciaChildUserNavigation { get; set; }
        public virtual ICollection<UsuarioHerencia> UsuarioHerenciaParentUserNavigation { get; set; }
        public virtual ICollection<UsuarioPassword> UsuarioPassword { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
        public virtual ICollection<WorkflowRol> WorkflowRol { get; set; }
        public virtual ICollection<WorkflowTarea> WorkflowTarea { get; set; }
    }
}
