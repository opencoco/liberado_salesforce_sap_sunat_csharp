using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Data.DataManager
{
    internal class JsonData
    {
        public JsonData()
        {
            Protocolos = new List<Protocolo>();
            Visitas = new List<Visita>();
        }

        public virtual IList<Protocolo> Protocolos { get; set; }
        public virtual IList<Visita> Visitas { get; set; }
    }

    internal class Protocolo
    {
        public Protocolo()
        {
            Pruebas = new List<Prueba>();
        }
        public int IIdProtocolo { get; set; }
        public virtual IList<Prueba> Pruebas { get; set; }
    }

    internal class Visita
    {
        public DateTime? DtInicio { get; set; }
        public DateTime? DtFin { get; set; }
        public int? IIdUsuarioInicio { get; set; }
        public int? IIdUsuarioFin { get; set; }
    }

    internal class Prueba
    {
        public Prueba()
        {
            Fichas = new List<Ficha>();
            EntregablesGenerados = new List<Entregable>();
            Diagnosticos = new List<Diagnostico>();
        }

        public int IIdPrueba { get; set; }
        public string VcNombre { get; set; }
        public DateTime? DtInicioRegistroDatos { get; set; }
        public DateTime? DtFinRegistroDatos { get; set; }
        public int? IIdUsuarioInicioRegistroDatos { get; set; }
        public int? IIdUsuarioFinRegistroDatos { get; set; }
        public bool BCompletado { get; set; }
        public virtual IList<Ficha> Fichas { get; set; }
        public virtual IList<Entregable> EntregablesGenerados { get; set; }
        public virtual IList<Diagnostico> Diagnosticos { get; set; }
    }

    internal class Ficha
    {
        public Ficha()
        {
            Campos = new List<Campo>();
        }
        public int IIdPrueba { get; set; }
        public int IIdFicha { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public virtual IList<Campo> Campos { get; set; }
    }

    internal class Campo
    {
        public int IIdCampo { get; set; }
        public string VcNombre { get; set; }
        public short? ITipoValor { get; set; }
        //public string VcValor { get; set; }
        public object VcValor { get; set; }
    }

    internal class Diagnostico
    {
        public int IIdPrueba { get; set; }
        public string VcIdCie10 { get; set; }
        public bool? BRestriccion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtControl { get; set; }
        public DateTime? DtDesde { get; set; }
        public DateTime? DtHasta { get; set; }
        public Seguimiento Seguimiento { get; set; }
    }

    internal class Seguimiento
    {
        public int? FrecuenciaDias { get; set; }
    }

    internal class Entregable
    {
        public int IIdDocumento { get; set; }
        public string Guid { get; set; }
        public int IIdClaseDocumento { get; set; }
        public int IIdSubClaseDocumento { get; set; }
        public string VcReporteQueGenera { get; set; }
    }
}
