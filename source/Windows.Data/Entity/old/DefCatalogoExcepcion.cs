using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefCatalogoExcepcion
    {
        public DefCatalogoExcepcion()
        {
            CotiEmpresaProtExcepcion = new HashSet<CotiEmpresaProtExcepcion>();
            ProtocoloExcepcion = new HashSet<ProtocoloExcepcion>();
        }

        public int IIdCatalogoExcepcion { get; set; }
        public string VcNombre { get; set; }
        public short SiAlturaLabor { get; set; }
        public short SiMomento { get; set; }
        public string VcDescripcion { get; set; }
        public string NvFormula { get; set; }
        public string VcRecomendacion { get; set; }
        public string VcCodigoMigracion { get; set; }
        public short? SiEstado { get; set; }
        public bool? BValido { get; set; }
        public int? IIdPruebaAnterior { get; set; }
        public int IIdPruebaPosterior { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual DefPrueba IIdPruebaAnteriorNavigation { get; set; }
        public virtual DefPrueba IIdPruebaPosteriorNavigation { get; set; }
        public virtual ICollection<CotiEmpresaProtExcepcion> CotiEmpresaProtExcepcion { get; set; }
        public virtual ICollection<ProtocoloExcepcion> ProtocoloExcepcion { get; set; }
    }
}
