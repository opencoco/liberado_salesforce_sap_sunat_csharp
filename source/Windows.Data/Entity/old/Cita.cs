using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Cita
    {
        public Cita()
        {
            CitaEmpresaUnidad = new HashSet<CitaEmpresaUnidad>();
            CitaSede = new HashSet<CitaSede>();
            CitaTrabajador = new HashSet<CitaTrabajador>();
        }

        public int IIdCita { get; set; }
        public int IIdServicio { get; set; }
        public DateTime DtFecha { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public bool? BParaElEmpleador { get; set; }
        public string VcCorreo { get; set; }
        public string VcCorreo1RecepcionFactura { get; set; }
        public string VcCorreo2RecepcionFactura { get; set; }
        public bool? BUsarMisInsumos { get; set; }
        public bool? SiPagador { get; set; }
        public string VcFacturaRs { get; set; }
        public string VcFacturaRuc { get; set; }
        public short? SiAccionFaltaPruebas { get; set; }
        public short? SiFormadePago { get; set; }
        public short? SiEstadoPago { get; set; }
        public short? SiAgente { get; set; }
        public int IIdEmpleador { get; set; }
        public int? IIdPagador { get; set; }
        public short? SiTipo { get; set; }
        public int? IIdAgente { get; set; }

        public virtual Empresa IIdEmpleadorNavigation { get; set; }
        public virtual Empresa IIdPagadorNavigation { get; set; }
        public virtual Servicio IIdServicioNavigation { get; set; }
        public virtual ICollection<CitaEmpresaUnidad> CitaEmpresaUnidad { get; set; }
        public virtual ICollection<CitaSede> CitaSede { get; set; }
        public virtual ICollection<CitaTrabajador> CitaTrabajador { get; set; }
    }
}
