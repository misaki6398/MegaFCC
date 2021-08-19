using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

using Oracle.ManagedDataAccess.Client;

namespace MegaTFLT.Models.MegaEcm.Repositorys
{
    public abstract class BaseRepository
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        protected string InsertSql { get; set; }
        public virtual async Task<int> InsertAsync<T>(T model)
        {
            return await InsertAsync(model, InsertSql);
        }
        public virtual async Task<int> InsertAsync<T>(List<T> models)
        {
            return await InsertAsync(models, InsertSql);
        }
        public virtual async Task<int> InsertAsync<T>(T model, string sql)
        {
            try
            {
                return await Connection.ExecuteAsync(sql, model, Transaction);
            }
            catch (OracleException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task<int> InsertAsync<T>(List<T> models, string sql)
        {
            try
            {
                return await Connection.ExecuteAsync(sql, models, Transaction);
            }
            catch (OracleException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}