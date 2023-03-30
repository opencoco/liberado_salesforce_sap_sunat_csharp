using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ACME.Data.Entity
{
    public partial class CitaTrabajador
    {
        public string Examen { get; set; }
        public string Empleador { get; set; }
        public string Lugar_nacimiento { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Actividad_empresa_actua { get; set; }
        public string Fecha_inicial_examen { get; set; }
        public string Motivo_evaluacion { get; set; }
        public string Tipo_emo { get; set; }
        public string Clinica_evaluadora { get; set; }
        public string Direccion_clinica { get; set; }
        public string Empresa { get; set; }
        public decimal? Tiempo_laborando { get; set; }
        public string Empresa_anterior { get; set; }
        public string Actividad_empresa_actual { get; set; }
        public string Actividad_empresa_anterior { get; set; }
        public string Unidad { get; set; }
        public string Zona { get; set; }
        public string Area { get; set; }
        public string Puesto { get; set; }
        public string Protocolo { get; set; }
        public string Titular { get; set; }
        public string Evaluador { get; set; }
        public string Tipo_examen { get; set; }
        public string Fecha_levante_observacion { get; set; }
        public string Nombres_apellidos { get; set; }
        public string Apellidos_nombres { get; set; }
        public string Dni { get; set; }
        public string Fecha_evaluacion { get; set; }
        public string Fecha { get; set; }
        public string Fecha_nacimiento { get; set; }
        public string Masculino { get; set; }
        public string Femenino { get; set; }
        public string Tipo_documento { get; set; }
        public string Telefono { get; set; }
        public string Hcl { get; set; }
        public string Edad { get; set; }
        public string Grado_instruccion { get; set; }
        public string Estado_civil { get; set; }
        public string Lugar_residencia { get; set; }
        public decimal? Tiempo_total_laborando { get; set; }
        public string Area_trabajo_superficie { get; set; }
        public string Area_trabajo_subsuelo { get; set; }
        public string Evaluacion_preocupacional { get; set; }
        public string Evaluacion_ocupacional { get; set; }
        public string Evaluacion_postocupacional { get; set; }
        public string C_ps_p { get; set; }
        //public string Apellidos { get; set; }
        //public string Nombres { get; set; }
        public string Nacionalidad { get; set; }
        public string Sexo { get; set; }
        public string Departamento { get; set; }
        public decimal? Tiempo_experiencia { get; set; }
        public string Compañia { get; set; }
        public string Tipo_licencia { get; set; }
        public string Codigo { get; set; }
        public string Version { get; set; }
        public string Evaluacion_periodica { get; set; }
        public string Nombre_eess { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        public string Hora_evaluacion { get; set; }
        public string Lugar_evaluacion { get; set; }
        public string Domicilio { get; set; }
        public string Soltero { get; set; }
        public string Casado { get; set; }
        public string Conviviente { get; set; }

        public string Viudo { get; set; }
        public string Divorciado { get; set; }
        public string Analfabeto { get; set; }
        public string Prim_completa { get; set; }
        public string Prim_incompleta { get; set; }
        public string Sec_completa { get; set; }
        public string Sec_incompleta { get; set; }
        public string Tecnico { get; set; }
        public string Universitario { get; set; }
        public string Mestizo { get; set; }
        public string Andino { get; set; }
        public string Asiatico_descendiente { get; set; }
        public string Indigena_amazonico { get; set; }
        public string Afrodescendiente { get; set; }

        public string Otra_etnia_raza { get; set; }
        public string Otra_etnia_raza_descripcion { get; set; }

        public string Pueblo_etnico { get; set; }
        public string Peruano { get; set; }

        public string Extranjero { get; set; }
        public string Pais_nacionalidad { get; set; }
        public string Migrante_si { get; set; }
        public short SiGenero { get; set; }

        // auxiliares
        public string Migrante_no { get; set; }
        public string Pais_origen { get; set; }
        public string Residencia_pais { get; set; }
        public string Residencia_localidad { get; set; }

        public string Residencia_urb_area { get; set; }
        public string Residencia_tipo_via { get; set; }

        public string Residencia_lote_nr { get; set; }
        public string Residencia_nombre_via { get; set; }
        public string Residencia_departamento { get; set; }
        public string Residencia_provincia { get; set; }
        public string Residencia_distrito { get; set; }
        public string Mayor_65_años { get; set; }
        public string Lugar_pais_visita { get; set; }
        public string Correo { get; set; }
        public string Datos_contacto_emergencia { get; set; }
        public string Contratista { get; set; }
        public string N_ficha { get; set; }
        public string Zona_labor_concentradora { get; set; }
        public string Zona_labor_subsuelo { get; set; }
        public string Altitud_labor_debajo_2500m { get; set; }
        public string Altitud_labor_hasta_3000 { get; set; }
        public string Altitud_labor_3001m_5000m { get; set; }
        public string Altitud_labor_3501m_4000m { get; set; }

        public string Altitud_labor_4001m_4500m { get; set; }
        public string Altitud_labor_mas_4501m { get; set; }
        public string Año { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
        public string Empresa_salud { get; set; }
        public string Ultima_fecha_visita_bambas { get; set; }
        public string Nombre_correo_supervisor { get; set; }
        public string Ruc { get; set; }
        public string VcDocumentoNombre { get; set; }
        public string VcDocumentoCodigo { get; set; }
    }

    public class CitaTrabajador2
    {
        //public CitaTrabajador2()
        //{
        //    CitaTrabajador = new HashSet<CitaTrabajador>();
        //}
        public int IIdCitaTrabajador { get; set; }
        public DateTime? DtHoraDeLaCita { get; set; }
        public string VcCorreo { get; set; }
        public string VcCelular { get; set; }
        public string VcNombres { get; set; }
        public string VcApellidoPaterno { get; set; }
        public string VcApellidoMaterno { get; set; }
        public short? SiGenero { get; set; }
        public DateTime? DNacimiento { get; set; }
        public string VcNumeroIdentificacion { get; set; }
        public short? SiTipoIdentificacion { get; set; }
        public short? SiGrupoSanguineo { get; set; }
        public short? SiFactorSanguineo { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short SiEstado { get; set; }
        public string VcNota { get; set; }
        public decimal? DePrecio { get; set; }
        public int IIdCita { get; set; }
        //public int? IIdProtocolo { get; set; }
        public int? IIdSede { get; set; }
        public int? IIdServicio { get; set; }
        //public int? IIdEmpresaZona { get; set; }
        //public int? IIdEmpresaArea { get; set; }
        public int? IIdPersona { get; set; }
        //public int? IIdCitaEmpresaUnidad { get; set; }
        public string vcCodigoMigracion { get; set; }
        public string vcErrorMigracion { get; set; }
        //***
        public short Dia { get; set; }
        public TimeSpan HoraCita { get; set; }
        //public DateTime FechaCita { get; set; }
        public string VcEstado { get; set; }
        public string VcPuestoTemporal { get; set; }
    }

    public class AlertasDisponiblesQuery
    {
        public int IIdSede { get; set; }
        public string VcNombre { get; set; }
        public TimeSpan DtHoraIniico { get; set; }
        public TimeSpan DtHoraFin { get; set; }
        public int DuracionTurno { get; set; }
        public int Citados { get; set; }
        public int Libres { get; set; }
    }

    public partial class AlertasDisponibles
    {
        public int IdSede { get; set; }
        public string NombreSede { get; set; }
        public bool Abierto { get; set; }
        public string HoraInicioTurno { get; set; }
        public string HoraFinTurno { get; set; }
        public short DuracionTurno { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Citados { get; set; }
        public int Libres { get; set; }
    }

    public partial class AlertasDisponibles2
    {
        public int Dia { get; set; }
        public int IdSede { get; set; }
        //public string NombreSede { get; set; }
        public bool Abierto { get; set; }
        public string HoraInicioTurno { get; set; }
        public string HoraFinTurno { get; set; }
        public short DuracionTurno { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Citados { get; set; }
        public int Libres { get; set; }
    }


    public partial class SedeServicioHorarioTurnoCiclo
    {
        public SedeServicioHorarioTurnoCiclo() 
        {
            TurnosPorDuracion = new List<Atencion>();
        }

        [JsonIgnore]
        public short Dia { get; set; }
        public int IdSede { get; set; }
        public string SedeNombre { get; set; }
        public bool? Abierto { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        //public short SiNumeroSalas { get; set; }
        //public short SiAforoPorSala { get; set; }
        public short Duracion { get; set; }
        [JsonIgnore]
        public int Ciclos { get; set; }
        public int Libres { get; set; }
        public List<Atencion> TurnosPorDuracion { get; set; }
    }

    public class Atencion
    {
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Citados { get; set; }
        public int Libres { get; set; }
    }

    public partial class SedeServicioHorarioTurnoCiclo2
    {
        public SedeServicioHorarioTurnoCiclo2()
        {
            TurnosPorDuracion = new List<Atencion2>();
        }

        public short Dia { get; set; }
        public bool? Abierto { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public short Duracion { get; set; }
        public List<Atencion2> TurnosPorDuracion { get; set; }
    }

    public class Atencion2
    {
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
    }

}
