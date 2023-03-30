using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Paciente
    {
        public Paciente()
        {
            Antecedente = new HashSet<Antecedente>();
            CitaTrabajador = new HashSet<CitaTrabajador>();
            Condicion = new HashSet<Condicion>();
            Medicamento = new HashSet<Medicamento>();
            Precedimiento = new HashSet<Procedimiento>();
            Workflow = new HashSet<Workflow>();
        }

        public int IIdPaciente { get; set; }
        public int IIdPersona { get; set; }
        public string VcNumeroIdentificacion { get; set; }
        public byte SiGenero { get; set; }
        public DateTime DFechaNacimiento { get; set; }
        public string VcUbigeo { get; set; }
        public DateTime? DtPrimeraEvaluacion { get; set; }
        public DateTime? DtUltimaEvaluacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Persona IIdPersonaNavigation { get; set; }
        public virtual ICollection<Antecedente> Antecedente { get; set; }
        public virtual ICollection<CitaTrabajador> CitaTrabajador { get; set; }
        public virtual ICollection<Condicion> Condicion { get; set; }
        public virtual ICollection<Medicamento> Medicamento { get; set; }
        public virtual ICollection<Procedimiento> Precedimiento { get; set; }
        public virtual ICollection<Workflow> Workflow { get; set; }
    }
}
