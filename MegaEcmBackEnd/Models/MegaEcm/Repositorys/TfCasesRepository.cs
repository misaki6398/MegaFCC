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
            SELECT
                MessageType,
                tf_cases.SwallowId,
                BusinessMessageIdentifier,                
                Currency,
                Amount,                 
                BranchNo,
                CaseStatus,
                tf_cases.CreateDateTime,
                tf_cases.Id as caseIdRaw
            FROM tf_cases
            JOIN tf_case_status ON tf_cases.CaseStatusCode = tf_case_status.CaseStatusCode and tf_case_status.validflag = 1
            WHERE 
                tf_cases.validflag = 1
        ";
        private readonly string _readRawDataSql = @"
            select 
                RAWMESSAGE
            from tf_cases 
            join tf_messages on tf_cases.messageid = tf_messages.Id
            where 
                tf_cases.Id = :CaseId
            and tf_messages.validflag = 1 
            and tf_cases.validflag = 1
        ";

        private readonly string _isCaseLockedSql = @"
            
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

        public async Task<IEnumerable<string>> QueryRawdataAsync(byte[] caseId)
        {
            string sql = $"{_readCaseSql} and tf_cases.Id = :caseId";
            try
            {
                return await Connection.QueryAsync<string>(_readRawDataSql, new{CaseId = caseId}, Transaction);
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