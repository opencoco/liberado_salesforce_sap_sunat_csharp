using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Cie10
    {
        public Cie10()
        {
            Antecedente = new HashSet<Antecedente>();
            CondicionAnotacion = new HashSet<CondicionAnotacion>();
            DefAntecedenteDx = new HashSet<DefAntecedenteDx>();
            DefPruebaDiagnostico = new HashSet<DefPruebaDiagnostico>();
        }

        public string VcIdCie10 { get; set; }
        public string VcDescripcion { get; set; }
        public bool? BEsCritico { get; set; }
        public bool? BEsRestriccion { get; set; }
        public int? IIdPruebaRequerida { get; set; }
        public bool? BEsAntecedente { get; set; }
        public bool? BEsOcupacional { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual DefPrueba IIdPruebaRequeridaNavigation { get; set; }
        public virtual ICollection<Antecedente> Antecedente { get; set; }
        public virtual ICollection<CondicionAnotacion> CondicionAnotacion { get; set; }
        public virtual ICollection<DefAntecedenteDx> DefAntecedenteDx { get; set; }
        public virtual ICollection<DefPruebaDiagnostico> DefPruebaDiagnostico { get; set; }
    }
}
