using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Concepto
    {
        public Concepto()
        {
            ConceptoHerenciaChildConcept = new HashSet<ConceptoHerencia>();
            ConceptoHerenciaParentConcept = new HashSet<ConceptoHerencia>();
            Condicion = new HashSet<Condicion>();
            Medicamento = new HashSet<Medicamento>();
            Precedimiento = new HashSet<Procedimiento>();
        }

        public long IIdConcepto { get; set; }
        public string VcNombre { get; set; }
        public short? SiTipo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdCitaTrabajador { get; set; }
        public short SiOrigen { get; set; }

        public virtual CitaTrabajador IIdCitaTrabajadorNavigation { get; set; }
        public virtual ICollection<ConceptoHerencia> ConceptoHerenciaChildConcept { get; set; }
        public virtual ICollection<ConceptoHerencia> ConceptoHerenciaParentConcept { get; set; }
        public virtual ICollection<Condicion> Condicion { get; set; }
        public virtual ICollection<Medicamento> Medicamento { get; set; }
        public virtual ICollection<Procedimiento> Precedimiento { get; set; }
    }
}
