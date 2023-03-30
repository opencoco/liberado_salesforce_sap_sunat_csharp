using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Empresa
    {
        public Empresa()
        {
            CitaEmpresaProtocolo = new HashSet<CitaEmpresaProtocolo>();
            CitaIIdEmpleadorNavigation = new HashSet<Cita>();
            CitaIIdPagadorNavigation = new HashSet<Cita>();
            CitaTrabajadorTitular = new HashSet<CitaTrabajadorTitular>();
            ClienteIIdEmpresaHijoNavigation = new HashSet<Cliente>();
            ClienteIIdEmpresaPadreNavigation = new HashSet<Cliente>();
            CotiClienteIIdEmpresaClienteNavigation = new HashSet<CotiCliente>();
            CotiClienteIIdEmpresaNavigation = new HashSet<CotiCliente>();
            CotiEmpresa = new HashSet<CotiEmpresa>();
            EmpresaCredito = new HashSet<EmpresaCredito>();
            EmpresaDocumento = new HashSet<EmpresaDocumento>();
            EmpresaRl = new HashSet<EmpresaRl>();
            EmpresaSedeInsumo = new HashSet<EmpresaSedeInsumo>();
            EmpresaTrabajador = new HashSet<EmpresaTrabajador>();
            EmpresaUnidad = new HashSet<EmpresaUnidad>();
            EmpresaUsuario = new HashSet<EmpresaUsuario>();
            ProveedorIIdEmpresaHijoNavigation = new HashSet<Proveedor>();
            ProveedorIIdEmpresaPadreNavigation = new HashSet<Proveedor>();
            ReservaFranjaHoraria = new HashSet<ReservaFranjaHoraria>();
            Workflow = new HashSet<Workflow>();
        }

        public int IIdEmpresa { get; set; }
        public int? IIdHolding { get; set; }
        public short? SiTipo { get; set; }
        public string VcRuc { get; set; }
        public string VcRazonSocial { get; set; }
        public string VcNombreComercial { get; set; }
        public byte? TiPeriodoDeValorizacion { get; set; }
        public byte? TiSplit { get; set; }
        public short? SiCicloCobranza { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdActividadEconomicai { get; set; }

        public virtual Holding IIdHoldingNavigation { get; set; }
        public virtual ICollection<CitaEmpresaProtocolo> CitaEmpresaProtocolo { get; set; }
        public virtual ICollection<Cita> CitaIIdEmpleadorNavigation { get; set; }
        public virtual ICollection<Cita> CitaIIdPagadorNavigation { get; set; }
        public virtual ICollection<CitaTrabajadorTitular> CitaTrabajadorTitular { get; set; }
        public virtual ICollection<Cliente> ClienteIIdEmpresaHijoNavigation { get; set; }
        public virtual ICollection<Cliente> ClienteIIdEmpresaPadreNavigation { get; set; }
        public virtual ICollection<CotiCliente> CotiClienteIIdEmpresaClienteNavigation { get; set; }
        public virtual ICollection<CotiCliente> CotiClienteIIdEmpresaNavigation { get; set; }
        public virtual ICollection<CotiEmpresa> CotiEmpresa { get; set; }
        public virtual ICollection<EmpresaCredito> EmpresaCredito { get; set; }
        public virtual ICollection<EmpresaDocumento> EmpresaDocumento { get; set; }
        public virtual ICollection<EmpresaRl> EmpresaRl { get; set; }
        public virtual ICollection<EmpresaSedeInsumo> EmpresaSedeInsumo { get; set; }
        public virtual ICollection<EmpresaTrabajador> EmpresaTrabajador { get; set; }
        public virtual ICollection<EmpresaUnidad> EmpresaUnidad { get; set; }
        public virtual ICollection<EmpresaUsuario> EmpresaUsuario { get; set; }
        public virtual ICollection<Proveedor> ProveedorIIdEmpresaHijoNavigation { get; set; }
        public virtual ICollection<Proveedor> ProveedorIIdEmpresaPadreNavigation { get; set; }
        public virtual ICollection<ReservaFranjaHoraria> ReservaFranjaHoraria { get; set; }
        public virtual ICollection<Workflow> Workflow { get; set; }
    }
}
