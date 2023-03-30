using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Servicio
    {
        public Servicio()
        {
            Cita = new HashSet<Cita>();
            DocuementoClase = new HashSet<DocuementoClase>();
            EmpresaSedeInsumoSrv = new HashSet<EmpresaSedeInsumoSrv>();
            Encuesta = new HashSet<Encuesta>();
            EspecialidadMedica = new HashSet<EspecialidadMedica>();
            ProtocoloServicio = new HashSet<ProtocoloServicio>();
            SedeNsumoServicio = new HashSet<SedeNsumoServicio>();
            SedeServicioHorario = new HashSet<SedeServicioHorario>();
            SedeZonaAreaServicio = new HashSet<SedeZonaAreaServicio>();
        }

        public int IIdServicio { get; set; }
        public string VcNombre { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcCodigoMigracion { get; set; }
        public short? SiEstado { get; set; }
        public string VcDescripcion { get; set; }
        public int? IIdFicha { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<DocuementoClase> DocuementoClase { get; set; }
        public virtual ICollection<EmpresaSedeInsumoSrv> EmpresaSedeInsumoSrv { get; set; }
        public virtual ICollection<Encuesta> Encuesta { get; set; }
        public virtual ICollection<EspecialidadMedica> EspecialidadMedica { get; set; }
        public virtual ICollection<ProtocoloServicio> ProtocoloServicio { get; set; }
        public virtual ICollection<SedeNsumoServicio> SedeNsumoServicio { get; set; }
        public virtual ICollection<SedeServicioHorario> SedeServicioHorario { get; set; }
        public virtual ICollection<SedeZonaAreaServicio> SedeZonaAreaServicio { get; set; }
    }
}
