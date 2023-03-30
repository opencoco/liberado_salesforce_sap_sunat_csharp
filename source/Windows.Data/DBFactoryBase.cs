using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ACME.Data
{
    public abstract class DbFactoryBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly string _connectionString;

        public DbFactoryBase(IConfiguration config, string connectionString, ILogger<DbFactoryBase> logger)
        //public DbFactoryBase(IConfiguration config, string connectionString)
        {
            _config = config;
            _connectionString = connectionString;
            _logger = logger;
        }

        //internal IDbConnection DbConnection => new SqlConnection(_config.GetConnectionString("SQLDBConnectionString"));
        internal IDbConnection DbConnection => new SqlConnection(_config.GetConnectionString(_connectionString));

        protected IDbConnection OpenConnection()
        {
            DbConnection.Open();

            return DbConnection;
        }

        protected T Fetch<T>(Func<IDbConnection, T> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            using var connection = OpenConnection();
            return func(connection);
        }

        public virtual async Task<IEnumerable<T>> DbQueryAsync<T>(string sql, object parameters = null, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                {
                    if (parameters == null)
                        return await dbCon.QueryAsync<T>(sql);

                    return await dbCon.QueryAsync<T>(sql, parameters);
                }
                else
                {
                    if (parameters == null)
                        return await dbCon.QueryAsync<T>(sql, null, null, null, CommandType.StoredProcedure);

                    return await dbCon.QueryAsync<T>(sql, parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public virtual async Task<T> DbQuerySingleAsync<T>(string sql, object parameters, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                    return await dbCon.QueryFirstOrDefaultAsync<T>(sql, parameters);
                else
                    return await dbCon.QueryFirstOrDefaultAsync<T>(sql, parameters, null, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual T DbQuerySingle<T>(string sql, object parameters, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                    return dbCon.QueryFirstOrDefault<T>(sql, parameters);
                else
                    return dbCon.QueryFirstOrDefault<T>(sql, parameters, null, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<bool> DbExecuteAsync<T>(string sql, object parameters, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                    return await dbCon.ExecuteAsync(sql, parameters) > 0;
                else
                    return await dbCon.ExecuteAsync(sql, parameters, null, null, CommandType.StoredProcedure) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<bool> DbExecuteScalarAsync(string sql, object parameters, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                    return await dbCon.ExecuteScalarAsync<bool>(sql, parameters);
                else
                    return await dbCon.ExecuteScalarAsync<bool>(sql, parameters, null, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<T> DbExecuteScalarDynamicAsync<T>(string sql, object parameters = null, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                {
                    if (parameters == null)
                        return await dbCon.ExecuteScalarAsync<T>(sql);

                    return await dbCon.ExecuteScalarAsync<T>(sql, parameters);
                }
                else
                {
                    if (parameters == null)
                        return await dbCon.ExecuteScalarAsync<T>(sql, null, null, null, CommandType.StoredProcedure);

                    return await dbCon.ExecuteScalarAsync<T>(sql, parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

        public virtual T DbExecuteScalarDynamic<T>(string sql, object parameters = null, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                {
                    if (parameters == null)
                        return dbCon.ExecuteScalar<T>(sql);

                    return dbCon.ExecuteScalar<T>(sql, parameters);
                }
                else
                {
                    if (parameters == null)
                        return dbCon.ExecuteScalar<T>(sql, null, null, null, CommandType.StoredProcedure);

                    return dbCon.ExecuteScalar<T>(sql, parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public virtual async Task<Tuple<int, IEnumerable<T>>> DbQueryPagedAsync<T>(string sql, object parameters = null, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                SqlMapper.GridReader objPaged;
                int totalRows;
                IEnumerable<T> pagedRows;

                if (!isSp == true)
                {
                    if (parameters == null)
                        objPaged = await dbCon.QueryMultipleAsync(sql);

                    objPaged = await dbCon.QueryMultipleAsync(sql, parameters);
                }
                else
                {
                    if (parameters == null)
                        objPaged = await dbCon.QueryMultipleAsync(sql, null, null, null, CommandType.StoredProcedure);

                    objPaged = await dbCon.QueryMultipleAsync(sql, parameters, null, null, CommandType.StoredProcedure);
                }

                totalRows = objPaged.ReadFirst<int>();
                pagedRows = objPaged.Read<T>();

                return Tuple.Create(totalRows, pagedRows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<SqlMapper.GridReader> DbQueryMultipleAsync<T>(string sql, object parameters = null, bool isSp = false)
        {
            try
            {
                using IDbConnection dbCon = DbConnection;
                dbCon.Open();

                if (!isSp == true)
                {
                    if (parameters == null)
                        return await dbCon.QueryMultipleAsync(sql);

                    return await dbCon.QueryMultipleAsync(sql, parameters);
                }
                else
                {
                    if (parameters == null)
                        return await dbCon.QueryMultipleAsync(sql, null, null, null, CommandType.StoredProcedure);

                    return await dbCon.QueryMultipleAsync(sql, parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual async Task<bool> DbTransactionAsync<T>(List<(string sql, object parameters)> sentences, bool isSp = false)
        {
            using IDbConnection dbCon = DbConnection;
            dbCon.Open();

            using var transaction = dbCon.BeginTransaction();
            try
            {

                foreach (var (sql, parameters) in sentences)
                {
                    var result = await dbCon.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                }

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex.Message);
                throw;
            }
        }


    }
}
