using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiCliente
    {
        public CotiCliente()
        {
            CotiClientePruebaAdd = new HashSet<CotiClientePruebaAdd>();
            CotiEmpresaSeguimiento = new HashSet<CotiEmpresaSeguimiento>();
            InverseIIdCotiClientePadreNavigation = new HashSet<CotiCliente>();
            Workflow = new HashSet<Workflow>();
        }

        public int IIdCotiCliente { get; set; }
        public int? IIdCotiClientePadre { get; set; }
        public short SiNumeroTrabajadores { get; set; }
        public byte? TiPeriodoContratoAnios { get; set; }
        public DateTime? DtSolicitud { get; set; }
        public short? SiEtapaFunnel { get; set; }
        public byte? TiEstado { get; set; }
        public int IIdEmpresa { get; set; }
        public int IIdEmpresaCliente { get; set; }
        public int IIdSolicitadoPor { get; set; }
        public int? IIdVendedorAsignado { get; set; }
        public int? IIdHolding { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int IIdUsuarioModificacion { get; set; }

        public virtual CotiCliente IIdCotiClientePadreNavigation { get; set; }
        public virtual Empresa IIdEmpresaClienteNavigation { get; set; }
        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Holding IIdHoldingNavigation { get; set; }
        public virtual Usuario IIdSolicitadoPorNavigation { get; set; }
        public virtual Usuario IIdVendedorAsignadoNavigation { get; set; }
        public virtual ICollection<CotiClientePruebaAdd> CotiClientePruebaAdd { get; set; }
        public virtual ICollection<CotiEmpresaSeguimiento> CotiEmpresaSeguimiento { get; set; }
        public virtual ICollection<CotiCliente> InverseIIdCotiClientePadreNavigation { get; set; }
        public virtual ICollection<Workflow> Workflow { get; set; }
    }
}
