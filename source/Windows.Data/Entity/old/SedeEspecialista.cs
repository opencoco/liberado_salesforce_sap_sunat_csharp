using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeEspecialista
    {
        public SedeEspecialista()
        {
            SedeEspecialistaRol = new HashSet<SedeEspecialistaRol>();
        }

        public int IIdSedeEspecialista { get; set; }
        public int IIdUsuario { get; set; }
        public string VcCmp { get; set; }
        public string VcRne { get; set; }
        public DateTime DtCreracion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdSede { get; set; }
        public int? IIdDocumento { get; set; }
        public int? IIdDocumentoFirmaFisica { get; set; }

        public virtual Documento IIdDocumentoFirmaFisicaNavigation { get; set; }
        public virtual Documento IIdDocumentoNavigation { get; set; }
        public virtual Usuario IIdUsuarioNavigation { get; set; }
        public virtual ICollection<SedeEspecialistaRol> SedeEspecialistaRol { get; set; }
    }
}
