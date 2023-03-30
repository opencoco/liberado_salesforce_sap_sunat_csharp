using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Sede
    {
        public Sede()
        {
            CitaSede = new HashSet<CitaSede>();
            CitaTrabajador = new HashSet<CitaTrabajador>();
            Condicion = new HashSet<Condicion>();
            DefPruebaSede = new HashSet<DefPruebaSede>();
            DefPruebaSedeTiempo = new HashSet<DefPruebaSedeTiempo>();
            HorarioEspecial = new HashSet<HorarioEspecial>();
            Precedimiento = new HashSet<Procedimiento>();
            ReservaFranjaHoraria = new HashSet<ReservaFranjaHoraria>();
            SedeEspecialistaRol = new HashSet<SedeEspecialistaRol>();
            SedeInsumo = new HashSet<SedeInsumo>();
            SedeServicioHorario = new HashSet<SedeServicioHorario>();
            SedeZona = new HashSet<SedeZona>();
            SedeZonaArea = new HashSet<SedeZonaArea>();
            SedeZonaAreaServicio = new HashSet<SedeZonaAreaServicio>();
        }

        public int IIdSede { get; set; }
        public string VcCodigoMigracion { get; set; }
        public string VcNombre { get; set; }
        public string VcUbigeo { get; set; }
        public byte? SiTipo { get; set; }
        public bool BTercerizado { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public string VcLatitud { get; set; }
        public string VcAltitud { get; set; }
        public string VcDireccion { get; set; }
        public string VcTelefono { get; set; }
        public string VcCelular { get; set; }
        public string VcWebsite { get; set; }
        public string VcCorreo { get; set; }
        public int? IIdContacto { get; set; }
        public bool? BAceptaEfectivo { get; set; }

        public virtual ICollection<CitaSede> CitaSede { get; set; }
        public virtual ICollection<CitaTrabajador> CitaTrabajador { get; set; }
        public virtual ICollection<Condicion> Condicion { get; set; }
        public virtual ICollection<DefPruebaSede> DefPruebaSede { get; set; }
        public virtual ICollection<DefPruebaSedeTiempo> DefPruebaSedeTiempo { get; set; }
        public virtual ICollection<HorarioEspecial> HorarioEspecial { get; set; }
        public virtual ICollection<Procedimiento> Precedimiento { get; set; }
        public virtual ICollection<ReservaFranjaHoraria> ReservaFranjaHoraria { get; set; }
        public virtual ICollection<SedeEspecialistaRol> SedeEspecialistaRol { get; set; }
        public virtual ICollection<SedeInsumo> SedeInsumo { get; set; }
        public virtual ICollection<SedeServicioHorario> SedeServicioHorario { get; set; }
        public virtual ICollection<SedeZona> SedeZona { get; set; }
        public virtual ICollection<SedeZonaArea> SedeZonaArea { get; set; }
        public virtual ICollection<SedeZonaAreaServicio> SedeZonaAreaServicio { get; set; }
    }
}
