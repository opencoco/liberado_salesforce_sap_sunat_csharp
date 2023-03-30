using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class CitaTrabajadorData
    {
        public int IIdCitaTrabData { get; set; }
        public int IIdCitaTrabajador { get; set; }
        public int IIdPaciente { get; set; }
        public int IIdSede { get; set; }
        public string JsonSchema { get; set; }
        public string JsonData { get; set; }
        public string VcNumeroIdentificacion { get; set; }
        public byte SiGenero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public byte SiEstado { get; set; }
        public int? IIdWorkflow { get; set; }
        public string JsonServicioFichaData { get; set; }
        public DateTime? DtSincronizacion { get; set; }
        public bool? BMigrado { get; set; }

        //public virtual CitaTrabajador IIdCitaTrabajadorNavigation { get; set; }
        // auxiliar
        public int IIdUsuario { get; set; }
    }
}
