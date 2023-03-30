//using ACME.Domain.Entity;
using ACME.Data;
using ACME.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace ACME.Data.Contracts
{
    public interface IBNVManager
    {
        Task<bool> SF_OrdenMEdicaNotiUpd(string id_orden);
        Task<IEnumerable<SFActualizaCita>> SF_GetUpdateCitas();
        Task<IEnumerable<SFBajaDatos>> SF_GetBajaPacientesPendientes();
        Task<bool> SF_BajaPacienteUpd(string id_paciente);

        Task<IEnumerable<string>> SF_GetPacientesPendientesDeAlta();
        Task<ICollection<SFAlta>> sproc_SF_BAJAS_NoComunicadasBNV();
        Task<object> SF_InsertError(string DNI, string ruc_titular, string vcError);
        Task<SFAlta> SF_GetPacienteAlta(string DNI);
        Task<bool> SF_InsertPacienteBaja(string id_paciente, int siEstado);
        Task<bool> SF_InsertPacienteAlta(SFAlta ent);
        Task<string> RegistraTrabajador(BNVTrab ent);
        Task<object> RegistraTrabajadorActivo(BNVTrab ent);
        Task<object> RegistraTrabajadorInActivo(BNVTrab ent);
        Task<object> CorrijeBajas();
        Task<IEnumerable<SFDatosMedicos>> SF_GetPacienteDatosMedicosIni(string DNI);
        Task<string> SF_GetPacienteExamenExiste(string id_paciente, string cod_episodio);
        Task<bool> SF_ExamensPacienteAlta(string id_examen, string ruc_titular, string id_paciente, string cod_episodio, string FechaExamen, string tipo_examen, string ruc_contrata, string cod_unidad, string vcaltura_unidad, string cod_zona, string cod_area);
        Task<IEnumerable<SFEnviarDatos>> SF_GetPacientesPendientesDeEnviarDatos();
        Task<bool> SF_SincronizaHeaCount();
    }
}
