using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaTrabajador
    {
        public CitaTrabajador()
        {
            CitaTrabajadorAccion = new HashSet<CitaTrabajadorAccion>();
            CitaTrabajadorExamEspe = new HashSet<CitaTrabajadorExamEspe>();
            CitaTrabajadorExams = new HashSet<CitaTrabajadorExams>();
            CitaTrabajadorPrecio = new HashSet<CitaTrabajadorPrecio>();
            CitaTrabajadorTitular = new HashSet<CitaTrabajadorTitular>();
            Concepto = new HashSet<Concepto>();
        }

        public int IIdCitaTrabajador { get; set; }
        public DateTime? DtHoraDeLaCita { get; set; }
        public string VcCorreo { get; set; }
        public string VcCelular { get; set; }
        public string VcNombres { get; set; }
        public string VcApellidoPaterno { get; set; }
        public string VcApellidoMaterno { get; set; }
        public short? SiGenero { get; set; }
        public DateTime? DNacimiento { get; set; }
        public string VcNumeroIdentificacion { get; set; }
        public short? SiTipoIdentificacion { get; set; }
        public short? SiGrupoSanguineo { get; set; }
        public short? SiFactorSanguineo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short SiEstado { get; set; }
        public string VcNota { get; set; }
        public decimal? DePrecio { get; set; }
        public int IIdCita { get; set; }
        public int? IIdSede { get; set; }
        public int? IIdPersona { get; set; }
        public int? IIdUsuario { get; set; }
        public string VcCodigoMigracion { get; set; }
        public string VcErrorMigracion { get; set; }
        public string VcPuestoTemporal { get; set; }
        public int? IIdPuestoAutoReferido { get; set; }
        public int? IIdAreaAutoReferido { get; set; }
        public int? IIdZonaAutoReferido { get; set; }
        public int? IIdPaciente { get; set; }
        public string VcCentroCosto { get; set; }

        public virtual Cita IIdCitaNavigation { get; set; }
        public virtual Paciente IIdPacienteNavigation { get; set; }
        public virtual Persona IIdPersonaNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
        public virtual CitaTrabajadorData CitaTrabajadorData { get; set; }
        public virtual ICollection<CitaTrabajadorAccion> CitaTrabajadorAccion { get; set; }
        public virtual ICollection<CitaTrabajadorExamEspe> CitaTrabajadorExamEspe { get; set; }
        public virtual ICollection<CitaTrabajadorExams> CitaTrabajadorExams { get; set; }
        public virtual ICollection<CitaTrabajadorPrecio> CitaTrabajadorPrecio { get; set; }
        public virtual ICollection<CitaTrabajadorTitular> CitaTrabajadorTitular { get; set; }
        public virtual ICollection<Concepto> Concepto { get; set; }
    }
}
