using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Rol
    {
        public Rol()
        {
            CotiEmpresaProtRolDocumento = new HashSet<CotiEmpresaProtRolDocumento>();
            CotiEmpresaProtRolMatrizInfo = new HashSet<CotiEmpresaProtRolMatrizInfo>();
            EmpresaUnidadUsuario = new HashSet<EmpresaUnidadUsuario>();
            EmpresaUsuario = new HashSet<EmpresaUsuario>();
            HoldingUsuario = new HashSet<HoldingUsuario>();
            PermisoAutomatizadoRol = new HashSet<PermisoAutomatizadoRol>();
            PermisoUrlRol = new HashSet<PermisoUrlRol>();
            ProtocoloRolDocumento = new HashSet<ProtocoloRolDocumento>();
            RolPermiso = new HashSet<RolPermiso>();
            RolRolChildRole = new HashSet<RolRol>();
            RolRolParentRole = new HashSet<RolRol>();
            RoutingRol = new HashSet<RoutingRol>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int IIdRol { get; set; }
        public string VcNombre { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public bool? BAccesoAdmin { get; set; }
        public short? SiEstado { get; set; }
        public bool? BParaClientes { get; set; }

        public virtual ICollection<CotiEmpresaProtRolDocumento> CotiEmpresaProtRolDocumento { get; set; }
        public virtual ICollection<CotiEmpresaProtRolMatrizInfo> CotiEmpresaProtRolMatrizInfo { get; set; }
        public virtual ICollection<EmpresaUnidadUsuario> EmpresaUnidadUsuario { get; set; }
        public virtual ICollection<EmpresaUsuario> EmpresaUsuario { get; set; }
        public virtual ICollection<HoldingUsuario> HoldingUsuario { get; set; }
        public virtual ICollection<PermisoAutomatizadoRol> PermisoAutomatizadoRol { get; set; }
        public virtual ICollection<PermisoUrlRol> PermisoUrlRol { get; set; }
        public virtual ICollection<ProtocoloRolDocumento> ProtocoloRolDocumento { get; set; }
        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
        public virtual ICollection<RolRol> RolRolChildRole { get; set; }
        public virtual ICollection<RolRol> RolRolParentRole { get; set; }
        public virtual ICollection<RoutingRol> RoutingRol { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
