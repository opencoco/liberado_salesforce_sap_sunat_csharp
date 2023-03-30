using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class PersonaTmp
    {
   

        public int IIdPersona { get; set; }
        public string VcNombres { get; set; }
        public string NroHistoriaMedica { get; set; }
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
        public bool? BValidacionReniec { get; set; }
        public byte? TiEdadCalculada { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdFoto { get; set; }
        public bool? BEsTelemetry { get; set; }

        public int IIdUsuario { get; set; }
        public string NvProfileSync { get; set; }
        public short? SiEstado { get; set; }
        public string VcEmail { get; set; }
        public string VcCelularPrincipal { get; set; }
        public DateTime? DtActivoDesde { get; set; }
        public string VcGenero{ get; set; }
        public string VcTipoIdentificacion { get; set; }
        public string VcGrupoSanguineo { get; set; }
        public string VcFactorSanguineo { get; set; }
        public string VcEstado { get; set; }
        public string VcUsuarioModificacion { get; set; }
        public int? IIdUsuarioPassword { get; set; }
        public string NvPassword { get; set; }
        public Byte[] VbPasswordHash { get; set; }
        public Byte[] VbPasswordSalt { get; set; }

        public string NvUUID { get; set; }
        public short? SiConfidencialidad { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsPremium { get; set; }

        public short? SiEstadoCivil { get; set; }
        public short? SiGradoInstruccion { get; set; }

        //public virtual ICollection<EmpresaTrabajador> EmpresaTrabajador { get; set; }
        //public virtual ICollection<Paciente> Paciente { get; set; }
        //public virtual ICollection<PersonaDireccion> PersonaDireccion { get; set; }
        //public virtual Usuario Usuario { get; set; }
        //public virtual ICollection<Rol> Rol { get; set; }

        //auxiliares
        public int[] LstRoles { get; set; } = null;
        public int? IIdCitaTrabajador { get; set; }
        public int? IPendientes { get; set; }

        public short? SiEstadoCovid { get; set; }
        public decimal? DeAlturaLaborVigente { get; set; }
        public string VcUbigeoVigente { get; set; }
        public string VcResidenciaVigente { get; set; }
        public int? IIdFirmaFisica { get; set; }
        public string VcUbigeoNacimiento { get; set; }

        public short? SiGrupoOcupacionalVigente { get; set; }

    }
}
