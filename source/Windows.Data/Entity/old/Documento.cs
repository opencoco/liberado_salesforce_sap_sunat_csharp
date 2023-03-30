using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Documento
    {
        public Documento()
        {
            CotiEmpresa = new HashSet<CotiEmpresa>();
            EmpresaCreditoDocumento = new HashSet<EmpresaCreditoDocumento>();
            EmpresaDocumento = new HashSet<EmpresaDocumento>();
            EmpresaRl = new HashSet<EmpresaRl>();
            Notificacion = new HashSet<Notificacion>();
            ProcedimientoCampo = new HashSet<ProcedimientoCampo>();
            ProcedimientoEntregable = new HashSet<ProcedimientoEntregable>();
            SedeEspecialistaIIdDocumentoFirmaFisicaNavigation = new HashSet<SedeEspecialista>();
            SedeEspecialistaIIdDocumentoNavigation = new HashSet<SedeEspecialista>();
        }

        public int IIdDocumento { get; set; }
        public string VcNombre { get; set; }
        public string VcRuta { get; set; }
        public short? SiTipoDoc { get; set; }
        public byte[] VbDocumento { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short SiEstado { get; set; }
        public string NvGuid { get; set; }
        public string VcExtension { get; set; }

        public virtual DocumentoSubclase IIdSubClaseDocumentoNavigation { get; set; }
        public virtual ICollection<CotiEmpresa> CotiEmpresa { get; set; }
        public virtual ICollection<EmpresaCreditoDocumento> EmpresaCreditoDocumento { get; set; }
        public virtual ICollection<EmpresaDocumento> EmpresaDocumento { get; set; }
        public virtual ICollection<EmpresaRl> EmpresaRl { get; set; }
        public virtual ICollection<Notificacion> Notificacion { get; set; }
        public virtual ICollection<ProcedimientoCampo> ProcedimientoCampo { get; set; }
        public virtual ICollection<ProcedimientoEntregable> ProcedimientoEntregable { get; set; }
        public virtual ICollection<SedeEspecialista> SedeEspecialistaIIdDocumentoFirmaFisicaNavigation { get; set; }
        public virtual ICollection<SedeEspecialista> SedeEspecialistaIIdDocumentoNavigation { get; set; }
    }
}
