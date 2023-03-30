using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPrueba
    {
        public DefPrueba()
        {
            Cie10 = new HashSet<Cie10>();
            CitaEmpresaProtocoloOpc = new HashSet<CitaEmpresaProtocoloOpc>();
            CondicionAnotacion = new HashSet<CondicionAnotacion>();
            CotiEmpresaProtPrueba = new HashSet<CotiEmpresaProtPrueba>();
            CotiEmpresaProtPruebaPrecio = new HashSet<CotiEmpresaProtPruebaPrecio>();
            DefCatalogoExcepcionIIdPruebaAnteriorNavigation = new HashSet<DefCatalogoExcepcion>();
            DefCatalogoExcepcionIIdPruebaPosteriorNavigation = new HashSet<DefCatalogoExcepcion>();
            DefPruebaDiagnostico = new HashSet<DefPruebaDiagnostico>();
            DefPruebaEquipo = new HashSet<DefPruebaEquipo>();
            DefPruebaEspecialista = new HashSet<DefPruebaEspecialista>();
            DefPruebaFicha = new HashSet<DefPruebaFicha>();
            DefPruebaInsumo = new HashSet<DefPruebaInsumo>();
            DefPruebaPrecio = new HashSet<DefPruebaPrecio>();
            DefPruebaPruebaIIdPruebaHijaNavigation = new HashSet<DefPruebaPrueba>();
            DefPruebaPruebaIIdPruebaPadreNavigation = new HashSet<DefPruebaPrueba>();
            DefPruebaReporte = new HashSet<DefPruebaReporte>();
            DefPruebaRequisito = new HashSet<DefPruebaRequisito>();
            DefPruebaSede = new HashSet<DefPruebaSede>();
            DefPruebaSedeTiempo = new HashSet<DefPruebaSedeTiempo>();
            DefPruebaSedeZonaAreaServicio = new HashSet<DefPruebaSedeZonaAreaServicio>();
            DefPruebaTipoResultado = new HashSet<DefPruebaTipoResultado>();
            InverseIIdDependeDePruebaNavigation = new HashSet<DefPrueba>();
            MatrizInfoPrueba = new HashSet<MatrizInfoPrueba>();
            MedicamentoAnotacion = new HashSet<MedicamentoAnotacion>();
            ProcedimientoAnotacion = new HashSet<ProcedimientoAnotacion>();
            ProtocoloPrueba = new HashSet<ProtocoloPrueba>();
        }

        public int IIdPrueba { get; set; }
        public int? IIdDependeDePrueba { get; set; }
        public string VcCodigoChaman { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short? SiUnidadMdida { get; set; }
        public short? SiTipoMuestra { get; set; }
        public short? SiSexo { get; set; }
        public decimal? DeValorMinimo { get; set; }
        public decimal? DeValorMaximo { get; set; }
        public decimal? DeCantidadMuestra { get; set; }
        public decimal? DeTiempoObtResultMinutos { get; set; }
        public byte? TiVigenciaMinMeses { get; set; }
        public byte? TiVigenciaMaxMeses { get; set; }
        public bool? BConvalidable { get; set; }
        public byte? TiVigenciaConvalidacionMeses { get; set; }
        public string VcLink { get; set; }
        public short SiTipoPrueba { get; set; }
        public bool? BInfluyeenAptitud { get; set; }
        public string VcNormaLegal { get; set; }
        public bool? BInfluyeAltitud { get; set; }
        public byte? TiDuracionMinutos { get; set; }
        public bool? BSeVendeSolo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdGrupoPrueba { get; set; }
        public int? IIdCpt { get; set; }
        public int? IIdLoinc { get; set; }
        public decimal? DePrecio { get; set; }
        public short SiEstado { get; set; }
        public bool? BTercerizado { get; set; }
        public short? SiEstandarMedico { get; set; }
        public byte? TiTiempoEsperaMinutos { get; set; }

        public virtual Cpt IIdCptNavigation { get; set; }
        public virtual DefPrueba IIdDependeDePruebaNavigation { get; set; }
        public virtual DefGrupoPrueba IIdGrupoPruebaNavigation { get; set; }
        public virtual Loinc IIdLoincNavigation { get; set; }
        public virtual ICollection<Cie10> Cie10 { get; set; }
        public virtual ICollection<CitaEmpresaProtocoloOpc> CitaEmpresaProtocoloOpc { get; set; }
        public virtual ICollection<CondicionAnotacion> CondicionAnotacion { get; set; }
        public virtual ICollection<CotiEmpresaProtPrueba> CotiEmpresaProtPrueba { get; set; }
        public virtual ICollection<CotiEmpresaProtPruebaPrecio> CotiEmpresaProtPruebaPrecio { get; set; }
        public virtual ICollection<DefCatalogoExcepcion> DefCatalogoExcepcionIIdPruebaAnteriorNavigation { get; set; }
        public virtual ICollection<DefCatalogoExcepcion> DefCatalogoExcepcionIIdPruebaPosteriorNavigation { get; set; }
        public virtual ICollection<DefPruebaDiagnostico> DefPruebaDiagnostico { get; set; }
        public virtual ICollection<DefPruebaEquipo> DefPruebaEquipo { get; set; }
        public virtual ICollection<DefPruebaEspecialista> DefPruebaEspecialista { get; set; }
        public virtual ICollection<DefPruebaFicha> DefPruebaFicha { get; set; }
        public virtual ICollection<DefPruebaInsumo> DefPruebaInsumo { get; set; }
        public virtual ICollection<DefPruebaPrecio> DefPruebaPrecio { get; set; }
        public virtual ICollection<DefPruebaPrueba> DefPruebaPruebaIIdPruebaHijaNavigation { get; set; }
        public virtual ICollection<DefPruebaPrueba> DefPruebaPruebaIIdPruebaPadreNavigation { get; set; }
        public virtual ICollection<DefPruebaReporte> DefPruebaReporte { get; set; }
        public virtual ICollection<DefPruebaRequisito> DefPruebaRequisito { get; set; }
        public virtual ICollection<DefPruebaSede> DefPruebaSede { get; set; }
        public virtual ICollection<DefPruebaSedeTiempo> DefPruebaSedeTiempo { get; set; }
        public virtual ICollection<DefPruebaSedeZonaAreaServicio> DefPruebaSedeZonaAreaServicio { get; set; }
        public virtual ICollection<DefPruebaTipoResultado> DefPruebaTipoResultado { get; set; }
        public virtual ICollection<DefPrueba> InverseIIdDependeDePruebaNavigation { get; set; }
        public virtual ICollection<MatrizInfoPrueba> MatrizInfoPrueba { get; set; }
        public virtual ICollection<MedicamentoAnotacion> MedicamentoAnotacion { get; set; }
        public virtual ICollection<ProcedimientoAnotacion> ProcedimientoAnotacion { get; set; }
        public virtual ICollection<ProtocoloPrueba> ProtocoloPrueba { get; set; }
    }
}
