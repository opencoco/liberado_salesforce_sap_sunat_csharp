using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiCopiaFileMedico
    {
        public int IIdCotiCopiaFileMedico { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public int IIdCotiEmpresa { get; set; }
        public byte? TiTipo { get; set; }
        public byte? TiPeriodicidad { get; set; }
        public string VcCorreoParaCopia { get; set; }
        public int? IIdRecogerloPor { get; set; }
        public int? IIdRecogerloEn { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual CotiEmpresa IIdCotiEmpresaNavigation { get; set; }
    }
}
