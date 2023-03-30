using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefCatalogoAptitud
    {
        public DefCatalogoAptitud()
        {
            CotiEmpresaProtAptitud = new HashSet<CotiEmpresaProtAptitud>();
        }

        public int IIdCatalogoAptitud { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public string NvFormula { get; set; }
        public string VcRecomendacion { get; set; }
        public bool? BValido { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcCodigoMigracion { get; set; }
        public int IIdTipoAptitud { get; set; }
        public short? SiEstado { get; set; }

        public virtual TipoAptitud IIdTipoAptitudNavigation { get; set; }
        public virtual ICollection<CotiEmpresaProtAptitud> CotiEmpresaProtAptitud { get; set; }
    }
}
