using ACME.Data.Contracts;
using ACME.Data.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System;
using Microsoft.Extensions.Logging;

namespace ACME.Data.DataManager
{
    public class MensajePlantillaManager : DbFactoryBase, IMensajePlantillaManager
    {
        private readonly ILogger<MensajePlantillaManager> _logger;
        public MensajePlantillaManager(IConfiguration config, ILogger<MensajePlantillaManager> logger) : base(config, "SQLDBConnectionString", logger)
        {
            _logger = logger;
        }

        public async Task<object> GetMensajePlantillaAsync(UrlQueryParameters urlQueryParameters)
        {
            var param = new
            {
                @Filter = urlQueryParameters.Filter,
                @Limit = urlQueryParameters.PageSize,
                @Offset = urlQueryParameters.PageNumber
            };

            var data = await DbQueryPagedAsync<MensajePlantilla>("sproc_MENSAJE_PLANTILLASelectAll", param, true);

            var result = new
            {
                Total = data.Item1,
                Items = data.Item2
            };

            return result;
        }

        public async Task<MensajePlantilla> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<MensajePlantilla>("sproc_MENSAJE_PLANTILLASelect", new { @iIdMensajePlantilla = id }, true);
        }

        public async Task<long> CreateAsync(MensajePlantilla entity)
        {
            var param = new {
                @vcNombre = entity.VcNombre,
                @vcMensaje = entity.VcMensaje,
                @iIdUsuarioCreacion = entity.IIdUsuarioCreacion,
            };

            return await DbQuerySingleAsync<long>("sproc_MENSAJE_PLANTILLAInsert", param, true);
        }

        public async Task<bool> UpdateAsync(MensajePlantilla entity)
        {
            var param = new
            {
                @iIdMensajePlantilla = entity.IIdMensajePlantilla,
                @vcNombre = entity.VcNombre,
                @vcMensaje = entity.VcMensaje,
                @iIdUsuarioModificacion = entity.IIdUsuarioModificacion
            };

            return await DbExecuteAsync<bool>("sproc_MENSAJE_PLANTILLAUpdate", param, true);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            ObjDeleted del = (ObjDeleted)id;
            var param = new
            {
                @iIdMensajePlantilla = del.id,
                @iIdUsuarioModificacion = del.userid
            };
            return await DbExecuteAsync<bool>("sproc_MENSAJE_PLANTILLADelete", param, true);
        }

        public Task<bool> ExistAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MensajePlantilla>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        


    }
}
