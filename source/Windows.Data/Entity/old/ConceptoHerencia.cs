using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ConceptoHerencia
    {
        public long IIdConceptoRelacion { get; set; }
        public long ParentConceptId { get; set; }
        public long ChildConceptId { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Concepto ChildConcept { get; set; }
        public virtual Concepto ParentConcept { get; set; }
    }
}
