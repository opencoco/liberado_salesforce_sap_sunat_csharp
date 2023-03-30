using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class PersonaDireccion
    {
        public int IIdPersonaDireccion { get; set; }
        public int IIdPersona { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Persona IIdPersonaNavigation { get; set; }
    }
}
