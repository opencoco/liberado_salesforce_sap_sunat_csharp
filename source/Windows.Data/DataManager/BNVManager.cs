using ACME.Data.Contracts;
using ACME.Data.Entity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace ACME.Data.DataManager
{
    public partial class BNVManager : DbFactoryBase, IBNVManager
    {
        private readonly IConfiguration _configuration;
        private readonly int iIdUsuario;
        private readonly ILogger<BNVManager> _logger;

        public BNVManager(IConfiguration configuration, ILogger<BNVManager> logger) : base(configuration, "SQLDBConnectionString", logger)
        {
            _configuration = configuration;
            iIdUsuario = int.Parse(@_configuration["WORKER_Parquet:IdUsuario"]);
            _logger = logger;
        }

        public async Task<string> RegistraTrabajador(BNVTrab ent)
        {
			string result = "";

			try
            {
                var param = new
                {
					@CodigoTrabajador = ent.CodigoTrabajador,
					@IdEmpleado = ent.IdEmpleado,
					@FechaAlta = ent.FechaAlta,
					@FechaCese = ent.FechaCese,
					@ApellidoPaterno = ent.ApellidoPaterno,
					@ApellidoMaterno = ent.ApellidoMaterno,
					@Nombres = ent.Nombres,
					@Sexo = ent.Sexo,
					@EstadoCivil = ent.EstadoCivil,
					@Edad = ent.Edad,
					@FechaNacimiento = ent.FechaNacimiento,
					@TipoDocumento = ent.TipoDocumento,
					@DNI = ent.DNI,
					@IdEmpresa = ent.IdEmpresa,
					@IdTipoEmpleado = ent.IdTipoEmpleado,
					@IdPuesto = ent.IdPuesto,
					@NombrePuesto = ent.NombrePuesto,
					@IdArea = ent.IdArea,
					@NombreArea = ent.NombreArea,
					@IdContrata = ent.IdContrata,
					@NombreContrata = ent.NombreContrata,
					@FechaInicioContrata = ent.FechaInicioContrata,
					@FechaFinContrata = ent.FechaFinContrata,
					@CodigoTipoContrato = ent.CodigoTipoContrato,
					@TipoContrato = ent.TipoContrato,
					@Direccion = ent.Direccion,
					@Telefono = ent.Telefono,
					@Email = ent.Email,
					@UbigeoNacimiento = ent.UbigeoNacimiento,
					@UbigeoDomicilio = ent.UbigeoDomicilio,
					@NivelDeRiesgo = ent.NivelDeRiesgo,
					@Celula = ent.Celula,
					@Anillo = ent.Anillo,
					@Categoria = ent.Categoria,
					@RestriccionesVigentes = ent.RestriccionesVigentes,
					@UltimaMarcaDeAcceso = ent.UltimaMarcaDeAcceso,
					@UltimaAreaDeAcceso = ent.UltimaAreaDeAcceso,
					@EntradaSalidaPrincipal = ent.EntradaSalidaPrincipal,
					@TipoPerfil = ent.TipoPerfil,
					@CodigoDeLaLocalidad = ent.CodigoDeLaLocalidad,
					@DescripcionDeLaLocalidad = ent.DescripcionDeLaLocalidad,
					@FechaUltimaModificacion = ent.FechaUltimaModificacion,
					@ContinuaLaborando = ent.ContinuaLaborando
				};

                result = await DbQuerySingleAsync<string>("sproc_BNV_TRABAJADORInsert", param, true);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.StackTrace);
                throw;
            }
            return result;
        }

		public async Task<object> RegistraTrabajadorActivo(BNVTrab ent)
		{
			try
			{
				var param = new
				{
					@DNI = ent.DNI,
					@FechaAlta = ent.FechaAlta,
					@Titular = ent.TitularMovimiento,
					@IdContrata = ent.IdContrata,
					@IdSubContrata = ent.IdSubContrata,
					@Unidad = ent.Unidad,
					@AreaAlta = ent.AreaAlta,
					@TipoPerfil = ent.TipoPerfil,
					@NombrePuesto = ent.NombrePuesto
				};

				var result = await DbExecuteAsync<object>("sproc_BNV_TRABAJADORUpdate_Activo", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw;
			}
			return true;
		}

		public async Task<object> CorrijeBajas()
		{
			try
			{
				var result = await DbExecuteAsync<object>("sproc_bnv_CorriejBajas", null, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw;
			}
			return true;
		}

		public async Task<object> SF_InsertError(string DNI,string ruc_titular,string vcError)
		{
			try
			{
				var param = new
				{
					@DNI,
					@ruc_titular,
					@vcError
				};
				var result = await DbExecuteAsync<object>("sproc_SF_ERRORInsert", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw;
			}
			return true;
		}


		public async Task<SFAlta> SF_GetPacienteAlta(string DNI)
		{
			return await DbQuerySingleAsync<SFAlta>("sproc_SF_GetPacienteAlta", new { @DNI }, true);
			
		}

		public async Task<string> SF_GetPacienteExamenExiste(string id_paciente,string cod_episodio)
		{
			return await DbQuerySingleAsync<string>("sproc_SF_GetPacienteExamenExiste", new { @id_paciente, @cod_episodio }, true);

		}

		public async Task<IEnumerable<SFDatosMedicos>> SF_GetPacienteDatosMedicosIni(string DNI)
		{
			return await DbQueryAsync<SFDatosMedicos>("sproc_SF_GetPacienteDatosIni", new { @DNI }, true);
		}

		public async Task<IEnumerable<SFEnviarDatos>> SF_GetPacientesPendientesDeEnviarDatos()
		{
			return await DbQueryAsync<SFEnviarDatos>("sproc_SF_ENVIA_DATOS_MEDICOS_Pending", null, true);
		}

		public async Task<IEnumerable<SFBajaDatos>> SF_GetBajaPacientesPendientes()
		{
			return await DbQueryAsync<SFBajaDatos>("sproc_SF_BajaPacientePendientes", null, true);
		}
		public async Task<IEnumerable<SFActualizaCita>> SF_GetUpdateCitas()
		{
			return await DbQueryAsync<SFActualizaCita>("sproc_SF_OrdenesMedicasNoti", null, true);
		}

		public async Task<bool> SF_BajaPacienteUpd(string id_paciente)
		{
			bool result = false;
			try
			{
				var param = new
				{
					@id_paciente
				};

				result = await DbExecuteAsync<bool>("sproc_SF_BajaPaciente", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task<bool> SF_OrdenMEdicaNotiUpd(string id_orden)
		{
			bool result = false;
			try
			{
				var param = new
				{
					@id_orden
				};

				result = await DbExecuteAsync<bool>("sproc_SF_ActualizaOrdenesMedicasNoti", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw new Exception(ex.Message);
			}
			return result;
		}



		public async Task<IEnumerable<string>> SF_GetPacientesPendientesDeAlta()
		{
			return await DbQueryAsync<string>("sproc_SF_ENVIA_AltaPending", null, true);
		}

		public async Task<ICollection<SFAlta>> sproc_SF_BAJAS_NoComunicadasBNV()
		{
			return (ICollection<SFAlta>)await DbQueryAsync<SFAlta>("sproc_SF_BAJAS_NoComunicadasBNV", null, true);

		}

		public async Task<bool> SF_SincronizaHeaCount()
		{
			return await DbExecuteAsync<bool>("sproc_SF_SincronizaHeadCount", null, true);

		}

		public async Task<bool> SF_InsertPacienteAlta(SFAlta ent)
		{
			bool result = false;
			try
			{
				var param = new
				{
					@id_paciente = ent.IdPaciente,
					@vcTipoIdentificacion = ent.TipoDocumento,
					@vcNumeroIdentificacion = ent.DocumentoIdentidad,
					@ruc_titular = ent.EXAM_COD_COMPAÑIA,
					@siEstado = ent.SiEstado,
					@vcPrimerNombre = ent.PrimerNombre,
					@vcSegundoNombre = ent.SegundoNombre,
					@vcApellidoPaterno = ent.ApellidoPaterno,
					@vcApellidoMaterno = ent.ApellidoMaterno,
					@vcGenero = ent.Sexo,
					@vcNacimiento = ent.FechaNacimiento,
					@ruc_contrata = ent.EXAM_COD_CONTRATA,
					@cod_unidad = ent.EXAM_COD_UNIDAD,
					@vcCodEpisodio = ent.EXAM_COD_ATENCION

				};

				result = await DbExecuteAsync<bool>("sproc_SF_PACIENTEInsert", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw new  Exception(ex.Message);
			}
			return result;
		}

		public async Task<bool> SF_ExamensPacienteAlta(string id_examen, string ruc_titular,string id_paciente,string cod_episodio,string FechaExamen,string tipo_examen,string ruc_contrata,string cod_unidad,string vcaltura_unidad,string cod_zona,string cod_area)
		{
			bool result = false;
			try
			{
				var param = new
				{
					@id_examen,
					@ruc_titular,
					@id_paciente,
					@cod_episodio,
					@FechaExamen,
					@tipo_examen,
					@ruc_contrata,
					@cod_unidad,
					@vcaltura_unidad,
					@cod_zona,
					@cod_area

				};

				result = await DbExecuteAsync<bool>("sproc_SF_EXAMENInsert", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw new Exception(ex.Message);
			}
			return result;
		}

		public async Task<bool> SF_InsertPacienteBaja(string id_paciente,int siEstado)
		{
			bool result = false;
			try
			{
				var param = new
				{
					@id_paciente,				
					@siEstado
				};

				result = await DbExecuteAsync<bool>("sproc_SF_PACIENTEBaja", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw new Exception(ex.Message);
			}
			return result;
		}


		public async Task<object> RegistraTrabajadorInActivo(BNVTrab ent)
		{
			try
			{
				var param = new
				{
					@DNI = ent.DNI,
					@Titular = ent.TitularMovimiento,
					@FechaCese = ent.FechaAlta,
					@IdContrata = ent.IdContrata,
					@Unidad = ent.Unidad
					
				};

				var result = await DbExecuteAsync<object>("sproc_BNV_TRABAJADORUpdate_InActivo", param, true);
			}
			catch (Exception ex)
			{
				_logger.LogCritical(ex.StackTrace);
				throw;
			}
			return true;
		}
	}


}