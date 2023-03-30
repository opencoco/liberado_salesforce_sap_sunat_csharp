using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Persona
    {
        public Persona()
        {
            CitaTrabajador = new HashSet<CitaTrabajador>();
            EmpresaTrabajador = new HashSet<EmpresaTrabajador>();
            EncuestaRespuesta = new HashSet<EncuestaRespuesta>();
            Paciente = new HashSet<Paciente>();
            PersonaDireccion = new HashSet<PersonaDireccion>();
            Usuario = new HashSet<Usuario>();
        }

        public int IIdPersona { get; set; }
        public string VcNombres { get; set; }
        public string VcApellidoPaterno { get; set; }
        public string VcApellidoMaterno { get; set; }
        public short? SiGenero { get; set; }
        public DateTime? DNacimiento { get; set; }
        public DateTime? DMuerte { get; set; }
        public string VcNumeroIdentificacion { get; set; }
        public short? SiTipoIdentificacion { get; set; }
        public short? SiGrupoSanguineo { get; set; }
        public short? SiFactorSanguineo { get; set; }
        public bool? BComorbilidadAsociada { get; set; }
        public byte? TiEdadCalculada { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual ICollection<CitaTrabajador> CitaTrabajador { get; set; }
        public virtual ICollection<EmpresaTrabajador> EmpresaTrabajador { get; set; }
        public virtual ICollection<EncuestaRespuesta> EncuestaRespuesta { get; set; }
        public virtual ICollection<Paciente> Paciente { get; set; }
        public virtual ICollection<PersonaDireccion> PersonaDireccion { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
