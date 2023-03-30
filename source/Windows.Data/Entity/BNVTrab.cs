using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class BNVTrab
    {
        public int Id { get; set; }
        public string CodigoTrabajador { get; set; }
        public string IdEmpleado { get; set; }
        public string FechaAlta { get; set; }
        public string FechaCese { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Edad { get; set; }
        public string FechaNacimiento { get; set; }
        public string TipoDocumento { get; set; }
        public string DNI { get; set; }
        public string IdEmpresa { get; set; }
        public string IdTipoEmpleado { get; set; }
        public string IdPuesto { get; set; }
        public string NombrePuesto { get; set; }
        public string IdArea { get; set; }
        public string NombreArea { get; set; }
        public string IdContrata { get; set; }
        public string NombreContrata { get; set; }
        public string FechaInicioContrata { get; set; }
        public string FechaFinContrata { get; set; }
        public string CodigoTipoContrato { get; set; }
        public string TipoContrato { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string UbigeoNacimiento { get; set; }
        public string UbigeoDomicilio { get; set; }
        public string NivelDeRiesgo { get; set; }
        public string Celula { get; set; }
        public string Anillo { get; set; }
        public string Categoria { get; set; }
        public string RestriccionesVigentes { get; set; }
        public string UltimaMarcaDeAcceso { get; set; }
        public string UltimaAreaDeAcceso { get; set; }
        public string EntradaSalidaPrincipal { get; set; }
        public string TipoPerfil { get; set; }
        public string CodigoDeLaLocalidad { get; set; }
        public string DescripcionDeLaLocalidad { get; set; }
        public string FechaUltimaModificacion { get; set; }
        public string ContinuaLaborando { get; set; }

        public string IdSubContrata { get; set; }
        public string Unidad { get; set; }
        public string AreaAlta { get; set; }
        public string TitularMovimiento { get; set; }
        public DateTime DtModificacion { get; set; }


    }
}
