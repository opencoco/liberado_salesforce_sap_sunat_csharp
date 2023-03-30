using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Protocolo
    {
        public Protocolo()
        {
            CitaEmpresaProtocolo = new HashSet<CitaEmpresaProtocolo>();
            CitaTrabajadorExamEspe = new HashSet<CitaTrabajadorExamEspe>();
            CitaTrabajadorPrecio = new HashSet<CitaTrabajadorPrecio>();
            CitaTrabajadorTitular = new HashSet<CitaTrabajadorTitular>();
            ComboProtocolo = new HashSet<ComboProtocolo>();
            CotiEmpresaProt = new HashSet<CotiEmpresaProt>();
            CotiEmpresaProtAptitud = new HashSet<CotiEmpresaProtAptitud>();
            CotiEmpresaProtAptitudDocumento = new HashSet<CotiEmpresaProtAptitudDocumento>();
            CotiEmpresaProtDocumentoFirma = new HashSet<CotiEmpresaProtDocumentoFirma>();
            CotiEmpresaProtExcepcion = new HashSet<CotiEmpresaProtExcepcion>();
            CotiEmpresaProtPrueba = new HashSet<CotiEmpresaProtPrueba>();
            CotiEmpresaProtPruebaPrecio = new HashSet<CotiEmpresaProtPruebaPrecio>();
            CotiEmpresaProtRolDocumento = new HashSet<CotiEmpresaProtRolDocumento>();
            CotiEmpresaProtRolMatrizInfo = new HashSet<CotiEmpresaProtRolMatrizInfo>();
            ProtocoloAptitudDocumento = new HashSet<ProtocoloAptitudDocumento>();
            ProtocoloConceptoFacturable = new HashSet<ProtocoloConceptoFacturable>();
            ProtocoloDocumentoFirma = new HashSet<ProtocoloDocumentoFirma>();
            ProtocoloExcepcion = new HashSet<ProtocoloExcepcion>();
            ProtocoloObservacion = new HashSet<ProtocoloObservacion>();
            ProtocoloPrueba = new HashSet<ProtocoloPrueba>();
            ProtocoloRolDocumento = new HashSet<ProtocoloRolDocumento>();
            ProtocoloRolMatrizInfo = new HashSet<ProtocoloRolMatrizInfo>();
            ProtocoloServicio = new HashSet<ProtocoloServicio>();
        }

        public int IIdProtocolo { get; set; }
        public string VcCodigoMigracion { get; set; }
        public byte? TiVigenciaMinMeses { get; set; }
        public byte TiVigenciaMaxMeses { get; set; }
        public string VcNombre { get; set; }
        public string VcDescricpion { get; set; }
        public byte TiTiempoAtencionDias { get; set; }
        public byte TiTiempoLevanteObsdias { get; set; }
        public int IIdTipoExamen { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual TipoPrueba IIdTipoExamenNavigation { get; set; }
        public virtual ICollection<CitaEmpresaProtocolo> CitaEmpresaProtocolo { get; set; }
        public virtual ICollection<CitaTrabajadorExamEspe> CitaTrabajadorExamEspe { get; set; }
        public virtual ICollection<CitaTrabajadorPrecio> CitaTrabajadorPrecio { get; set; }
        public virtual ICollection<CitaTrabajadorTitular> CitaTrabajadorTitular { get; set; }
        public virtual ICollection<ComboProtocolo> ComboProtocolo { get; set; }
        public virtual ICollection<CotiEmpresaProt> CotiEmpresaProt { get; set; }
        public virtual ICollection<CotiEmpresaProtAptitud> CotiEmpresaProtAptitud { get; set; }
        public virtual ICollection<CotiEmpresaProtAptitudDocumento> CotiEmpresaProtAptitudDocumento { get; set; }
        public virtual ICollection<CotiEmpresaProtDocumentoFirma> CotiEmpresaProtDocumentoFirma { get; set; }
        public virtual ICollection<CotiEmpresaProtExcepcion> CotiEmpresaProtExcepcion { get; set; }
        public virtual ICollection<CotiEmpresaProtPrueba> CotiEmpresaProtPrueba { get; set; }
        public virtual ICollection<CotiEmpresaProtPruebaPrecio> CotiEmpresaProtPruebaPrecio { get; set; }
        public virtual ICollection<CotiEmpresaProtRolDocumento> CotiEmpresaProtRolDocumento { get; set; }
        public virtual ICollection<CotiEmpresaProtRolMatrizInfo> CotiEmpresaProtRolMatrizInfo { get; set; }
        public virtual ICollection<ProtocoloAptitudDocumento> ProtocoloAptitudDocumento { get; set; }
        public virtual ICollection<ProtocoloConceptoFacturable> ProtocoloConceptoFacturable { get; set; }
        public virtual ICollection<ProtocoloDocumentoFirma> ProtocoloDocumentoFirma { get; set; }
        public virtual ICollection<ProtocoloExcepcion> ProtocoloExcepcion { get; set; }
        public virtual ICollection<ProtocoloObservacion> ProtocoloObservacion { get; set; }
        public virtual ICollection<ProtocoloPrueba> ProtocoloPrueba { get; set; }
        public virtual ICollection<ProtocoloRolDocumento> ProtocoloRolDocumento { get; set; }
        public virtual ICollection<ProtocoloRolMatrizInfo> ProtocoloRolMatrizInfo { get; set; }
        public virtual ICollection<ProtocoloServicio> ProtocoloServicio { get; set; }
    }
}
