using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeEspecialistaRol
    {
        public int IIdSedeEspecialistaEspe { get; set; }
        public int IIdUsuario { get; set; }
        public int IIdEspecialidadMedica { get; set; }
        public int IIdSede { get; set; }
        public int IIdSedeEspecialista { get; set; }

        public virtual EspecialidadMedica IIdEspecialidadMedicaNavigation { get; set; }
        public virtual SedeEspecialista IIdSedeEspecialistaNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
        public virtual Usuario IIdUsuarioNavigation { get; set; }
    }
}
