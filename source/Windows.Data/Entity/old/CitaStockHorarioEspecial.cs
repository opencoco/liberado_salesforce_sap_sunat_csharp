using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaStockHorarioEspecial
    {
        public long IIdCitaStockHorarioEspecial { get; set; }
        public byte SiDia { get; set; }
        public int IIdSede { get; set; }
        public int IIdServicio { get; set; }
        public DateTime DFecha { get; set; }
        public TimeSpan THoraInicio { get; set; }
        public TimeSpan THoraFin { get; set; }
        public int IStock { get; set; }
        public int? IOcupados { get; set; }
    }
}
