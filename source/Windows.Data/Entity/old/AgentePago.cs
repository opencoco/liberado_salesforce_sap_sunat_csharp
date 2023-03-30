using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class AgentePago
    {
        public int IIdAgente { get; set; }
        public string VcNombre { get; set; }
        public string VcRuc { get; set; }
        public string VcDireccion { get; set; }
        public string VcUbigeo { get; set; }
        public string VcDistrito { get; set; }
        public string VcProvincia { get; set; }
        public string VcDepartamento { get; set; }
        public string VcWebsite { get; set; }
        public string VcCorreo { get; set; }
        public string VcCelular { get; set; }
    }
}
