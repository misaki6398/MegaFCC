using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.Models.MegaEcm.Repositorys
{
    public class TfCasesRepository : BaseRepository
    {
        private readonly string _readCaseSql = @"
                select 
                MessageDefinitionIdentifier,
                tf_cases.SwallowId,
                BusinessMessageIdentifier,                
                Currency,
                Amount,
                BranchNo,                
                CaseStatus,
                tf_messages.CreateDateTime,
                tf_cases.Id as caseIdRaw
            from tf_messages 
            join tf_cases on tf_messages.id = tf_cases.messageid
            where tf_messages.validflag = 1 
            and tf_cases.validflag = 1
        ";


        public TfCasesRepository(IDbTransaction transaction) : base(transaction)
        {

        }      

        public async Task<IEnumerable<TfCasesResource>> QueryCaseAsync()
        {
            try
            {
                return await Connection.QueryAsync<TfCasesResource>(_readCaseSql, new{}, Transaction);
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
        public async Task<IEnumerable<TfCasesResource>> QueryCaseAsync(byte[] caseId)
        {
            string sql = $"{_readCaseSql} and tf_cases.Id = :caseId";
            try
            {
                return await Connection.QueryAsync<TfCasesResource>(sql, new{CaseId = caseId}, Transaction);
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