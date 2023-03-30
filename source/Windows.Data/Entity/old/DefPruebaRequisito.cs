using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefPruebaRequisito
    {
        public int IIdPruebaRequisito { get; set; }
        public int IIdPrueba { get; set; }
        public int IIdRequisito { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual DefPrueba IIdPruebaNavigation { get; set; }
        public virtual Requisito IIdRequisitoNavigation { get; set; }
    }
}
