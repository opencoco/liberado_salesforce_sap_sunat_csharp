using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiDocumentoFirma
    {
        public int IIdPlanFirmaDocumento { get; set; }
        public int IIdCotiEmpresa { get; set; }
        public bool? BFirmaDeQuienRealiza { get; set; }
        public bool? BFirmaDeQuienValida { get; set; }
        public bool? BFirmaOpaciente { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public int IIdQuienEmiteInforme { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual CotiEmpresa IIdCotiEmpresaNavigation { get; set; }
    }
}
