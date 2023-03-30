using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiEmpresa
    {
        public CotiEmpresa()
        {
            CotiCopiaFileMedico = new HashSet<CotiCopiaFileMedico>();
            CotiDocumentoFirma = new HashSet<CotiDocumentoFirma>();
            CotiEmpresaAdd = new HashSet<CotiEmpresaAdd>();
            CotiEmpresaProt = new HashSet<CotiEmpresaProt>();
            CotiEmpresaProtAptitud = new HashSet<CotiEmpresaProtAptitud>();
            CotiEmpresaProtAptitudDocumento = new HashSet<CotiEmpresaProtAptitudDocumento>();
            CotiEmpresaProtDocumentoFirma = new HashSet<CotiEmpresaProtDocumentoFirma>();
            CotiEmpresaProtExcepcion = new HashSet<CotiEmpresaProtExcepcion>();
            CotiEmpresaProtPrueba = new HashSet<CotiEmpresaProtPrueba>();
            CotiEmpresaProtPruebaPrecio = new HashSet<CotiEmpresaProtPruebaPrecio>();
            CotiEmpresaProtRolDocumento = new HashSet<CotiEmpresaProtRolDocumento>();
            CotiEmpresaProtRolMatrizInfo = new HashSet<CotiEmpresaProtRolMatrizInfo>();
            CotiEmpresaSeguimiento = new HashSet<CotiEmpresaSeguimiento>();
            InverseIIdCotiEmpresaPadreNavigation = new HashSet<CotiEmpresa>();
            Workflow = new HashSet<Workflow>();
        }

        public int IIdCotiEmpresa { get; set; }
        public int? IIdCotiEmpresaPadre { get; set; }
        public int InNumeroTrabajadores { get; set; }
        public byte? TiPeriodoContratoAnios { get; set; }
        public DateTime? DtSolicitud { get; set; }
        public short? SiEtapaFunnel { get; set; }
        public short? SiEstado { get; set; }
        public int IIdEmpresa { get; set; }
        public int IIdSolicitadoPor { get; set; }
        public int IIdCombo { get; set; }
        public int? IIdVendedorAsignado { get; set; }
        public bool? BResultadoEnDigital { get; set; }
        public bool? BTieneMedicoSo { get; set; }
        public byte? TiTipoPago { get; set; }
        public int? IIdActividadEconomicai { get; set; }
        public int? IIdCriteriosPropios { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IVersion { get; set; }

        public virtual ActividadEconomica IIdActividadEconomicaiNavigation { get; set; }
        public virtual Combo IIdComboNavigation { get; set; }
        public virtual CotiEmpresa IIdCotiEmpresaPadreNavigation { get; set; }
        public virtual Documento IIdCriteriosPropiosNavigation { get; set; }
        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Usuario IIdSolicitadoPorNavigation { get; set; }
        public virtual Usuario IIdVendedorAsignadoNavigation { get; set; }
        public virtual ICollection<CotiCopiaFileMedico> CotiCopiaFileMedico { get; set; }
        public virtual ICollection<CotiDocumentoFirma> CotiDocumentoFirma { get; set; }
        public virtual ICollection<CotiEmpresaAdd> CotiEmpresaAdd { get; set; }
        public virtual ICollection<CotiEmpresaProt> CotiEmpresaProt { get; set; }
        public virtual ICollection<CotiEmpresaProtAptitud> CotiEmpresaProtAptitud { get; set; }
        public virtual ICollection<CotiEmpresaProtAptitudDocumento> CotiEmpresaProtAptitudDocumento { get; set; }
        public virtual ICollection<CotiEmpresaProtDocumentoFirma> CotiEmpresaProtDocumentoFirma { get; set; }
        public virtual ICollection<CotiEmpresaProtExcepcion> CotiEmpresaProtExcepcion { get; set; }
        public virtual ICollection<CotiEmpresaProtPrueba> CotiEmpresaProtPrueba { get; set; }
        public virtual ICollection<CotiEmpresaProtPruebaPrecio> CotiEmpresaProtPruebaPrecio { get; set; }
        public virtual ICollection<CotiEmpresaProtRolDocumento> CotiEmpresaProtRolDocumento { get; set; }
        public virtual ICollection<CotiEmpresaProtRolMatrizInfo> CotiEmpresaProtRolMatrizInfo { get; set; }
        public virtual ICollection<CotiEmpresaSeguimiento> CotiEmpresaSeguimiento { get; set; }
        public virtual ICollection<CotiEmpresa> InverseIIdCotiEmpresaPadreNavigation { get; set; }
        public virtual ICollection<Workflow> Workflow { get; set; }
    }
}
