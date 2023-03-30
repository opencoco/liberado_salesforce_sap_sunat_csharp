using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EspecialidadMedica
    {
        public EspecialidadMedica()
        {
            DefPruebaEspecialista = new HashSet<DefPruebaEspecialista>();
            DefPruebaReporte = new HashSet<DefPruebaReporte>();
            ProtocoloDocumentoFirma = new HashSet<ProtocoloDocumentoFirma>();
            SedeEspecialistaRol = new HashSet<SedeEspecialistaRol>();
        }

        public int IIdEspecialidadMedica { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public int? IIdServicio { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual Servicio IIdServicioNavigation { get; set; }
        public virtual ICollection<DefPruebaEspecialista> DefPruebaEspecialista { get; set; }
        public virtual ICollection<DefPruebaReporte> DefPruebaReporte { get; set; }
        public virtual ICollection<ProtocoloDocumentoFirma> ProtocoloDocumentoFirma { get; set; }
        public virtual ICollection<SedeEspecialistaRol> SedeEspecialistaRol { get; set; }
    }
}
